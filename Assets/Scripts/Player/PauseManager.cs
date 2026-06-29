using UnityEngine;

public class PauseManager : Singleton<PauseManager>
{
    [SerializeField] GameObject[] _menuesWithPause;
    [SerializeField] GameObject _hud;

    public bool isPaused { get; private set; } = false;

    public void SetPause()
    {
        foreach(GameObject go in _menuesWithPause)
        {
            if (go.activeSelf)
            {
                isPaused = true;
                _hud.SetActive(false);
                return;
            }
        }
        isPaused = false;
        _hud.SetActive(true);
    }

    public void ForcePause()
    {
        isPaused = true;
        _hud.SetActive(false);
    }
}
