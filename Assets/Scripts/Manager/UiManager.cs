using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject fightCanvas;

    [SerializeField] private GameObject flightPanel;
    private GameObject enemyStatPanel;
    private GameObject enemySpritePanel;
    private GameObject playerStatPanel;
    private GameObject playerSpritePanel;
    private GameObject blackscreen;

    [SerializeField] private GameObject actionPanel;
    private GameObject attackButton;
    private GameObject itemButton;

    [SerializeField] private GameObject attackMenuPanel;
    private GameObject attack1Button;
    private GameObject attack2Button;
    private GameObject attack3Button;
    private GameObject attack4Button;

    [SerializeField] private GameObject itemMenuPanel;
    private GameObject item1Button;
    private GameObject item2Button;
    private GameObject item3Button;
    private GameObject item4Button;

    private bool inFightScene;

    public void Awake()
    {
        SearchForReferences();
        DisableAll();
    }

    private void DisableAll()
    {
        blackscreen.SetActive(false);
        attackMenuPanel.SetActive(false);
        itemMenuPanel.SetActive(false);
    }

    private void SearchForReferences()
    {
        // #TO-DO Jo: put this check into gamemanager maybe?
        if (SceneManager.GetActiveScene().name == "Jo - FightScene")
            inFightScene = true;

        if (inFightScene)
        {
            if (fightCanvas == null)
                transform.Find("FightCanvas");

            flightPanel = fightCanvas.transform.Find("FightPanel").gameObject;
            enemyStatPanel = flightPanel.transform.Find("EnemyStatPanel").gameObject;
            enemySpritePanel = flightPanel.transform.Find("EnemySpritePanel").gameObject;
            playerStatPanel = flightPanel.transform.Find("PlayerStatPanel").gameObject;
            playerSpritePanel = flightPanel.transform.Find("PlayerSpritePanel").gameObject;
            blackscreen = flightPanel.transform.Find("blackscreen").gameObject;

            actionPanel = fightCanvas.transform.Find("ActionPanel").gameObject;
            attackButton = actionPanel.transform.Find("AttackButton").gameObject;
            itemButton = actionPanel.transform.Find("ItemButton").gameObject;

            attackMenuPanel = fightCanvas.transform.Find("AttackMenuPanel").gameObject;
            attack1Button = attackMenuPanel.transform.Find("Attack1Button").gameObject;
            attack2Button = attackMenuPanel.transform.Find("Attack2Button").gameObject;
            attack3Button = attackMenuPanel.transform.Find("Attack3Button").gameObject;
            attack4Button = attackMenuPanel.transform.Find("Attack4Button").gameObject;

            itemMenuPanel = fightCanvas.transform.Find("ItemMenuPanel").gameObject;
            item1Button = itemMenuPanel.transform.Find("Item1Button").gameObject;
            item2Button = itemMenuPanel.transform.Find("Item2Button").gameObject;
            item3Button = itemMenuPanel.transform.Find("Item3Button").gameObject;
            item4Button = itemMenuPanel.transform.Find("Item4Button").gameObject;
        }
    }
}
