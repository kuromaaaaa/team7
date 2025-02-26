using System;

public class FartGauge
{
    
    private float _gaugeValue;
    
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