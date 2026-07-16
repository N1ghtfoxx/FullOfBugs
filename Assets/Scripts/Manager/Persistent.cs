using UnityEngine;

public class Persistent : Singleton<Persistent>
{
    protected override void Awake()
    {
        base.Awake ();
        DontDestroyOnLoad(gameObject);
    }
}
