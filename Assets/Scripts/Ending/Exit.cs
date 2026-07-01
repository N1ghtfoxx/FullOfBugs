using UnityEngine;

public class Exit : HermbertEntered
{
    [SerializeField] GameObject _victoryScreen;
    [SerializeField] GameObject _confetti;

    protected override void Interaction()
    {
        base.Interaction();
        Debug.Log("Victory");
        _victoryScreen.SetActive(true);
        _confetti.SetActive(true);
    }
}
