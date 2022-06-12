///<author>ThomasKrahl</author>


using System.Collections.Generic;
using UnityEngine;

using IdleGame.Profile;

namespace IdleGame.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private List<ProfileSlot> profileSlots = new List<ProfileSlot>();

        private void Awake()
        {
            foreach (var slot in profileSlots)
            {
                slot.LoadProfile();
            }
        }

        public void QuitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

    }
}

