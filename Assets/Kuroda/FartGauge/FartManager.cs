using UnityEngine;

public class FartManager : MonoBehaviour ,PauseManager.IPauseable
{
    [SerializeField,Header("ゲージ増加量割合(毎秒)")] private float fartParSecond;

    bool _isPaused = false;
    
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
            FartGauge.Instance.GaugeValue += fartParSecond * Time.deltaTime;
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
