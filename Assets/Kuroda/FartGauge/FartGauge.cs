using System;
using UnityEngine;

public class FartGauge : SceneSingletonMonoBehavior<FartGauge>
{
    
    private float _gaugeValue;
    private bool _isGameOver = false;
    
    public GameObject Player;

    public bool FartStop = false;

    public float GaugeValue
    {
        set
        {
            if (value > 1f && !_isGameOver)
            {
                _isGameOver = true;
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