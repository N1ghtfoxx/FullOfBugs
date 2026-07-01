using UnityEngine;

using Skilltree;

public class PlayerStatsManager : Singleton<PlayerStatsManager>
{
    public int maxHp { get; private set; } = 5;
    public int hp { get; private set; } = 5;

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
    }

    public void IncreaseMaxHp(int amount)
    {
        maxHp += amount;
        hp += amount;
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
