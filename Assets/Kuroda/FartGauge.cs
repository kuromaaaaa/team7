using System;

public class FartGauge : SceneSingletonMonoBehavior<FartGauge>
{
    
    private float _gaugeValue;
    
    public float GaugeValue
    {
        set
        {
            if (value > 1f)
            {
                _gaugeValue = 1f;
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
    
}