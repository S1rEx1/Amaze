using UnityEngine;
using UnityEngine.SceneManagement;

namespace Amaze
{
    public class PlayerDataManager : MonoBehaviour
    {
        [Header("Настройки сохранения")]
        public bool savePosition = true; // Сохранять позицию игрока
        public bool saveHealth = true; // Сохранять здоровье
        public bool saveInventory = true; // Сохранять инвентарь
        
        [Header("Настройки восстановления")]
        public Vector3 defaultSpawnPosition = Vector3.zero; // Позиция по умолчанию
        public bool useCheckpoint = false; // Использовать чекпоинты
        
        // Статические данные для передачи между сценами
        public static Vector3 savedPlayerPosition;
        public static float savedPlayerHealth = 100f;
        public static bool hasSavedData = false;
        
        private PlayerMovement playerMovement;
        private Transform playerTransform;
        
        private void Awake()
        {
            // Делаем объект неуничтожаемым при смене сцен
            DontDestroyOnLoad(gameObject);
            
            // Подписываемся на событие загрузки сцены
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        private void Start()
        {
            // Ищем игрока в текущей сцене
            FindPlayer();
        }
        
        private void FindPlayer()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
                playerMovement = player.GetComponent<PlayerMovement>();
                
                // Восстанавливаем данные игрока, если они есть
                if (hasSavedData && savePosition)
                {
                    RestorePlayerPosition();
                }
            }
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log($"PlayerDataManager: Загружена сцена '{scene.name}'");
            
            // Ищем игрока в новой сцене
            FindPlayer();
        }
        
        public void SavePlayerData()
        {
            if (playerTransform != null)
            {
                savedPlayerPosition = playerTransform.position;
                hasSavedData = true;
                Debug.Log($"PlayerDataManager: Сохранена позиция игрока: {savedPlayerPosition}");
            }
        }
        
        public void RestorePlayerPosition()
        {
            if (playerTransform != null && hasSavedData)
            {
                playerTransform.position = savedPlayerPosition;
                Debug.Log($"PlayerDataManager: Восстановлена позиция игрока: {savedPlayerPosition}");
            }
        }

        public void SetSpawnPosition(Vector3 position)
        {
            savedPlayerPosition = position;
            hasSavedData = true;
            Debug.Log($"PlayerDataManager: Установлена новая позиция спавна: {position}");
        }
        
        public void ResetPlayerData()
        {
            hasSavedData = false;
            savedPlayerPosition = defaultSpawnPosition;
            Debug.Log("PlayerDataManager: Данные игрока сброшены");
        }
        
        private void OnDestroy()
        {
            // Отписываемся от события
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        
        // Методы для вызова из других скриптов
        public static void SaveCurrentPlayerData()
        {
            PlayerDataManager instance = FindFirstObjectByType<PlayerDataManager>();
            if (instance != null)
            {
                instance.SavePlayerData();
            }
        }
        
        public static void SetCheckpoint(Vector3 position)
        {
            PlayerDataManager instance = FindFirstObjectByType<PlayerDataManager>();
            if (instance != null)
            {
                instance.SetSpawnPosition(position);
            }
        }
    }
}
