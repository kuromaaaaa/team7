using UnityEngine;

public abstract class SceneSingletonMonoBehavior<T> : MonoBehaviour where T : SceneSingletonMonoBehavior<T>
{
    private static T _instance;
    
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    Debug.LogError("Instanceされたオブジェクトがないです");
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this as T;
    }
}
