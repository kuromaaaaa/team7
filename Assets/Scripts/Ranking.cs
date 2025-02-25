using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
	private string iPAdress = "10.76.10.235";
	[SerializeField] private string port = "5000";
	[SerializeField] private Text _rankingText;
	[SerializeField] private Text _text;

	List<PlayerData> ranking = new List<PlayerData>();

	private void Start()
	{
		PlayerDataManager.Instance.PlayerData.Score = 10.56f;
		CreateRanking();
	}

	private void CreateRanking()
	{
		string URL = "http://";
		URL += iPAdress;
		URL += ":";
		URL += port;
		URL += @"/";
		Debug.Log("Server URL = " + URL);
		StartCoroutine(OnSend(URL));
	}

	//コルーチン
	private IEnumerator OnSend(string url)
	{
		//POSTする情報
		WWWForm form = new WWWForm();
		form.AddField("ID", PlayerDataManager.Instance.PlayerData.ID, Encoding.UTF8);
		form.AddField("Name", PlayerDataManager.Instance.PlayerData.Name, Encoding.UTF8);
		form.AddField("Score", (int)(PlayerDataManager.Instance.PlayerData.Score * 100));
		//URLをPOSTで用意
		UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
		//UnityWebRequestにバッファをセット
		webRequest.downloadHandler = new DownloadHandlerBuffer();
		//URLに接続して結果が戻ってくるまで待機
		yield return webRequest.SendWebRequest();
		//エラーが出ていないかチェック
		if (webRequest.isNetworkError)
		{
			//通信失敗
			Debug.Log(webRequest.error);
		}
		else
		{
			//通信成功
			string json = "{\"Ranking\":" + webRequest.downloadHandler.text + "}";
			Debug.Log(json);

			RankingData users = JsonUtility.FromJson<RankingData>(json);

			ranking = users.Ranking.ToList();

			IndicateRanking();
		}
	}

	private void IndicateRanking()
	{
		ranking.Sort((x, y) => (int)(y.Score - x.Score));
		Debug.Log(PlayerDataManager.Instance.PlayerData.ID);
		for (int i = 0; i < ranking.Count && i < 10; i++)
		{
			Debug.Log(ranking[i].ID);
			if (PlayerDataManager.Instance.PlayerData.ID == ranking[i].ID)
				_rankingText.text +=
					$"<color=\"yellow\">{PlayerDataManager.Instance.PlayerData.Name}: {PlayerDataManager.Instance.PlayerData.Score}points</color>\n";
			else
			_rankingText.text += $"{ranking[i].Name}: {ranking[i].Score/100}points\n";
		}

		_text.text =
			$"<color=\"yellow\">{PlayerDataManager.Instance.PlayerData.Name}: {PlayerDataManager.Instance.PlayerData.Score}</color>";
	}
}
