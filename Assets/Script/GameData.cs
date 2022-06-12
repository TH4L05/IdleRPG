///<author>ThomasKrahl</author>

using System.Collections.Generic;
using UnityEngine;

using IdleGame.Profile;
using IdleGame.Unit;
using IdleGame.Unit.Stats;

namespace IdleGame
{
    [CreateAssetMenu(fileName = "New GameData", menuName = "IdleGame/Data/GameData")]
    public class GameData : ScriptableObject
    {
         [SerializeField] private PlayerProfile activePlayeProfile;

        public bool SaveActiveProfile()
        {
            if (activePlayeProfile == null) return false;
            Serialization.Save(activePlayeProfile, activePlayeProfile.name + ".save");
            return true;
        }

        public bool UpdateProfileData(Player player, PlayerStats playerStats)
        {
            if (activePlayeProfile == null) return false;
            if(player == null) return false;
            if(playerStats == null) return false;

            activePlayeProfile.startedOnce = true;
            activePlayeProfile.currentHealth = player.CurrentHealth;
            activePlayeProfile.currentMana = player.CurrentMana;
            activePlayeProfile.currentSP = player.CurrentSP;
            activePlayeProfile.distance = player.Distance;
            activePlayeProfile.stats = playerStats.Stats;
            activePlayeProfile.level = playerStats.Level;
            activePlayeProfile.expToNextLevel = playerStats.ExpToNextLevel;
            activePlayeProfile.attributePoints = playerStats.AttributePoints;
            activePlayeProfile.rebirthPointsTotal = playerStats.RebirthPointsTotal;
            activePlayeProfile.rebirthPointsNextRebirthDistance = playerStats.RebirthPointsNextRebirthDistance;
            activePlayeProfile.rebirthPointsNextRebirthLevel = playerStats.RebirthPointsNextRebirthLevel;
            activePlayeProfile.ulimatePoints = playerStats.UlimatePoints;

            return true;
        }

        public Dictionary<string, object> GetProfileData()
        {
            Dictionary<string, object> profileData = new Dictionary<string, object>();
            profileData.Add("currentHealth", activePlayeProfile.currentHealth);
            profileData.Add("currentMana", activePlayeProfile.currentMana);
            profileData.Add("currentSP", activePlayeProfile.currentSP);
            profileData.Add("distance", activePlayeProfile.distance);
            profileData.Add("stats", activePlayeProfile.stats);
            profileData.Add("level", activePlayeProfile.level);
            profileData.Add("expToNextLevel", activePlayeProfile.expToNextLevel);
            profileData.Add("attributePoints", activePlayeProfile.attributePoints);
            profileData.Add("rebirthPointsTotal", activePlayeProfile.rebirthPointsTotal);
            profileData.Add("rebirthPointsNextRebirthDistance", activePlayeProfile.rebirthPointsNextRebirthDistance);
            profileData.Add("rebirthPointsNextRebirthLevel", activePlayeProfile.rebirthPointsNextRebirthLevel);
            profileData.Add("ulimatePoints", activePlayeProfile.ulimatePoints);
            
            return profileData;
        }


        public void SetActiveProfile(PlayerProfile profile)
        {
            activePlayeProfile = profile;
        }

        public void ResetActiveProfile()
        {
            activePlayeProfile = null;
        }

        public bool CheckProfile()
        {
            if (activePlayeProfile == null || string.IsNullOrEmpty(activePlayeProfile.name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool PlayedCheck()
        {
            return activePlayeProfile.startedOnce;
        }
    }
}

