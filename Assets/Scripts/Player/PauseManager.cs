using UnityEngine;

public class PauseManager : Singleton<PauseManager>
{
    [SerializeField] GameObject[] _menuesWithPause;

    public bool isPaused { get; private set; } = false;

    public void SetPause()
    {
        foreach(GameObject go in _menuesWithPause)
        {
            if (go.activeSelf)
            {
                isPaused = true;
                return;
            }
        }
        isPaused = false;
    }
}
