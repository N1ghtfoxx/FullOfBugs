using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using static UnityEngine.LowLevelPhysics2D.PhysicsLayers;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;

public class UiManager : MonoBehaviour
{
    // temp
    public GameObject fightManager;

    [Header("UiManager")]
    [SerializeField] private GameObject fightCanvas;
    [SerializeField] private GameObject fightPanel;
    private GameObject enemyStatPanel;
    private GameObject enemyNameText;
    private GameObject enemyVariantText;
    private GameObject enemyHealthSlider;
    private GameObject enemySpritePanel;
    private GameObject playerStatPanel;
    private GameObject playerNameText;
    private GameObject playerHealthSlider;
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

            fightPanel = fightCanvas.transform.Find("FightPanel").gameObject;
            enemyStatPanel = fightPanel.transform.Find("EnemyStatPanel").gameObject;
            enemyNameText = enemyStatPanel.transform.Find("EnemyNameText").gameObject;
            enemyVariantText = enemyStatPanel.transform.Find("EnemyVariantText").gameObject;
            enemyHealthSlider = enemyStatPanel.transform.Find("EnemyHealthSlider").gameObject;
            enemySpritePanel = fightPanel.transform.Find("EnemySpritePanel").gameObject;
            playerStatPanel = fightPanel.transform.Find("PlayerStatPanel").gameObject;
            playerNameText = playerStatPanel.transform.Find("PlayerNameText").gameObject;
            playerHealthSlider = playerStatPanel.transform.Find("PlayerHealthSlider").gameObject;
            playerSpritePanel = fightPanel.transform.Find("PlayerSpritePanel").gameObject;
            blackscreen = fightPanel.transform.Find("blackscreen").gameObject;

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

            fightManager.GetComponent<FightManager>().tempSetFightUI();
        }
    }

    public void OnClickSetWeapon(string weapon)
    {
        fightManager.GetComponent<FightManager>().SetWeapon(weapon);
    }

    public void SetFightUi(string playerName, int playerLevel, int playerHealth, string enemyName, string enemyVariant, int enemyHealth)
    {
        playerNameText.GetComponent<TextMeshProUGUI>().text = playerName;
        // fehlend: playerLevel #TO-DO Jo
        playerHealthSlider.GetComponent<Slider>().value = playerHealth;
        enemyNameText.GetComponent<TextMeshProUGUI>().text = enemyName.ToString();
        enemyVariantText.GetComponent<TextMeshProUGUI>().text = enemyVariant.ToString();
        enemyHealthSlider.GetComponent<Slider>().value = enemyHealth;
    }

    public void PlayerFightControl(bool playerTurn)
    {
        actionPanel.SetActive(playerTurn);
        if (!playerTurn)
            fightManager.GetComponent<FightManager>().Attack();
    }
}
