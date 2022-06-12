

using System;
using System.Collections.Generic;
using UnityEngine;
using IdleGame.Stats;

namespace IdleGame.Profile
{
    [Serializable]
    public class PlayerProfile
    {
        #region Fields

        public string name = "";
        public bool startedOnce = false;

        public float currentHealth;
        public float currentMana;
        public float currentSP;
        public int distance;

        public List<Stat> stats = new List<Stat>();
        public int level;
        public int expToNextLevel;

        public int currentExp;
        public int attributePoints;
        public int rebirthPointsTotal;
        public int rebirthPointsNextRebirthDistance;
        public int rebirthPointsNextRebirthLevel;
        public int ulimatePoints;

        #endregion

        public PlayerProfile()
        {
        }

        public PlayerProfile(string name)
        {
            this.name = name;
        }

    }
}

