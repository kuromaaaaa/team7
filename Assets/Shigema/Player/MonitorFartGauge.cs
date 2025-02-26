using Cysharp.Threading.Tasks;
using UnityEngine;

public class JudgeGauge : MonoBehaviour
{
    [SerializeField, Header("死亡Anim名")] private string _death = "Death";

    [SerializeField, Header("ゲームオーバーシーン名")]
    private string _gameOver = "GameOver";

    private Animator _animator;
    [SerializeField] private float _gaugeValue;

    private void Start()
    {
        FartGauge.Instance.GaugeMax += GameOver;
        _animator = GameObject.FindWithTag("Player").gameObject.GetComponent<Animator>();
    }

    public bool Judge(float value)
    {
        var gaugeValue = FartGauge.Instance.GaugeValue;
        // ゲージの残量があればジャンプ可能
        if (gaugeValue >= value)
        {
            gaugeValue -= value;
            _gaugeValue = gaugeValue;
            return true;
        }

        return false;
    }

    private void GameOver()
    {
        ToGameOver().Forget();
    }

    private async UniTask ToGameOver()
    {
        _animator.Play(_death);
        await UniTask.WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        await SceneLoader.Instance.FadeAndLoadSceneAsync(_gameOver);
    }
}