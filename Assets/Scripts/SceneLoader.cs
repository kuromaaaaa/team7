using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : SingletonMonoBehavior<SceneLoader>
{
	public string CurrentScene = "Title";
	public event Action OnSceneLoaded;
	public async UniTask LoadSceneAsync(string sceneName)
	{
		CurrentScene = sceneName;
		await SceneManager.LoadSceneAsync(sceneName);
		OnSceneLoaded?.Invoke();
	}

	//FadeInFadeoutができるシーン移動
	public async UniTask FadeAndLoadSceneAsync(string sceneName)
	{
		CurrentScene = sceneName;
		if (TryGetComponentInChildren<Fade>(out Fade fade))
		{
			await fade.FadeOutAsync();
			await LoadSceneAsync(sceneName);
			await fade.FadeInAsync();
			OnSceneLoaded?.Invoke();
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
