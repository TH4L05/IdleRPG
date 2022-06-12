///<author>ThomasKrahl</author>

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IdleGame.UI.Ingame
{
    public class ButtonBar : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<GameObject> panels = new List<GameObject>();

        #endregion

        #region UnityFunctions

        private void Awake()
        {
            DisableAllPanels();
            ActivatePanel(0);

            var button = transform.GetChild(0).GetComponent<Button>();
            button.Select();
        }

        #endregion

        #region Panels

        public void DisableAllPanels()
        {
            foreach (var panels in panels)
            {
                panels.SetActive(false);
            }
        }

        public void DisablePanel(int index)
        {
            panels[index].SetActive(false);
        }

        public void ActivatePanel(int index)
        {
            panels[index].SetActive(true);
        }

        #endregion
    }
}

