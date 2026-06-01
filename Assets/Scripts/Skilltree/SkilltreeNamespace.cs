using System.Collections.Generic;

namespace Skilltree
{

    [System.Serializable]
    public class Skill
    {
        public SkillID skillID;
        public string skillName;
        public string description;
        public List<SkillID> prerequisites = new List<SkillID>();
        public Cost cost;

    }

    public enum SkillID
    {
        none,
        HealingPotion,
        StrongHealingPotion,
        GlowPotion,
        LongGlowPotion,
        StrongAttack,
        HeavyAttack,
        NuklearBomb,
        Tank,
        Giant,
        Colossus,
        Garden,
        Farm,
        Fertilizer,
        PowerFertilizer,
        GreenThumb,
        PlantMaster,
    }

    [System.Serializable]
    public class Cost
    {
        public ShadowType costType;
        public int amount;
    }
    public enum ShadowType
    {
        Small,
        Medium,
        Large
    }
}