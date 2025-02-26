using System;
using UnityEngine;

public class FartGauge : SceneSingletonMonoBehavior<FartGauge>
{
    
    private float _gaugeValue;

    public GameObject Player;

    public bool FartStop = false;

    public float GaugeValue
    {
        set
        {
            if (value > 1f)
            {
                _gaugeValue = 1f;
                GaugeMax?.Invoke();
            }
            else
            {
                _gaugeValue = value;
                OnGaugeChange?.Invoke();
            }
        }
        get => _gaugeValue;
    }
    
    public event Action OnGaugeChange;
    public event Action GaugeMax;
}