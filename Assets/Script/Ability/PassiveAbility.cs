using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IdleGame.Stats;

namespace IdleGame
{
    [CreateAssetMenu(fileName = "newPassiveAbility", menuName = "IdleGame/Abilities/PassiveAbility")]
    public class PassiveAbility : Ability
    { 
        [SerializeField] private List<AbilityStatModifier> statModiefiers = new List<AbilityStatModifier>();

        public override void Use()
        {
            foreach (var statModifier in statModiefiers)
            {
                StatModifier mod = new StatModifier(statModifier.modifierType, statModifier.value);
                Game.Instance.PlayerStats.AddStatModifier(statModifier.statType, mod, Game.Instance.PlayerStats.ID);
            }
            IncreaseLevel(1);
        }

        public void IncreaseLevel(int amount)
        {
            level += amount;
        }

        public void ResetAbility()
        {
            for (int i = 0; i < level; i++)
            {
                foreach (var statModifier in statModiefiers)
                {
                    StatModifier mod = new StatModifier(statModifier.modifierType, statModifier.value);
                    Game.Instance.PlayerStats.RemoveStatModifier(statModifier.statType, mod, Game.Instance.PlayerStats.ID);
        }
            }

            
            level = 0;
        }
    }
}
