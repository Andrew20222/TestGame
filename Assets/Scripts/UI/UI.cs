using System;
using Player;
using Spawners;
using Stats;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI health;
        [SerializeField] private TextMeshProUGUI countDieEnemy;
        [SerializeField] private TextMeshProUGUI finalCountDieEnemy;
        [SerializeField] private CanvasGroup gameScreen;
        [SerializeField] private CanvasGroup gameOverScreen;
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button quitBtn;
        [SerializeField] private Score score;
        private Health.Health _playerHealth;
        private PlayerSpawner _playerSpawner;
        
        private readonly string _keySave = "Score";
        private readonly string _sceneName = "Game";

        private void Awake()
        {
            OnGameScreen();
            restartBtn.onClick.AddListener(Restart);
            quitBtn.onClick.AddListener(Quit);
        }

        public void Init(PlayerSpawner playerSpawner)
        {
            _playerSpawner = playerSpawner;
            _playerSpawner.OnSpawnPlayer += SetPlayer;
        }

        private void SetScreen(CanvasGroup canvasGroup, int alpha, bool interactable, bool blockRaycasts)
        {
            canvasGroup.alpha = alpha;
            canvasGroup.interactable = interactable;
            canvasGroup.blocksRaycasts = blockRaycasts;
        }

        private void OnGameScreen()
        {
            SetScreen(gameScreen,1,true,true);
            SetScreen(gameOverScreen,0,false,false);
        }
        private void OnGameOverScreen(GameObject player)
        {
            if (player.TryGetComponent(out Rotate rotate))
            {
                SetScreen(gameOverScreen,1,true,true);
                SetScreen(gameScreen,0,false,false);
                finalCountDieEnemy.text = $"Count Killed {PlayerPrefs.GetInt(_keySave).ToString()}";
            }
        }

        private void Restart()
        {
            SceneManager.LoadScene(_sceneName);
        }

        private void Quit()
        {
            Application.Quit();
        }

        private void SetPlayer(Rotate player)
        {
            if (player == null) return;
            _playerHealth = player.GetComponent<Health.Health>();
            _playerHealth.OnChangeHealth += SetHealth;
            _playerHealth.OnDie += OnGameOverScreen;
            score.OnChangeCountDieEnemy += SetCountDieEnemy;
        }

        private void OnDestroy()
        {
            _playerSpawner.OnSpawnPlayer -= SetPlayer;
            _playerHealth.OnChangeHealth -= SetHealth;
            score.OnChangeCountDieEnemy -= SetCountDieEnemy;
            _playerHealth.OnDie -= OnGameOverScreen;
            restartBtn.onClick.RemoveListener(Restart);
            quitBtn.onClick.RemoveListener(Quit);
        }

        private void SetHealth(float health) => this.health.text = health.ToString();
        private void SetCountDieEnemy(float countDieEnemy) => this.countDieEnemy.text =
            $"Count Killed {countDieEnemy.ToString()}";
    }
}
