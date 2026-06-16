using UnityEngine;

public class Exit : HermbertEntered
{
    [SerializeField] GameObject _victoryScreen;

    protected override void Interaction()
    {
        base.Interaction();
        Debug.Log("Victory");
        _victoryScreen.SetActive(true);
    }
}
