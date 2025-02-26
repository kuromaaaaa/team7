using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LifeView : MonoBehaviour
{
	Image[] images = new Image[3];
	private CancellationToken _token;

	private async void Start()
	{
		_token = new CancellationTokenSource().Token;
		images = gameObject.GetComponentsInChildren<Image>();
		await UniTask.WaitUntil(() => FindAnyObjectByType<PlayerHP>() != null, PlayerLoopTiming.Update, _token);
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
