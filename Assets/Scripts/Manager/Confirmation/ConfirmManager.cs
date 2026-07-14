using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

/* How to use:
   You need to use using UnityEngine.Events
Checkout dont ask region optionally to save performance
ConfirmManager.instance.CheckDontAsk(DontAskRegion.Skilltree);
1. Create a UnityEvent
   UnityEvent e = new UnityEvent();
2. Add your functions to the UnityEvent
    e.AddListener(() =>
    {
        //Your functions on confirmation
    });
3. Call this instance function AskForConfirmation with
    your created event,
    the region for the toggle (if your region dont exist just add it to the enum below)
    and optional your shown text
    ConfirmManager.instance.AskForConfirmation(e, DontAskRegion.Skilltree, $"Text you want to show if you dont want the default of 'Are you sure?'");
 */

public class ConfirmManager : MonoBehaviour
{
    public static ConfirmManager instance;

    public GameObject _confirmPanel;
    [SerializeField] TMP_Text _confirmText;
    [SerializeField] Toggle _dontAskAgainToggle;

    private bool[] dontAskToggle;

    public UnityEvent onConfirm;
    public UnityEvent onCancel;

    private DontAskRegion currentRegion;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //_confirmPanel = GameObject.Find("ConfirmPanel");
        //_confirmText = _confirmPanel.GetComponentInChildren<TMP_Text>();
        //_dontAskAgainToggle = _confirmPanel.GetComponentInChildren<Toggle>();
        _confirmPanel.SetActive(false);

        dontAskToggle = new bool[DontAskRegion.GetValues(typeof(DontAskRegion)).Length];
        dontAskToggle[(int)DontAskRegion.none] = false; //default region that always asks for confirmation
    }

    public bool CheckDontAsk(DontAskRegion region)
    {
        return dontAskToggle[(int)region];
    }

    [ContextMenu("Test Confirmation")]
    public void Test()
    {
        UnityEvent testEvent = new UnityEvent();
        testEvent.AddListener(() => 
        {
            Debug.Log("Succsessfully Confirmed Your Test");
        });
        AskForConfirmation(testEvent);
    }

    public void AskForConfirmation(UnityEvent confirmAction,UnityEvent cancelAction = null, DontAskRegion region = DontAskRegion.none, string text = "Are you sure?")
    {
        currentRegion = region;
        if (dontAskToggle[(int)region])
        {
            confirmAction?.Invoke();
            return;
        }
        onConfirm = confirmAction;
        onCancel = cancelAction;
        _confirmText.text = text;
        _dontAskAgainToggle.gameObject.SetActive(region != DontAskRegion.none); //only show toggle if the region is not none
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
        onCancel?.Invoke();
    }

    public void ToggleDontAsk(bool value)
    {
        dontAskToggle[(int)currentRegion] = value;
    }
}

public enum  DontAskRegion
{
    none,
    Skilltree,
    Save,
    Load,
    DeleteSave
}