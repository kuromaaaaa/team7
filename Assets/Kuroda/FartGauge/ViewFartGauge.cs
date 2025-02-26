using UnityEngine;
using UnityEngine.UI;

public class ViewFartGauge : MonoBehaviour
{
    [SerializeField] Image _fillImage;

    private FartGauge _gauge;
    public void GaugeSet(FartGauge f) => _gauge = f;
    private void OnEnable()
    {
        _gauge.OnGaugeChange += UpdateView;
    }
    private async void UpdateView()
    {
        _fillImage.fillAmount = _gauge.GaugeValue;
    }
}