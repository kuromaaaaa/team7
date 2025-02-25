using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
	[SerializeField] Slider _slider;
	Tween _tween;

	public void SliderValueChange(float value)
	{
		if (_tween != null) _tween.Kill();
		_tween = _slider.DOValue(value, 1f);
	}
}
