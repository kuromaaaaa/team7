using UnityEngine;
using UnityEngine.UI;

public class ViewFartGauge : MonoBehaviour
{
    [SerializeField] Image image;
    private void OnEnable()
    {
        FartGauge.Instance.OnGaugeChange += UpdateView;
    }
    private async void UpdateView()
    {
        image.fillAmount = FartGauge.Instance.GaugeValue;
    }
}