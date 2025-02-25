using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class Fade : MonoBehaviour
{
	[SerializeField] Image _panelImage;

	public async  UniTask FadeInAsync()
	{
		await _panelImage.DOFade(1, 0f).SetEase(Ease.Linear);
		await _panelImage.DOFade(0, 1f).SetEase(Ease.Linear);
		_panelImage.enabled = false;
		
	}

	public async UniTask FadeOutAsync()
	{
		_panelImage.enabled = true;
		await _panelImage.DOFade(0, 0f).SetEase(Ease.Linear);
		await _panelImage.DOFade(1, 1.5f).SetEase(Ease.Linear);
	}
}
