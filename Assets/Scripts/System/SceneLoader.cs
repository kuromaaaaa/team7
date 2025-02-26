using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : SingletonMonoBehavior<SceneLoader>
{
    public SceneType CurrentSceneType = SceneType.Title;
    public event Action OnSceneLoaded;

    public async UniTask LoadSceneAsync(string sceneName)
    {
        await SceneManager.LoadSceneAsync(sceneName);
        ChangeBGM((SceneType)Enum.Parse(typeof(SceneType), sceneName));
        OnSceneLoaded?.Invoke();
    }

    //FadeInFadeoutができるシーン移動
    public async UniTask FadeAndLoadSceneAsync(string sceneName)
    {
        if (TryGetComponentInChildren<Fade>(out Fade fade) && fade != null)
        {
            await fade.FadeOutAsync();
            await LoadSceneAsync(sceneName);
            ChangeBGM((SceneType)Enum.Parse(typeof(SceneType), sceneName));
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

    public SceneType ChangeCurrentSceneType(SceneType sceneType)
    {
        CurrentSceneType = sceneType;
        return CurrentSceneType;
    }

    public void ChangeBGM(SceneType sceneType)
    {
        switch (ChangeCurrentSceneType(sceneType))
        {
            case SceneType.Title:
            case SceneType.Opening:
                BGMManager.Instance.NextBGM = AudioClipType.BGM_Title;
                break;
            case SceneType.Stage1:
            case SceneType.Stage2:
            case SceneType.Stage3:
                BGMManager.Instance.NextBGM = AudioClipType.BGM_Stage;
                break;
            case SceneType.Ending:
                BGMManager.Instance.NextBGM = AudioClipType.BGM_Ending;
                break;
            case SceneType.GameOver:
                BGMManager.Instance.NextBGM = AudioClipType.BGM_GameOver;
                break;
        }
    }
}

public enum SceneType
{
    Title,
    Opening,
    Stage1,
    Stage2,
    Stage3,
    GameOver,
    Ending,
    None
}