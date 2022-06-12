///<author>ThomasKrahl</author>

using System.Collections;
using UnityEngine;
using TMPro;

using IdleGame.Unit;
using IdleGame.Unit.Stats;
using IdleGame.UI;

namespace IdleGame
{
    public class Game : MonoBehaviour
    {
        #region SerializedFields

        [SerializeField] private GameObject floatingTextTemplate;
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameData gameData;
        [SerializeField] [Range(30.0f, 600.0f)] private float autoSaveTime = 120f;
        [SerializeField] private GameObject playerObject;
        [SerializeField] private Player player;
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private EnemyHandler enemyHandler;
        [SerializeField] private PropSpawner propSpawner;
        [SerializeField] private Animator ground;
        [SerializeField] private UnitState playerState;
        [SerializeField] private TextMeshProUGUI fpsTextField;
        [SerializeField] private TextMeshProUGUI infoTextField;

        #endregion

        #region PrivateFields

        private float updateTime;
        private float dt;

        #endregion

        #region PublicFields

        public static Game Instance;
        public Player Player => player;
        public PlayerStats PlayerStats => playerStats;
        public UnitState PlayerState => playerState;

        #endregion

        #region UnityFunctions

        private void Awake()
        {
            Instance = this;
            player = playerObject.GetComponent<Player>();
            playerStats = playerObject.GetComponent<PlayerStats>();
            Player.PlayerStateChanged += UpdatePlayerState;
            Player.PlayerDied += PlayerDied;

            gamePanel.GetComponent<GamePanel>().SetStartText();
            gamePanel.SetActive(true);

            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            if (infoTextField != null) infoTextField.gameObject.SetActive(false);
            if (gameData.CheckProfile() == false) return;

            if (gameData.PlayedCheck())
            {
                Load();
            }
            else
            {
                Save();
            }
        }

        private void Update()
        {
            DevKeys();
            DisplayFPS();
        }

        private void FixedUpdate()
        {
            updateTime += Time.fixedDeltaTime;
            if (updateTime >= autoSaveTime)
            {
                updateTime = 0;
                Save();
            }
        }

        private void OnDestroy()
        {
            Player.PlayerStateChanged -= UpdatePlayerState;
            Player.PlayerDied -= PlayerDied;
            Save();
            gameData.ResetActiveProfile();
        }

        #endregion

        #region Player

        private void UpdatePlayerState(UnitState state)
        {
            gamePanel.SetActive(false);
            playerState = state;
            enemyHandler.SetPlayerStateToEnemies(playerState);
            propSpawner.SetPlayerStateToProps(playerState);
        }

        private void PlayerDied()
        {
            enemyHandler.SetPlayerStateToEnemies(UnitState.Resting);
            gamePanel.GetComponent<GamePanel>().SetRebirthText();
            gamePanel.SetActive(true);
        }

        public void Rebirth()
        {
            enemyHandler.RemoveAllActiveEnemies();
            player.Rebirth();
        }

        #endregion

        #region Save/Load

        private void Save()
        {
            if (gameData.CheckProfile() == false) return;

            bool success = gameData.UpdateProfileData(player, playerStats);
            if (success) Debug.Log($"<color=magenta>Profile Updated</color>");

            success = gameData.SaveActiveProfile();
            if(success) Debug.Log($"<color=magenta>Game Saved</color>");

            StartCoroutine(DisplayInfoText("GameSaved!"));
        }

        private void Load()
        {
            if (gameData.CheckProfile() == false) return;

            var profileData = gameData.GetProfileData();
            player.SetData(profileData);
            playerStats.SetData(profileData);
        }

        #endregion

        public void GroundAnim(bool play, float movementSpeed)
        {
            if (ground == null) return;

            if (play)
            {
                ground.speed = movementSpeed;
                ground.Play("ParallaxGround");
            }
            else
            {
                ground.Play("Idle");
            }
        }

        public void InstaniateFloatingText(Vector3 spawnPosition, string text)
        {
            if (floatingTextTemplate == null) return;
            var floatingText = Instantiate(floatingTextTemplate, spawnPosition, Quaternion.identity);
            floatingText.GetComponent<FloatingText>().SetText(text);
        }

        public void DisplayFPS()
        {
            if(fpsTextField == null) return;

            dt += (Time.deltaTime - dt) * 0.1f;
            float frames = 1.0f / dt;
            frames = Mathf.Clamp(frames, 0.0f, 999f);
            fpsTextField.text = "FPS: " + Mathf.Ceil(frames).ToString();
        }

        IEnumerator DisplayInfoText(string text)
        {
            if (infoTextField != null)
            {
                infoTextField.text = text;
                infoTextField.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(5f);
            infoTextField.gameObject.SetActive(false);
        }

        #region Dev

        private void DevKeys()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                playerStats.AddExperience(7);
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                player.TakeDamage(5f);
            }
        }

        #endregion
    }
}

