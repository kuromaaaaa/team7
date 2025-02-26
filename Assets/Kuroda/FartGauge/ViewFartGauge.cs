using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ViewFartGauge : MonoBehaviour
{
    [SerializeField] Image _fillImage;
    CancellationTokenSource _cts;
    Tween _flashingTween;
    Color _originalColor;

    private const float MaxGaugeSize = 0.78f;
    private const float FlashingDuration = 1f / 4;
    private const float FlashingTimer = 0.2f;
    private void OnEnable()
    {
        FartGauge.Instance.OnGaugeChange += UpdateView;
        _originalColor = _fillImage.color;
    }

    private void OnDisable()
    {
        Unsubscribe();
        _cts?.Cancel();
        _cts?.Dispose();
        _flashingTween?.Kill();
    }

    void Unsubscribe()
    {
        if (FartGauge.Instance == null)
            return;
        FartGauge.Instance.OnGaugeChange -= UpdateView;
    }
    
    private async void UpdateView()
    {
        _fillImage.fillAmount = FartGauge.Instance.GaugeValue * MaxGaugeSize;
    }

    public async void GaugeColorChange(Color color, float duration)
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();
        _flashingTween?.Kill();
        
        try
        {
            _fillImage.color = color;
            await UniTask.Delay(TimeSpan.FromSeconds(duration - duration * FlashingDuration),
                cancellationToken: _cts.Token);
            _flashingTween = _fillImage.DOColor(_originalColor, FlashingTimer).SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
            await UniTask.Delay(TimeSpan.FromSeconds(duration * FlashingDuration), cancellationToken: _cts.Token);
            _flashingTween.Kill();
            _fillImage.color = _originalColor;
        }
        catch (OperationCanceledException)
        {
            _flashingTween.Kill();
        }
    }
}