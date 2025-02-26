using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GridCreater : MonoBehaviour
{
    public TextAsset csvFile;

    public SOSellCenter _sOSellCenter;
    public float _guildRange;

    public Transform _fielfTransform;

    private int[][] _arrayArray;

    private int[][] LoadCSV(TextAsset csv)
    {
        string[] lines = csv.text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        int[][] result = new int[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            result[i] = lines[i].Split(',').Select(int.Parse).ToArray();
        }

        return result;
    }

    private void PrintArray()
    {
        foreach (var row in _arrayArray)
        {
            Debug.Log(string.Join(",", row));
        }
    }

    private async void Start()
    {
         _arrayArray = LoadCSV(csvFile);
        for (int y = _arrayArray.Length - 1; y >= 0; y--)
        {
            await UniTask.Delay(10);
            float py = (_arrayArray.Length - y - 1) * _guildRange;

            var array = _arrayArray[y];
            for (int x = 0; x < array.Length; x++)
            {

                await UniTask.Delay(10);

                int targetGuildID = _arrayArray[y][x];
                if (targetGuildID == 0) continue;
                var sell = Instantiate(_sOSellCenter.SellDataList[targetGuildID].SellGOBJ, _fielfTransform);
                sell.transform.position = new Vector2(x * _guildRange, py);
                if(targetGuildID != 7)
                {
                    sell.name = $"{x}:{py}";
                }
                    
            }
        }
    }
}
