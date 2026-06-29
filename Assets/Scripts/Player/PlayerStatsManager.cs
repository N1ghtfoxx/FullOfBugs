using UnityEngine;

using Skilltree;
using UnityEngine.UI;

public class PlayerStatsManager : Singleton<PlayerStatsManager>
{
    [SerializeField] Slider _hudHp;

    public int maxHp { get; private set; } = 5;
    public int hp { get; private set; } = 5;

    private void Start()
    {
        UpdateHud();
    }

    private void UpdateHud()
    {
        _hudHp.maxValue = maxHp;
        _hudHp.value = hp;
    }

    public void modifyHp(int amount)
    {
        hp += amount;
        if (hp <= 0)
        {
            Debug.Log("You Died");
        }
        else if(hp > maxHp)
        {
            hp = maxHp;
        }
        UpdateHud();
    }

    public void IncreaseMaxHp(int amount)
    {
        maxHp += amount;
        hp += amount;
        UpdateHud();
    }

    public void SetStats()
    {
        maxHp = 5;
        if (SkillManager.instance.HasSkill(SkillID.Tank))
        {
            maxHp += 1;
            if (SkillManager.instance.HasSkill(SkillID.Giant))
            {
                maxHp += 2;
                if (SkillManager.instance.HasSkill(SkillID.Colossus))
                    maxHp += 4;
            }
        }
    }
}
