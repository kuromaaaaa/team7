using System;
using UnityEngine;
using UnityEngine.UI;

public class LifeView : MonoBehaviour
{
	Image[] images = new Image[3];

	private void Start()
	{
		images = gameObject.GetComponentsInChildren<Image>();
		PlayerHP playerHp = FindAnyObjectByType<PlayerHP>();
		playerHp.TakeDamageAction += Life;
	}

	private void Life(int remains)
	{
		for (int i = 0; i < images.Length; i++)
		{
			if (i > remains)
			{
				images[i].enabled = true;
			}
			else if (i <= remains)
			{
				images[i].enabled = false;
			}
		}
	}
}
