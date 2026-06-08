using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

/* How to use:
   You need to use using UnityEngine.Events
1. Create a UnityEvent
   UnityEvent e = new UnityEvent();
2. Add your functions to the UnityEvent
    e.AddListener(() =>
    {
        //Your functions on confirmation
    });
3. Call this instance function Show with
    your created event,
    the region for the toggle (if your region dont exist just add it to the enum below)
    and optional your shown text
    ConfirmManager.instance.Show(e, DontAskRegion.SkillTree, $"Text you want to show if you dont want the default of 'Are you sure?'");
 */

public class ConfirmManager : MonoBehaviour
{
    public static ConfirmManager instance;

    public GameObject _confirmPanel;
    private TMP_Text _confirmText;
    private Toggle _dontAskAgainToggle;

    private bool[] dontAskToggle;

    public UnityEvent onConfirm;

    private DontAskRegion currentRegion;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        _confirmPanel = GameObject.Find("ConfirmPanel");
        _confirmText = _confirmPanel.GetComponentInChildren<TMP_Text>();
        _dontAskAgainToggle = _confirmPanel.GetComponentInChildren<Toggle>();
        _confirmPanel.SetActive(false);

        dontAskToggle = new bool[DontAskRegion.GetValues(typeof(DontAskRegion)).Length];
    }

    public void Show(UnityEvent confirmAction, DontAskRegion region, string text = "Are you sure?")
    {
        currentRegion = region;
        if (dontAskToggle[(int)region])
        {
            confirmAction?.Invoke();
            return;
        }
        onConfirm = confirmAction;
        _confirmText.text = text;
        _dontAskAgainToggle.isOn = false;
        _confirmPanel.SetActive(true);
    }

    public void Confirm()
    {
        onConfirm?.Invoke();
        _confirmPanel.SetActive(false);
    }

    public void Cancel()
    {
        _confirmPanel.SetActive(false);
    }

    public void ToggleDontAsk(bool value)
    {
        dontAskToggle[(int)currentRegion] = value;
    }
}

public enum  DontAskRegion
{
    SkillTree,
    Save,
    Load,
    DeleteSave
}