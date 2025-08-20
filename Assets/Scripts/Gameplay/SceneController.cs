using UnityEngine;
using UnityEngine.SceneManagement;

namespace Amaze
{
    public class SceneController : MonoBehaviour
    {
        [Header("Настройки сцен")]
        public string[] sceneNames = { "Tutorial", "Winter" }; // Названия сцен
        public int currentSceneIndex = 0; // Индекс текущей сцены
        
        [Header("Настройки перехода")]
        public bool showSceneInfo = true; // Показывать информацию о сцене
        public float sceneInfoDisplayTime = 3f; // Время показа информации
        
        private void Start()
        {
            // Определяем текущую сцену
            string currentSceneName = SceneManager.GetActiveScene().name;
            for (int i = 0; i < sceneNames.Length; i++)
            {
                if (sceneNames[i] == currentSceneName)
                {
                    currentSceneIndex = i;
                    break;
                }
            }
            
            Debug.Log($"SceneController: Текущая сцена '{currentSceneName}' (индекс {currentSceneIndex})");
            
            if (showSceneInfo)
            {
                ShowSceneInfo();
            }
        }
        
        private void Update()
        {
            // Горячие клавиши для тестирования
            if (Input.GetKeyDown(KeyCode.F1))
            {
                LoadNextScene();
            }
            else if (Input.GetKeyDown(KeyCode.F2))
            {
                LoadPreviousScene();
            }
            else if (Input.GetKeyDown(KeyCode.F3))
            {
                ReloadCurrentScene();
            }
            else if (Input.GetKeyDown(KeyCode.F4))
            {
                ShowSceneList();
            }
        }
        
        public void LoadScene(string sceneName)
        {
            Debug.Log($"SceneController: Загружаем сцену '{sceneName}'");
            SceneManager.LoadScene(sceneName);
        }
        
        public void LoadScene(int sceneIndex)
        {
            if (sceneIndex >= 0 && sceneIndex < sceneNames.Length)
            {
                Debug.Log($"SceneController: Загружаем сцену по индексу {sceneIndex} ('{sceneNames[sceneIndex]}')");
                SceneManager.LoadScene(sceneNames[sceneIndex]);
            }
            else
            {
                Debug.LogError($"SceneController: Неверный индекс сцены: {sceneIndex}");
            }
        }
        
        public void LoadNextScene()
        {
            int nextIndex = (currentSceneIndex + 1) % sceneNames.Length;
            LoadScene(nextIndex);
        }
        
        public void LoadPreviousScene()
        {
            int prevIndex = (currentSceneIndex - 1 + sceneNames.Length) % sceneNames.Length;
            LoadScene(prevIndex);
        }
        
        public void ReloadCurrentScene()
        {
            Debug.Log($"SceneController: Перезагружаем текущую сцену");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        public void ShowSceneList()
        {
            Debug.Log("=== ДОСТУПНЫЕ СЦЕНЫ ===");
            for (int i = 0; i < sceneNames.Length; i++)
            {
                string status = (i == currentSceneIndex) ? " (ТЕКУЩАЯ)" : "";
                Debug.Log($"  {i}: {sceneNames[i]}{status}");
            }
            Debug.Log("========================");
        }
        
        private void ShowSceneInfo()
        {
            Debug.Log($"=== ИНФОРМАЦИЯ О СЦЕНЕ ===");
            Debug.Log($"Название: {SceneManager.GetActiveScene().name}");
            Debug.Log($"Индекс: {currentSceneIndex}");
            Debug.Log($"Всего сцен: {sceneNames.Length}");
            Debug.Log($"========================");
            
            // Показываем управление
            Invoke(nameof(ShowControls), sceneInfoDisplayTime);
        }
        
        private void ShowControls()
        {
            Debug.Log("=== УПРАВЛЕНИЕ СЦЕНАМИ ===");
            Debug.Log("F1 - Следующая сцена");
            Debug.Log("F2 - Предыдущая сцена");
            Debug.Log("F3 - Перезагрузить сцену");
            Debug.Log("F4 - Список сцен");
            Debug.Log("========================");
        }
    }
}
