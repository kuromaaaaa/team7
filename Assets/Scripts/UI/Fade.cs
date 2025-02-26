using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class Fade : MonoBehaviour
{
	[SerializeField] Image _panelImage;
	[SerializeField] private Image _loadText;
	[SerializeField] private Image _loadImage;

	public async  UniTask FadeInAsync()
	{
		await _panelImage.DOFade(1, 0f).SetEase(Ease.Linear);
		await _panelImage.DOFade(0, 1f).SetEase(Ease.Linear);
		_panelImage.gameObject.SetActive(false);
		
	}

	public async UniTask FadeOutAsync()
	{
		_panelImage.gameObject.SetActive(true);
		await _panelImage.DOFade(0, 0f).SetEase(Ease.Linear);
		await _panelImage.DOFade(1, 1.5f).SetEase(Ease.Linear);
		_loadText.gameObject.SetActive(true);
		_loadImage.gameObject.SetActive(true);
		await UniTask.Delay(TimeSpan.FromSeconds(1f));
		_loadText.gameObject.SetActive(false);
		_loadImage.gameObject.SetActive(false);
	}
}
