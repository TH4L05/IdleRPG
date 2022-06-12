///<author>ThomasKrahl</author>

using IdleGame.Stats;
using UnityEngine;

namespace IdleGame.Unit.Stats
{
    public class EnemyStats : UnitStats
    {
        internal void SetStats(int playerLevel)
        {
            var randL = UnityEngine.Random.Range(-1, 2);
            var lvl = playerLevel + randL;
         
            if (lvl < 1) lvl = 1;

            level = lvl;
            if (lvl > 1)
            {
                AddModifier(StatType.STR, new StatModifier(ModifierType.Flat, lvl - 1), id);
                AddModifier(StatType.DEX, new StatModifier(ModifierType.Flat, lvl - 1), id);
                AddModifier(StatType.AGI, new StatModifier(ModifierType.Flat, lvl - 1), id);
                AddModifier(StatType.INT, new StatModifier(ModifierType.Flat, lvl - 1), id);
                AddModifier(StatType.VIT, new StatModifier(ModifierType.Flat, lvl - 1), id);
                AddModifier(StatType.LUK, new StatModifier(ModifierType.Flat, lvl - 1), id);
                AddModifier(StatType.DamageMin, new StatModifier(ModifierType.Flat, lvl - 1), id);
                AddModifier(StatType.DamageMax, new StatModifier(ModifierType.Flat, lvl - 1), id);
            }
        }
    }
}
