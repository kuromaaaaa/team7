using System;
using UnityEngine;
using UnityEngine.UI;

public class ViewFartGauge : MonoBehaviour
{
    [SerializeField] Image _fillImage;
    
    private void OnEnable()
    {
        FartGauge.Instance.OnGaugeChange += UpdateView;
    }

    private void OnDisable()
    {
        FartGauge.Instance.OnGaugeChange -= UpdateView;
    }

    private async void UpdateView()
    {
        _fillImage.fillAmount = FartGauge.Instance.GaugeValue;
    }

    public void GaugeColor()
    {
        
    }
}