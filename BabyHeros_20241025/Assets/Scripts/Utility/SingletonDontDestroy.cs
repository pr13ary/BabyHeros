using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonDontDestroy<T> : MonoBehaviour where T : SingletonDontDestroy<T>
{
    public static T Instance { get; private set; }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
            OnAwake();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Start()
    {
        if (Instance == this)
        {
            OnStart();
        }
    }
}
