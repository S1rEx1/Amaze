using UnityEngine;
using UnityEngine.SceneManagement;

namespace Amaze
{
    public class SceneLoader : MonoBehaviour
    {
        [Header("Настройки загрузки")]
        public string sceneNameToLoad; // Имя сцены, которую нужно загрузить
        public bool useSceneIndex = false; // Использовать индекс сцены вместо имени
        public int sceneIndexToLoad = 0; // Индекс сцены для загрузки
        
        [Header("Настройки перехода")]
        public bool fadeOut = true; // Затемнение при переходе
        public float transitionTime = 1f; // Время перехода
        
        [Header("Сохранение данных")]
        public bool savePlayerData = true; // Сохранять данные игрока перед переходом
        public bool useCheckpoint = false; // Использовать чекпоинт для новой сцены
        public Vector3 checkpointPosition = Vector3.zero; // Позиция чекпоинта в новой сцене
        
        private void Start()
        {
            // Проверяем настройки
            if (!useSceneIndex && string.IsNullOrEmpty(sceneNameToLoad))
            {
                Debug.LogError($"SceneLoader {name}: sceneNameToLoad не указано!");
            }
            
            if (useSceneIndex && sceneIndexToLoad < 0)
            {
                Debug.LogError($"SceneLoader {name}: sceneIndexToLoad должен быть >= 0!");
            }
            
            // Проверяем, что сцена существует в Build Settings
            CheckSceneExists();
        }

        private void CheckSceneExists()
        {
            bool sceneExists = false;
            
            if (useSceneIndex)
            {
                if (sceneIndexToLoad < SceneManager.sceneCountInBuildSettings)
                {
                    sceneExists = true;
                    string scenePath = SceneUtility.GetScenePathByBuildIndex(sceneIndexToLoad);
                    string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                    Debug.Log($"SceneLoader {name}: Сцена по индексу {sceneIndexToLoad} найдена: '{sceneName}'");
                }
            }
            else
            {
                for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
                {
                    string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                    string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                    if (sceneName == sceneNameToLoad)
                    {
                        sceneExists = true;
                        Debug.Log($"SceneLoader {name}: Сцена '{sceneNameToLoad}' найдена в Build Settings (индекс {i})");
                        break;
                    }
                }
            }
            
            if (!sceneExists)
            {
                if (useSceneIndex)
                {
                    Debug.LogError($"SceneLoader {name}: Сцена с индексом {sceneIndexToLoad} НЕ найдена в Build Settings!");
                }
                else
                {
                    Debug.LogError($"SceneLoader {name}: Сцена '{sceneNameToLoad}' НЕ найдена в Build Settings!");
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"SceneLoader {name}: Триггер сработал! Объект: {other.name}, Тег: {other.tag}");
            
            // Проверяем, что в триггер вошел именно игрок
            if (other.CompareTag("Player"))
            {
                Debug.Log($"SceneLoader {name}: Загружаем сцену...");
                
                // Сохраняем данные игрока перед переходом
                if (savePlayerData)
                {
                    PlayerDataManager.SaveCurrentPlayerData();
                    Debug.Log($"SceneLoader {name}: Данные игрока сохранены");
                }
                
                // Устанавливаем чекпоинт для новой сцены
                if (useCheckpoint)
                {
                    PlayerDataManager.SetCheckpoint(checkpointPosition);
                    Debug.Log($"SceneLoader {name}: Установлен чекпоинт в позиции {checkpointPosition}");
                }
                
                LoadScene();
            }
            else
            {
                Debug.Log($"SceneLoader {name}: Объект {other.name} не имеет тег 'Player'");
            }
        }

        private void LoadScene()
        {
            if (useSceneIndex)
            {
                if (sceneIndexToLoad < SceneManager.sceneCountInBuildSettings)
                {
                    Debug.Log($"SceneLoader {name}: Загружаем сцену по индексу {sceneIndexToLoad}");
                    SceneManager.LoadScene(sceneIndexToLoad);
                }
                else
                {
                    Debug.LogError($"SceneLoader {name}: Индекс сцены {sceneIndexToLoad} выходит за пределы Build Settings!");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(sceneNameToLoad))
                {
                    Debug.Log($"SceneLoader {name}: Загружаем сцену '{sceneNameToLoad}'");
                    SceneManager.LoadScene(sceneNameToLoad);
                }
                else
                {
                    Debug.LogError($"SceneLoader {name}: Имя сцены не указано!");
                }
            }
        }
    }
}
