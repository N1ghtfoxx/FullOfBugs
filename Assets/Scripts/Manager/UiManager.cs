using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private GameObject fightLostPanel;
    private GameObject fightLostText;
    private GameObject fightLostContinueButton;
    private GameObject fightWonPanel;
    private GameObject fightWonText;
    private GameObject fightWonContinueButton;

    public static UiManager instance;

    public void Awake()
    {
        instance = this;

        SearchForReferences();
        FightDisableAllUi();
    }

    private void FightDisableAllUi()
    {
        blackscreen.SetActive(false);
        attackMenuPanel.SetActive(false);
        itemMenuPanel.SetActive(false);
        fightLostPanel.SetActive(false);
        fightWonPanel.SetActive(false);
        fightLostContinueButton.SetActive(false);
        fightWonContinueButton.SetActive(false);
    }

    private void SearchForReferences()
    {
        // #TO-DO Jo: put this check into gamemanager maybe?
        if (SceneManager.GetActiveScene().name == "MainScene")    // change this later to FightScene (ask Naomi if we want a SceneChange tho)
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

            fightLostPanel = fightCanvas.transform.Find("FightLostPanel").gameObject;
            fightLostText = fightLostPanel.transform.Find("FightLostText").gameObject;
            fightLostContinueButton = fightLostPanel.transform.Find("ContinueButton").gameObject;
            fightWonPanel = fightCanvas.transform.Find("FightWonPanel").gameObject;
            fightWonText = fightWonPanel.transform.Find("FightWonText").gameObject;
            fightWonContinueButton = fightWonPanel.transform.Find("ContinueButton").gameObject;

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

    public void FightEnded(bool playerWon, string enemyNameText, int playerHealth)
    {
        if (playerWon)
            ShowFightWonScreen(playerWon, enemyNameText, playerHealth);
        else ShowFightLostScreen(playerWon, enemyNameText, playerHealth);

        StartCoroutine(FightManager.instance.Wait("beforeContinueAfterFight", 10));
    }

    public void ShowFightWonScreen(bool playerWon, string enemyNameText, int playerHealth)
    {
        fightWonText.GetComponent<TextMeshProUGUI>().text = "You won against " + enemyNameText + "with " + playerHealth.ToString() + " HP.";
        FightDisableAllUi();
        fightWonPanel.SetActive(true);
    }

    public void ShowFightLostScreen(bool playerWon, string enemyNameText, int playerHealth)
    {
        fightLostText.GetComponent<TextMeshProUGUI>().text = "You lost against " + enemyNameText + ".";
        FightDisableAllUi();
        fightLostPanel.SetActive(true);
    }

    public void UpdateHealthUi(int playerHealth, int enemyHealth)
    {
        playerHealthSlider.GetComponent<Slider>().value = playerHealth;
        enemyHealthSlider.GetComponent<Slider>().value = enemyHealth;
    }

    public void ShowContinueButtonAfterFight(bool playerWon)
    {
        if (playerWon)
            fightWonContinueButton.SetActive(true);
        else
            fightLostContinueButton.SetActive(true);
    }

    public void OnClickContinueAfterFight()
    {
        Debug.Log("Exit the FightScene.");
    }

}
