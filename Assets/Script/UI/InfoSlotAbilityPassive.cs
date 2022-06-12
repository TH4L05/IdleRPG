///<author>ThomasKrahl</author>

using UnityEngine;
using TMPro;

namespace IdleGame.UI.Ingame
{

    public class InfoSlotAbilityPassive : InfoSlotAbility
    {
        private PassiveAbility passiveAbility => abilityData as PassiveAbility;
    }
}
