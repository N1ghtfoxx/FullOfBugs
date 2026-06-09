using UnityEngine;

public class ConfimationPanelFunctions : MonoBehaviour
{
    public void Confirm()
    {
        ConfirmManager.instance.Confirm();
    }

    public void Cancel()
    {
        ConfirmManager.instance.Cancel();
    }

    public void ToggleDontAsk(bool value)
    {
        ConfirmManager.instance.ToggleDontAsk(value);
    }

}
