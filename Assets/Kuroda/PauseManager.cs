using System;
using UnityEngine;

public class PauseManager : SceneSingletonMonoBehavior<PauseManager>
{
    public Action Pause;
    public Action Resume;
    bool paused = false;

    public bool Paused
    {
        get => paused;
        set
        {
            (value ? Pause : Resume)?.Invoke();
            paused = value;
        }
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            PauseGame(!paused);
        }
    }

    public void PauseGame(bool pauseState)
    {
        Paused = pauseState;
    }
    
    public interface IPauseable
    {
        abstract void Pause();

        abstract void Resume();
    }

}
