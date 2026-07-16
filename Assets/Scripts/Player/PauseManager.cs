using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : Singleton<PauseManager>
{
    [SerializeField] GameObject[] _menuesWithPause;
    //Fightpanel index
    [SerializeField] int _fightPanelIndex = 5;
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

    public void OnEsc(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!isPaused)
            {
                UiManager.instance.ToggglePausepanel();
                SetPause();
            }
            else
            {
                if (_menuesWithPause[_fightPanelIndex].activeSelf) return;
                foreach (GameObject go in _menuesWithPause)
                {
                    go.SetActive(false);
                }
                SetPause();
            }
        }
    }
}
