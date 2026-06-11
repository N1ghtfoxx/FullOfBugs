using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;
using System.Net;

public class FightManager : MonoBehaviour
{
    public GameObject UiManager;

    // these are just temp, will be inserted from PlayerData later
    public string playerName;
    public int playerLevel;
    public Weapon playerWeapon;
    public int attackModifier;
    public int playerHealth;

    public string enemyName;
    public string enemyVariant;
    public int enemyHealth;

    // temp
    public bool playerTurn;
    public bool fightOver;
    public bool playerWon;

    public static FightManager instance;

    // #TO-DO Jo: insert PlayerData etc later after first merge with Naomi
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // temp disabled and moved so it doesnt have race issues
        //UiManager.GetComponent<UiManager>().SetFightUi(playerName, playerLevel, playerHealth, enemyName, enemyVariant, enemyHealthSlider);

        Coinflip();
        UiManager.GetComponent<UiManager>().PlayerFightControl(playerTurn);
    }

    public void SetWeapon(string weaponName)
    {
        playerWeapon = (Weapon)System.Enum.Parse(typeof(Weapon), weaponName);
        Debug.Log("SetWeapon: " + playerWeapon.ToString());

        switch (playerWeapon)
        {
            case Weapon.CheeseFork:
                attackModifier = 1;
                break;
            case Weapon.Slingshot:
                attackModifier = 2;
                break;
            case Weapon.MagicStuff:
                attackModifier = 3;
                break;
            case Weapon.ThrowPoopAndScream:
                attackModifier = 4;
                break;
            default:
                Debug.Log("Attack failed successfully. Sorry.");
                break;
        }
    }

    public void Attack()
    {
        Debug.Log("Attacking for " + attackModifier.ToString() + " damage");
        if (playerTurn)
        {
            int newEnemyHealth = enemyHealth - attackModifier;
            enemyHealth = newEnemyHealth;
            UiManager.GetComponent<UiManager>().UpdateHealthUi(playerHealth, enemyHealth);
            playerTurn = false;
            CheckWinCondition();
            return;
        }
        else
        {
            Debug.Log("Enemy turn");
            playerHealth = playerHealth - 2;
            UiManager.GetComponent<UiManager>().UpdateHealthUi(playerHealth, enemyHealth);
            playerTurn = true;
            CheckWinCondition();
            return;
        }

    }

    public void CheckWinCondition()
    {
        if (playerHealth <= 0 || enemyHealth <= 0)
            fightOver = true;

        if (!fightOver)
            UiManager.GetComponent<UiManager>().PlayerFightControl(playerTurn);
        else CheckWinner();
    }

    public void CheckWinner()
    {
        if (!fightOver)
            return;

        if (playerHealth <= 0)
        {
            UiManager.GetComponent<UiManager>().FightEnded(playerWon, enemyName, playerHealth);
            return;
        }
        if (enemyHealth <= 0)
        {
            UiManager.GetComponent<UiManager>().FightEnded(!playerWon, enemyName, playerHealth);
            return;
        }
    }

    public void ClearFightData()
    {
        playerName = "";
        playerHealth = 0;
        enemyName = "";
        enemyVariant = "";
        enemyHealth = 0;
        attackModifier = 0;
}


    // temp method
    public void tempSetFightUI()
    {
        UiManager.GetComponent<UiManager>().SetFightUi(playerName, playerLevel, playerHealth, enemyName, enemyVariant, enemyHealth);
    }

    // also only temp here
    public enum Weapon
    {
        CheeseFork,
        Slingshot,
        MagicStuff,
        ThrowPoopAndScream
    }

    // also only temp here
    public void Coinflip()
    {
        int rng = Random.Range(1, 2);

        if (rng == 1)
            playerTurn = true;
        else playerTurn = false;

        Debug.Log("playerTurn " + playerTurn);
    }

    // also only temp here


     public IEnumerator Wait(string beforeWhat, int howLong)
    {
        yield return new WaitForSeconds(howLong);

        switch (beforeWhat)
        {
            case "beforeContinueAfterFight":
                UiManager.GetComponent<UiManager>().ShowContinueButtonAfterFight(playerWon);
                ClearFightData();
                break;
            default:
                Debug.Log("Somehow, something got wrong. Sorry.");
                break;
        }
    }
}
