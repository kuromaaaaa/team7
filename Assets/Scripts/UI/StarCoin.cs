using System;
using UnityEngine;

public class StarCoin : MonoBehaviour
{
	StarCoinView view;

	private void Start()
	{
		view = FindAnyObjectByType<StarCoinView>();
	}

	public void GetCoin()
	{
		if(view == null)
			view = FindAnyObjectByType<StarCoinView>();
		view.GetStarCoin();
	}
}
