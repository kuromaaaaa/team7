using System;
using UnityEngine;
using UnityEngine.UI;

public class StarCoinView : MonoBehaviour
{
	[SerializeField] private Image[] images;
	private Texture2D texture;
	public void GetStarCoin()
	{
		foreach (var image in images)
		{
			if (!image.IsActive())
			{
				image.gameObject.SetActive(true);
				return;
			}
		}
	}
}
