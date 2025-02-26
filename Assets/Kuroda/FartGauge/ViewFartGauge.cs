using System;
using UnityEngine;
using UnityEngine.UI;

public class ViewFartGauge : MonoBehaviour
{
    [SerializeField] Image image;

    private FartGauge _gauge;
    public void GaugeSet(FartGauge f) => _gauge = f;
    private void OnEnable()
    {
        _gauge.OnGaugeChange += UpdateView;
    }
    private async void UpdateView()
    {
        image.fillAmount = _gauge.GaugeValue;
    }
}