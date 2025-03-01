using UnityEngine;

public class FartManager : MonoBehaviour, PauseManager.IPauseable
{
    [SerializeField, Header("ゲージ増加量割合(毎秒)")] private float fartParSecond;

    bool _isPaused = false;

    private void OnEnable()
    {
        PauseManager.Instance.Pause += Pause;
        PauseManager.Instance.Resume += Resume;
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    void Unsubscribe()
    {
        if (PauseManager.Instance == null)
            return;
        PauseManager.Instance.Pause -= Pause;
        PauseManager.Instance.Resume -= Resume;
    }

    void Update()
    {
        if (!_isPaused && FartGauge.Instance.Player != null)
        {
            if (FartGauge.Instance.FartStop == false)
            {
                FartGauge.Instance.GaugeValue += fartParSecond * Time.deltaTime;
            }
        }
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
