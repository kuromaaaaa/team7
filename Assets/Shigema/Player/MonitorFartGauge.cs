using Cysharp.Threading.Tasks;
using UnityEngine;

public class MonitorFartGauge : MonoBehaviour
{
    [SerializeField, Header("死亡Anim名")] private string _death = "Death";

    [SerializeField, Header("ゲームオーバーシーン名")]
    private string _gameOver = "GameOver";

    private Animator _animator;
    [SerializeField] private float _gaugeValue;

    private void Start()
    {
        FartGauge.Instance.GaugeMax += GameOver;
    }

    public bool Judge(float value)
    {
        // ゲージの残量があればジャンプ可能
        if (FartGauge.Instance.GaugeValue >= value)
        {
            FartGauge.Instance.GaugeValue -= value;
            _gaugeValue = FartGauge.Instance.GaugeValue;
            return true;
        }

        return false;
    }

    private void GameOver()
    {
        Debug.Log("Die");
        if(!_animator) _animator = GameObject.FindWithTag("Player").gameObject.GetComponentInChildren<Animator>();
        ToGameOver().Forget();
    }

    private async UniTask ToGameOver()
    {
        _animator.Play(_death);
        await UniTask.WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        await SceneLoader.Instance.FadeAndLoadSceneAsync(_gameOver);
    }
}