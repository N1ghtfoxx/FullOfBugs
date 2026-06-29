using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{
    [Header("General")]
    [SerializeField] private GameObject playerNameText;
    [SerializeField] private GameObject playerLevelText;
    [SerializeField] private GameObject playerHealthSlider;

    [SerializeField] private GameObject devPanel;
    [SerializeField] private GameObject devButton1;
    [SerializeField] private GameObject devButton2;
    [SerializeField] private GameObject devButton3;

    [Header("GameScene")]
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject playerPanel;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseText;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject saveButton;
    [SerializeField] private GameObject loadButton;
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private GameObject messageText;
    [SerializeField] private GameObject messageButtonPanel;
    [SerializeField] private GameObject yesButton;
    [SerializeField] private GameObject noButton;
    [SerializeField] private bool inGameScene;

    [Header("FightScene")]
    [SerializeField] GameObject _fightPanel;
    [SerializeField] TMP_Text _enemyNameText;
    [SerializeField] TMP_Text _enemyVariantText;
    [SerializeField] Slider _enemyHealthSlider;
    [SerializeField] Image _enemyImage;
    [SerializeField] TMP_Text _playerNameText;
    [SerializeField] Slider _playerHealthSlider;
    [SerializeField] Image _playerImage;
    [SerializeField] GameObject _actionButtons;
    [SerializeField] GameObject _fightWonContinueButton;
    [SerializeField] GameObject _fightLostContinueButton;
    [SerializeField] GameObject _fightWonScreen;
    [SerializeField] GameObject _fightLostScreen;
    [SerializeField] GameObject _blackscreen;
    [SerializeField] TMP_Text _fightWonText;
    [SerializeField] TMP_Text _fightLostText;

    public bool inFightScene;


    #region init

    protected override void Awake()
    {
        base.Awake();

        SearchForReferences();
        
        if (inFightScene)
            FightDisableAllUi();

        if (inGameScene)
            GameDisableUi();
    }

    /// <summary>
    /// depending on the activeScene
    /// </summary>
    private void SearchForReferences()
    {
        CheckActiveScene();
        //if (inGameScene)
        //{
        //    if (uiCanvas == null)
        //        transform.Find("UiCanvas");

        //    playerPanel = uiCanvas.transform.Find("PlayerPanel").gameObject;
        //    playerSprite = playerPanel.transform.Find("PlayerSprite").gameObject;
        //    playerNameText = playerPanel.transform.Find("PlayerNameText").gameObject;
        //    playerLevelText = playerPanel.transform.Find("PlayerLevelText").gameObject;
        //    playerHealthSlider = playerPanel.transform.Find("PlayerHealthSlider").gameObject;

        //    pausePanel = uiCanvas.transform.Find("PausePanel").gameObject;
        //    pauseText = pausePanel.transform.Find("PauseText").gameObject;
        //    continueButton = pausePanel.transform.Find("ContinueButton").gameObject;
        //    loadButton = pausePanel.transform.Find("LoadButton").gameObject;
        //    optionsButton = pausePanel.transform.Find("OptionsButton").gameObject;
        //    exitButton = pausePanel.transform.Find("ExitButton").gameObject;
        //    optionsPanel = uiCanvas.transform.Find("OptionsPanel").gameObject;

        //    messagePanel = uiCanvas.transform.Find("MessagePanel").gameObject;
        //    messageText = messagePanel.transform.Find("MessageText").gameObject;
        //    messageButtonPanel = messagePanel.transform.Find("MessageButtonPanel").gameObject;
        //    yesButton = messageButtonPanel.transform.Find("YesButton").gameObject;
        //    noButton = messageButtonPanel.transform.Find("NoButton").gameObject;

        //    devPanel = uiCanvas.transform.Find("DevPanel").gameObject;
        //    devButton1 = devPanel.transform.Find("DevButton1").gameObject;
        //    devButton2 = devPanel.transform.Find("DevButton2").gameObject;
        //    devButton3 = devPanel.transform.Find("DevButton3").gameObject;
        //}
    }

    // #TO-DO Jo: put this check into gamemanager maybe?
    private void CheckActiveScene()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        Debug.Log("You're in " +  activeScene);

        switch (activeScene)
        {
            case "TitleScene":
                break;
            case "FightScene":
                inFightScene = true;
                inGameScene = false;
                break;
            case "MainScene":
                inFightScene = false;
                inGameScene = true;
                break;
            case "Jo - Ui":
                inFightScene = false;
                inGameScene = true;
                break;
            default:
                Debug.LogError("No active Scene found.");
                break;
        }
    }

    #endregion

    #region GameScene

    public void SetGameUI(string playerName, int playerLevel, int playerHealth, string enemyName, string enemyVariant, int enemyHealth)
    {
        playerNameText.GetComponent<TextMeshProUGUI>().text = playerName;
        playerLevelText.GetComponent<TextMeshProUGUI>().text = playerLevel.ToString();
        playerHealthSlider.GetComponent<Slider>().value = playerHealth;
    }

    public void GameDisableUi()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        messagePanel.SetActive(false);
    }

    #endregion

    #region FightScene

    /// <summary>
    /// is collecting and setting the PlayerData
    /// </summary>
    /// <param name="playerName"></param>
    /// <param name="playerLevel"></param>
    /// <param name="playerHealth"></param>
    /// <param name="enemyName"></param>
    /// <param name="enemyVariant"></param>
    /// <param name="enemyHealth"></param>
    public void SetFightUi(string playerName, int playerMaxHealth, int playerHealth, string enemyName, string enemyVariant, int enemyHealth, Sprite enemySprite)
    {
        _fightPanel.SetActive(true);
        playerNameText.GetComponent<TextMeshProUGUI>().text = playerName;
        _playerHealthSlider.GetComponent<Slider>().maxValue = playerMaxHealth;
        _playerHealthSlider.GetComponent<Slider>().value = playerHealth;
        _enemyNameText.GetComponent<TextMeshProUGUI>().text = enemyName.ToString();
        _enemyVariantText.GetComponent<TextMeshProUGUI>().text = enemyVariant.ToString();
        _enemyHealthSlider.GetComponent<Slider>().maxValue = enemyHealth;
        _enemyHealthSlider.GetComponent<Slider>().value = enemyHealth;
        _enemyImage.sprite = enemySprite;
    }

    /// <summary>
    /// is checking which turn is it and acts accordingly
    /// </summary>
    /// <param name="playerTurn"></param> true if it's the players turn
    public void PlayerFightControl(bool playerTurn)
        {
            _actionButtons.SetActive(playerTurn);
            //if (!_playerTurn)
                //fightManager.GetComponent<FightManager>().Attack();
        }

    public void FightEnded(bool playerWon, string enemyNameText, int playerHealth)
    {
        if (playerWon)
            ShowFightWonScreen(enemyNameText, playerHealth);
        else ShowFightLostScreen(enemyNameText);

        StartCoroutine(FightManager.instance.Wait("beforeContinueAfterFight", 3));
    }

    public void ShowContinueButtonAfterFight(bool playerWon)
    {
        if (playerWon)
            _fightWonContinueButton.SetActive(true);
        else
            _fightLostContinueButton.SetActive(true);
    }

    public void UpdateHealthUi(int playerHealth, int enemyHealth)
    {
        if(_playerHealthSlider.value != playerHealth)
            _playerHealthSlider.value = playerHealth;
        if(_enemyHealthSlider.value != enemyHealth)
            _enemyHealthSlider.value = enemyHealth;
    }


    // actually, #TO-DO Jo: This can be one method
    public void ShowFightWonScreen(string enemyNameText, int playerHealth)
    {
        _fightWonText.text = "You won against " + enemyNameText + " with " + playerHealth.ToString() + " HP.";
        FightDisableAllUi();
        _fightWonScreen.SetActive(true);
    }

    public void ShowFightLostScreen(string enemyNameText)
    {
        _fightLostText.text = "You lost against " + enemyNameText + ".";
        FightDisableAllUi();
        _fightLostScreen.SetActive(true);
    }

    private void FightDisableAllUi()
    {
        _blackscreen.SetActive(false);
        //_attackMenuPanel.SetActive(false);
        //_itemMenuPanel.SetActive(false);
        _fightLostScreen.SetActive(false);
        _fightWonScreen.SetActive(false);
        _fightLostContinueButton.SetActive(false);
        _fightWonContinueButton.SetActive(false);
    }

    #endregion

    #region OnClickFight

    public void OnClickSetWeapon(string weapon)
    {
        FightManager.instance.SetWeapon(weapon);
    }   

    public void OnClickContinueAfterFight()
    {
        Debug.Log("Exit the FightScene.");
    }

    public void OnClickContinueAfterLose()
    {
        SceneLoadingManager.Instance.LoadScene("StartScreen");
    }

    #endregion
}
