using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LifeView : MonoBehaviour
{
	[SerializeField] private Text _lifeText;
	private CancellationToken _token;

	private async void Start()
	{
		_token = new CancellationTokenSource().Token;
		await UniTask.WaitUntil(() => FindAnyObjectByType<PlayerHP>() != null, PlayerLoopTiming.Update, _token);
		PlayerHP playerHp = FindAnyObjectByType<PlayerHP>();
		playerHp.TakeDamageAction += Life;
	}

	private void Life(int remains)
	{
		_lifeText.text = $"×{remains}";
	}
}
