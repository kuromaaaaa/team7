using System;
using UnityEngine;
using UnityEngine.UI;

public class StarCoin : MonoBehaviour
{
	[SerializeField] private Image[] images;
	[SerializeField] private string path;
	private Texture2D texture;
	private void Start()
	{
		texture = Resources.Load<Texture2D>("");
	}

	public void GetStarCoin()
	{
		foreach (var image in images)
		{
			if (!image.IsActive())
			{
				image.enabled = true;
				return;
			}
		}
	}
}
