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

    // #TO-DO Jo: insert PlayerData etc later after first merge with Naomi
    public void Awake()
    {
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
        }
    }

    public void Attack()
    {
        Debug.Log("Attacking for " + attackModifier.ToString() + " damage");
        if (playerTurn)
        {
            int newEnemyHealth = enemyHealth - attackModifier;
            enemyHealth = newEnemyHealth;
            playerTurn = false;
            UiManager.GetComponent<UiManager>().PlayerFightControl(playerTurn);
            return;
        }
        else
        {
            Debug.Log("Enemy turn");
            playerHealth = playerHealth - 2;
            playerTurn = true;
            UiManager.GetComponent<UiManager>().PlayerFightControl(playerTurn);
            return;
        }

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
}
