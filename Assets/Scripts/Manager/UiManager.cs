using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject flightPanel;
    public GameObject enemyStatPanel;
    public GameObject enemySpritePanel;
    public GameObject playerStatPanel;
    public GameObject playerSpritePanel;
    public GameObject blackscreen;

    public GameObject actionPanel;
    public GameObject attackButton;
    public GameObject itemButton;

    public GameObject attackMenuPanel;
    public GameObject attack1Button;
    public GameObject attack2Button;
    public GameObject attack3Button;
    public GameObject attack4Button;

    public GameObject itemMenuPanel;
    public GameObject item1Button;
    public GameObject item2Button;
    public GameObject item3Button;
    public GameObject item4Button;

    public void Awake()
    {
        blackscreen.SetActive(false);
        attackMenuPanel.SetActive(false);
        itemMenuPanel.SetActive(false);
    }
}
