using UnityEngine;

public class FartManager : MonoBehaviour ,PauseManager.IPauseable
{
    [SerializeField,Header("ゲージ増加量割合(毎秒)")] private float fartParSecond;
    FartGauge _fartGauge = new FartGauge();

    public FartGauge FartGauge
    {
        get => _fartGauge;
        set => _fartGauge = value;
    }

    bool _isPaused = false;
    
    private void Awake()
    {
        FindAnyObjectByType<ViewFartGauge>().GaugeSet(_fartGauge);
    }

    private void OnEnable()
    {
        PauseManager.Instance.Pause += Pause;
        PauseManager.Instance.Resume += Resume;
    }

    private void OnDisable()
    {
        PauseManager.Instance.Pause -= Pause;
        PauseManager.Instance.Resume -= Resume;
    }

    void Update()
    {
        if(!_isPaused)
            _fartGauge.GaugeValue += fartParSecond * Time.deltaTime;
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
    }
}
