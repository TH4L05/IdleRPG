///<author>ThomasKrahl</author>

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IdleGame.Profile
{
    public class ProfileSlot : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameData gameData;
        [SerializeField] private LoadScene loadScene;
        [SerializeField] private string slotName = "ProfileSlot";
        [SerializeField] private PlayerProfile playerProfile;
        [SerializeField] private Button buttonCreate;
        [SerializeField] private Button buttonDelete;
        [SerializeField] private Button buttonPlay;
        private bool created = false;

        #endregion

        #region UnityFunctions

        private void Awake()
        {
            gameData.ResetActiveProfile();
            SetButtonVisbility(false, false, true);
        }

        #endregion

        public void CreateProfile()
        {
            string name = slotName;
            playerProfile = new PlayerProfile(slotName);
            Save();
            created = true;
            buttonPlay.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = slotName;
            SetButtonVisbility(true, true, false);
            Debug.Log($"<color=lightblue>Profile created</color>");
        }

        public void LoadProfile()
        {
            var file = slotName + ".save";

            if (Serialization.FileExistenceCheck(file))
            {
                playerProfile = new PlayerProfile();
                playerProfile = Load();
                created = true;
                buttonPlay.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = slotName;
                SetButtonVisbility(true, true, false);
                Debug.Log($"<color=lightblue>Profile loaded</color>");
            }
        }

        public void DeleteProfile()
        {
            Serialization.DeleteFile(slotName + ".save");
            SetButtonVisbility(false, false, true);
            Debug.Log($"<color=lightblue>Profile deleted</color>");
        }

        public void StartGame()
        {
            gameData.SetActiveProfile(playerProfile);
            loadScene.LoadSpecificScene(1);
        }

        private PlayerProfile Load()
        {
            return (PlayerProfile)Serialization.Load(slotName + ".save");
        }

        private void Save()
        {
            Serialization.Save(playerProfile, slotName + ".save");
        }

        private void SetButtonVisbility(bool deleteButtonActive, bool startButtonActive, bool createButtonActive)
        {
            buttonDelete.gameObject.SetActive(deleteButtonActive);
            buttonPlay.gameObject.SetActive(startButtonActive);
            buttonCreate.gameObject.SetActive(createButtonActive);
        }
    }
}

