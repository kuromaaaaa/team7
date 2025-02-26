using System;
using UnityEngine;

public class FartManager : MonoBehaviour
{
    [SerializeField] private float fartParSecond;
    FartGauge _fartGauge = new FartGauge();

    private void Start()
    {
        FindAnyObjectByType<ViewFartGauge>().GaugeSet(_fartGauge);
    }

    void Update() => _fartGauge.GaugeValue += fartParSecond * Time.deltaTime;
}
