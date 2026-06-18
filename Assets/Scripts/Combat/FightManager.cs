using UnityEngine;
using System.Collections;

using Skilltree;
/*
 To start a fight you need an Enemy yourEnemyClass.
Call: Fightmanager.instance.StartFight(yourEnemyClass);
 */
public class FightManager : Singleton<FightManager>
{
    [SerializeField] Enemy _enemy;

    private bool _playerTurn;
    private bool _fightOver;
    private bool _playerWon;

    public void UseItem(string itemName)
    {
        switch (itemName)
        {
            case "HealingPotion":
                //Chick Inventory and use() one
                //update ui;
                    break;
        }
        _playerTurn = false;
        Attack();
    }

    public void SetWeapon(string weaponName)
    {
        Weapon weapon = (Weapon)System.Enum.Parse(typeof(Weapon), weaponName);
        int dmg = 0;

        switch (weapon)
        {
            case Weapon.CheeseFork:
                dmg = 1;
                break;
            case Weapon.Slingshot:
                dmg = 2;
                //To-Do: Check for Ammunition and remove or cancle
                break;
            case Weapon.MagicStuff:
                dmg = 3;
                break;
            case Weapon.ThrowPoopAndScream:
                dmg = 4;
                break;
            default:
                Debug.Log("Attack failed successfully. Sorry.");
                break;
        }

        if (SkillManager.instance.HasSkill(SkillID.StrongAttack))
        {
            dmg += 1;
            if (SkillManager.instance.HasSkill(SkillID.HeavyAttack))
            {
                dmg += 1;
                if (SkillManager.instance.HasSkill(SkillID.NuklearBomb))
                    dmg += 2;
            }
        }

        Attack(dmg);
    }

    public void Attack(int dmg = 0)
    {
        if (_playerTurn)
        {
            Debug.Log("Attacking for " + dmg.ToString() + " damage");
            _enemy.hp -= dmg;
            UiManager.instance.UpdateHealthUi(PlayerStatsManager.instance.hp, _enemy.hp);
            _playerTurn = false;
            EndAttack();
        }
        else
        {
            StartCoroutine(EnemyAttackRoutine());
        }
    }

    private void EndAttack()
    {
        CheckWinCondition();
        if (!_playerTurn && !_fightOver)
            Attack();
    }

    private IEnumerator EnemyAttackRoutine()
    {
        Debug.Log("Enemy turn");
        yield return new WaitForSeconds(1f);
        Debug.Log("Enemy atacks");
        yield return new WaitForSeconds(0.5f);
        PlayerStatsManager.instance.modifyHp(-_enemy.dmg);
        UiManager.instance.UpdateHealthUi(PlayerStatsManager.instance.hp, _enemy.hp);
        yield return new WaitForSeconds(0.5f);
        _playerTurn = true;
        EndAttack();
    }

    public void CheckWinCondition()
    {
        if (PlayerStatsManager.instance.hp <= 0 || _enemy.hp <= 0)
            _fightOver = true;

        if (!_fightOver)
            UiManager.instance.PlayerFightControl(_playerTurn);
        else CheckWinner();
    }

    public void CheckWinner()
    {
        if (!_fightOver)
            return;
        _playerWon = PlayerStatsManager.instance.hp > 0;
        UiManager.instance.FightEnded(_playerWon, _enemy.name, PlayerStatsManager.instance.hp);
    }

    public void ClearFightData()
    {
        _fightOver = false;
    }


    public void StartFight(Enemy e)
    {
        _enemy = e;
        UiManager.instance.SetFightUi("Hermbert", PlayerStatsManager.instance.maxHp, PlayerStatsManager.instance.hp, _enemy.name, _enemy.variant, _enemy.hp, _enemy.sprite);
        Coinflip();
        UiManager.instance.PlayerFightControl(_playerTurn);
        if(!_playerTurn)
            Attack();
    }

    // temp method
    [ContextMenu("StartFight")]
    public void tempSetFightUI()
    {
        StartFight(_enemy);
    }

    public void Coinflip()
    {
        int rng = Random.Range(1, 3);

        if (rng == 1)
            _playerTurn = true;
        else _playerTurn = false;
        string text = _playerTurn ? "you beginn" : _enemy.name + " beginns";
        Debug.Log($"Battle Started {text}!");
    }

     public IEnumerator Wait(string beforeWhat, int howLong)
     {
        yield return new WaitForSeconds(howLong);

        switch (beforeWhat)
        {
            case "beforeContinueAfterFight":
                UiManager.instance.ShowContinueButtonAfterFight(_playerWon);
                ClearFightData();
                break;
            default:
                Debug.Log("Somehow, something got wrong. Sorry.");
                break;
        }
     }

    public enum Weapon
    {
        CheeseFork,
        Slingshot,
        MagicStuff,
        ThrowPoopAndScream
    }
}


[System.Serializable]
public class Enemy
{
    public string name;
    public string variant;
    public int hp;
    public int dmg;
    public Sprite sprite;
}