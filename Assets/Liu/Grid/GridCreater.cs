using Cysharp.Threading.Tasks;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public SOSellCenter _sOSellCenter;
    public float _guildRange;
    const int _weight = 10;
    const int _hight = 10;
    public Transform _fielfTransform;

    private int[][] _arrayArray = new int[_hight][]
    {
        new int[_weight] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[_weight] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[_weight] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[_weight] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[_weight] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[_weight] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[_weight] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[_weight] { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[_weight] { 2, 2, 1, 1, 1, 1, 1, 1, 1, 1 },
        new int[_weight] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
    };

    private async void Start()
    {
        for (int y = _arrayArray.Length - 1; y >= 0; y--)
        {
            await UniTask.Delay(10);
            float py = (_hight - y - 1) * _guildRange;

            var array = _arrayArray[y];
            for (int x = 0; x < array.Length; x++)
            {

                await UniTask.Delay(10);

                int targetGuildID = _arrayArray[y][x];
                if (targetGuildID == 0) continue;
                var sell = Instantiate(_sOSellCenter.SellDataList[targetGuildID].SellGOBJ, _fielfTransform);
                sell.transform.position = new Vector2(x * _guildRange, py);
                sell.name = $"{x}:{py}";
            }
        }
    }


}
