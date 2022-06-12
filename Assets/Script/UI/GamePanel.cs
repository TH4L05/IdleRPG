///<author>ThomasKrahl</author>

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IdleGame.UI
{
    public class GamePanel : MonoBehaviour
    {
        #region Fields

        [SerializeField][Multiline(5)] private string startText = "START TEXT";
        [SerializeField][Multiline(5)] private string deathText = "REBIRTH TEXT";
        [SerializeField] private TextMeshProUGUI panelTextField;
        [SerializeField] private Button buttonRebirth;
        [SerializeField] private Button buttonQuit;

        #endregion

        public void SetStartText()
        {
            if (panelTextField != null) panelTextField.text = startText;
            buttonRebirth.gameObject.SetActive(false);
            buttonQuit.gameObject.SetActive(false);
        }

        public void SetRebirthText()
        {
            var tempText = deathText.Split('#');
            var finaltext = string.Empty;

            for (int i = 0; i < tempText.Length; i++)
            {
                Debug.Log(tempText[i]);
                finaltext += tempText[i];
                if (i == 0)
                {
                    finaltext += Game.Instance.PlayerStats.RebirthPointsNextRebirthLevel;
                    continue;
                }

                if (i == 1)
                {
                    finaltext += Game.Instance.PlayerStats.RebirthPointsNextRebirthLevel;
                    continue;
                }
            }

            if (panelTextField != null) panelTextField.text = finaltext;
            buttonRebirth.gameObject.SetActive(true);
            buttonQuit.gameObject.SetActive(true);
        }

        public void RebirthButtonClicked()
        {
            Game.Instance.Rebirth();
        }

        public void QuitButtonClicked()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit(); 
            #endif
        }
    }
}

