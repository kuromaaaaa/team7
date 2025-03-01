using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class GoalUIView : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private string _nextScene;


    public async void GoalView()
    {
        _text.gameObject.SetActive(true);
        _text.rectTransform.position = new Vector2(3000, 540);
        await _text.rectTransform.DOLocalMove(Vector3.zero, 1f).SetEase(Ease.InQuart);

        switch (SceneManager.GetActiveScene().name)
        {
            case "Stage1":
                _nextScene = "Stage2";
                break;
            case "Stage2":
                _nextScene = "Stage3";
                break;
            case "Stage3":
                _nextScene = "Ending";
                break;
        }

        await SceneLoader.Instance.FadeAndLoadSceneAsync(_nextScene);
    }
}