using System.Net.WebSockets;
using System.Threading;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using UnityEditor;

public class WebManager : SingletonMonoBehavior<WebManager>
{
    [SerializeField] GameObject playerPrefab;
    public ClientWebSocket ws = default;
    private string URL = "ws://192.168.0.32:8080/";
    public UserData UserData = new();
    private SemaphoreSlim sendLock = new SemaphoreSlim(1, 1); // 送信をスレッドセーフにする
    private CancellationTokenSource _ctsReceiving = new();

    Dictionary<string, GameObject> players = new();


    private async void Start()
    {
        Guid guid = Guid.NewGuid();
        UserData.ID = guid.ToString();
        UserData.Name = "Slime";
        await ConnectServerByPlayerID();
        CommunicateData aaa = new();
        aaa.ID = UserData.ID;
        aaa.Command = "JoinGame";
        aaa.JsonBody = "";
        string jsons = JsonUtility.ToJson(aaa);
        await SendCommand(jsons);
        
        
        CommunicateData data = new();
        data.ID = UserData.ID;
        data.Command = "InstancePlayer";
        data.JsonBody = "";
        string json = JsonUtility.ToJson(data);
        await SendCommand(json);
    }

    /// <summary>
    /// WebSocket サーバーへ接続
    /// </summary>
    /// <returns>失敗時 null</returns>
    public async UniTask<ClientWebSocket> ConnectServerByPlayerID()
    {
        if (UserData.ID == default)
        {
            Debug.LogWarning("IDがありません。先にログインして下さい");
            return null;
        }

        ws = new ClientWebSocket();
        Uri serverUri = new Uri(URL);

        try
        {
            Debug.Log("WebSocket に接続中...");
            await ws.ConnectAsync(serverUri, CancellationToken.None);

            if (ws.State == WebSocketState.Open)
            {
                Debug.Log($"WebSocket に接続成功: {URL}");

                // ID をサーバーに送信
                //await SendCommand("AddID");

                // メッセージの受信開始（非同期で実行）
                _ = StartReceiving(_ctsReceiving.Token);
                return ws;
            }
            else
            {
                Debug.LogError("WebSocket 接続失敗");
                ws = null;
                return null;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"WebSocket 接続エラー: {ex.Message}");
            ws = null;
            return null;
        }
    }

    /// <summary>
    /// WebSocket サーバーにコマンドを送信する
    /// </summary>
    public async UniTask SendCommand(string command, string jsonbody = "")
    {
        if (ws == null || ws.State != WebSocketState.Open)
        {
            Debug.LogWarning("WebSocket に接続されていないため、コマンドを送信できません。");
            return;
        }

        try
        {
            byte[] buffer = Encoding.UTF8.GetBytes(command);

            await sendLock.WaitAsync();
            try
            {
                await ws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true,
                    CancellationToken.None);
                Debug.Log($"コマンド送信: {command}");
            }
            finally
            {
                sendLock.Release();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"WebSocket コマンド送信エラー: {ex.Message}");
        }
    }

    /// <summary>
    /// WebSocket メッセージの受信
    /// </summary>
    private async UniTask StartReceiving(CancellationToken cancellationToken)
    {
        Debug.Log("StartReceiving");
        var buffer = new byte[4096]; // バッファサイズを調整
        try
        {
            while (ws != null && ws.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
            {
                WebSocketReceiveResult result =
                    await ws.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
                string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Debug.Log($"Received: {message}");

                CommunicateData communicateData = JsonUtility.FromJson<CommunicateData>(message);

                switch (communicateData.Command)
                {
                    case "JoinGame":
                        JoinGame(communicateData.JsonBody);
                        break;
                    case "InstancePlayer":
                        InstancePlayer(communicateData.ID);
                        break;
                    case "RemovePlayer":
                        RemovePlayer(communicateData.ID);
                        break;
                    case "SynchronizeTransform":
                        ApplyTransform(communicateData.ID, JsonUtility.FromJson<Vector3>(communicateData.JsonBody));
                        break;
                    case "SynchronizeRotation":
                        break;
                }
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("WebSocket 受信処理がキャンセルされました。");
        }
        catch (Exception ex)
        {
            Debug.LogError($"WebSocket 受信エラー: {ex.Message}");
            await Reconnect();
        }
    }

    /// <summary>
    /// WebSocket 再接続処理
    /// </summary>
    private async UniTask Reconnect()
    {
        Debug.Log("WebSocket 再接続中...");
        ws = null;
        await UniTask.Delay(3000); // 3秒後に再接続
        await ConnectServerByPlayerID();
    }


    private async void OnDestroy()
    {
        if (ws != null && ws.State == WebSocketState.Open)
        {
            try
            {
                await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection Closed", CancellationToken.None);
                Debug.Log("WebSocket を正常に閉じました。");
            }
            catch (Exception ex)
            {
                Debug.LogError($"WebSocket を閉じる際のエラー: {ex.Message}");
            }
            finally
            {
                ws = null;
            }
        }
    }

    public void JoinGame(string json)
    {
        Debug.Log(json);
        if(json == "[]") return;
        string processing = "{\"Players\":" + json + "}";
        RoomPlayersData data = JsonUtility.FromJson<RoomPlayersData>(processing);
        foreach (var id in data.Players)
        {
            InstancePlayer(id);
        }
    }

    public void InstancePlayer(string ID)
    {
        GameObject player = Instantiate(playerPrefab);
        player.GetComponent<SynchronizePlayer>().data.ID = ID;
        players.Add(ID, player);
    }

    public void RemovePlayer(string ID)
    {
        players.Remove(ID);
        Destroy(players[ID]);
    }

    public void ApplyTransform(string ID, Vector3 position)
    {
        if (ID != UserData.ID && players.ContainsKey(ID))
        {
            players[ID].transform.position = position;
        }
    }

    public void ApplyRotation(string ID, Quaternion rotation)
    {
        if (ID != UserData.ID && players.ContainsKey(ID))
        {
            players[ID].transform.rotation = rotation;
        }
    }

    public bool IsPlayer(string ID)
    {
        return UserData.ID == ID;
    }
}
