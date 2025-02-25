using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : SingletonMonoBehavior<SceneLoader>
{
	public async UniTask LoadSceneAsync(string sceneName)
	{
		await SceneManager.LoadSceneAsync(sceneName);
	}

	//FadeInFadeoutができるシーン移動
	public async UniTask FadeAndLoadSceneAsync(string sceneName)
	{
		if (TryGetComponentInChildren<Fade>(out Fade fade))
		{
			await fade.FadeOutAsync();
			await LoadSceneAsync(sceneName);
			await fade.FadeInAsync();
		}
	}

	public void FadeAndLoadSceneAsyncButton(string sceneName)
	{
		FadeAndLoadSceneAsync(sceneName);
	}

	public bool TryFindObjectByType<T>(out T obj) where T : UnityEngine.Object
	{
		obj = FindAnyObjectByType<T>();
		if (obj != null)
			return true;
		return false;
	}
	public bool TryGetComponentInChildren<T>(out T obj) where T : UnityEngine.Object
	{
		obj = GetComponentInChildren<T>();
		if (obj != null)
			return true;
		return false;
	}
}
