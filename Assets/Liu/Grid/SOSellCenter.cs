using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOSellCenter", menuName = "Scriptable Objects/SOSellCenter")]
public class SOSellCenter : ScriptableObject
{
    [SerializeField]
    public List<CellDataStruct> SellDataList;


}

[Serializable]
public struct CellDataStruct
{
    public GameObject SellGOBJ;
}
