using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdleGame
{
    public class Ability : ScriptableObject
    {
        [SerializeField] protected string abilityName;
        [SerializeField] [Multiline(5)] protected string tooltip;
        [SerializeField] protected int level = 0;
        public int Level => level;

        public virtual void Use()
        {
            
        }
    }
}

