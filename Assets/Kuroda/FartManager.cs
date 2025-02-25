using UnityEngine;

public class FartManager : SceneSingletonMonoBehavior<FartManager>
{
    [SerializeField] private float fartParSecond;
    void Update() => FartGauge.Instance.GaugeValue += fartParSecond * Time.deltaTime;
}
