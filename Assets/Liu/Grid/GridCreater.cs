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
        new int[_weight] {2, 2, 1, 1, 1, 1, 1, 1, 1, 1 },
        new int[_weight] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[_weight] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[_weight] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[_weight] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[_weight] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[_weight] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[_weight] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int[_weight] {2, 2, 1, 1, 1, 1, 1, 1, 1, 1 },
        new int[_weight] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
    };

    private void Start()
    {
        for (int i = _arrayArray.Length - 1; i >= 0; i--)
        {
            var array = _arrayArray[i];
            for (int k = 0; k < array.Length; k++)
            {

                int targetGuildID = _arrayArray[i][k];
                if (targetGuildID == 0) return;
                var sell = Instantiate(_sOSellCenter.SellDataList[targetGuildID].SellGOBJ, _fielfTransform);
                sell.transform.position = new Vector2(i * _guildRange, k * _guildRange);
                sell.name = $"{i}:{k}";
            }
        } 
    }


}
