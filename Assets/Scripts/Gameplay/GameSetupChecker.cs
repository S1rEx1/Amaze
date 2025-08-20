using UnityEngine;

namespace Amaze
{
    public class GameSetupChecker : MonoBehaviour
    {
        [Header("Проверка настройки")]
        public bool checkPlayerTag = true;
        public bool checkPlayerCollider = true;
        public bool checkSceneLoader = true;
        
        void Start()
        {
            Debug.Log("=== ПРОВЕРКА НАСТРОЙКИ ИГРЫ ===");
            
            if (checkPlayerTag)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    Debug.Log($"✓ Игрок найден: {player.name}");
                    
                    // Проверяем компоненты игрока
                    var playerMovement = player.GetComponent<PlayerMovement>();
                    var rigidbody = player.GetComponent<Rigidbody2D>();
                    var collider = player.GetComponent<Collider2D>();
                    
                    if (playerMovement != null) Debug.Log("✓ PlayerMovement найден");
                    else Debug.LogError("✗ PlayerMovement НЕ найден!");
                    
                    if (rigidbody != null) Debug.Log("✓ Rigidbody2D найден");
                    else Debug.LogError("✗ Rigidbody2D НЕ найден!");
                    
                    if (collider != null) Debug.Log("✓ Collider2D найден");
                    else Debug.LogError("✗ Collider2D НЕ найден!");
                }
                else
                {
                    Debug.LogError("✗ Игрок с тегом 'Player' НЕ найден!");
                }
            }
            
            if (checkSceneLoader)
            {
                var sceneLoaders = FindObjectsByType<SceneLoader>(FindObjectsSortMode.None);
                if (sceneLoaders.Length > 0)
                {
                    Debug.Log($"✓ Найдено SceneLoader'ов: {sceneLoaders.Length}");
                    foreach (var loader in sceneLoaders)
                    {
                        if (!string.IsNullOrEmpty(loader.sceneNameToLoad))
                        {
                            Debug.Log($"  - {loader.name}: сцена '{loader.sceneNameToLoad}'");
                        }
                        else
                        {
                            Debug.LogError($"  - {loader.name}: имя сцены НЕ указано!");
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("⚠ SceneLoader'ы не найдены");
                }
            }
            
            if (checkPlayerCollider)
            {
                var cameras = FindObjectsByType<Camera>(FindObjectsSortMode.None);
                if (cameras.Length > 0)
                {
                    Debug.Log($"✓ Найдено камер: {cameras.Length}");
                    foreach (var cam in cameras)
                    {
                        var cameraFollow = cam.GetComponent<CameraFollow>();
                        if (cameraFollow != null)
                        {
                            Debug.Log($"  - {cam.name}: CameraFollow найден");
                        }
                        else
                        {
                            Debug.LogWarning($"  - {cam.name}: CameraFollow НЕ найден");
                        }
                    }
                }
                else
                {
                    Debug.LogError("✗ Камеры не найдены!");
                }
            }
            
            Debug.Log("=== ПРОВЕРКА ЗАВЕРШЕНА ===");
        }
    }
}
