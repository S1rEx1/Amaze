using UnityEngine;
using UnityEngine.SceneManagement;

namespace Amaze
{
    public class SceneTransitionTester : MonoBehaviour
    {
        [Header("Тестирование переходов")]
        public bool showTestUI = true;
        public KeyCode testNextSceneKey = KeyCode.N;
        public KeyCode testPrevSceneKey = KeyCode.P;
        public KeyCode testReloadKey = KeyCode.R;
        
        private void Update()
        {
            // Тестирование переходов клавишами
            if (Input.GetKeyDown(testNextSceneKey))
            {
                TestNextScene();
            }
            else if (Input.GetKeyDown(testPrevSceneKey))
            {
                TestPreviousScene();
            }
            else if (Input.GetKeyDown(testReloadKey))
            {
                TestReloadScene();
            }
        }
        
        private void TestNextScene()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            string nextScene = "";
            
            if (currentScene == "Tutorial")
            {
                nextScene = "Winter";
            }
            else if (currentScene == "Winter")
            {
                nextScene = "Tutorial";
            }
            
            if (!string.IsNullOrEmpty(nextScene))
            {
                Debug.Log($"Тест: Переходим на сцену '{nextScene}'");
                SceneManager.LoadScene(nextScene);
            }
            else
            {
                Debug.LogWarning("Тест: Неизвестная сцена для перехода");
            }
        }
        
        private void TestPreviousScene()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            string prevScene = "";
            
            if (currentScene == "Tutorial")
            {
                prevScene = "Winter";
            }
            else if (currentScene == "Winter")
            {
                prevScene = "Tutorial";
            }
            
            if (!string.IsNullOrEmpty(prevScene))
            {
                Debug.Log($"Тест: Возвращаемся на сцену '{prevScene}'");
                SceneManager.LoadScene(prevScene);
            }
            else
            {
                Debug.LogWarning("Тест: Неизвестная сцена для возврата");
            }
        }
        
        private void TestReloadScene()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            Debug.Log($"Тест: Перезагружаем сцену '{currentScene}'");
            SceneManager.LoadScene(currentScene);
        }
        
        private void OnGUI()
        {
            if (!showTestUI) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 300, 150));
            GUILayout.Label("=== ТЕСТ ПЕРЕХОДОВ МЕЖДУ СЦЕНАМИ ===");
            GUILayout.Label($"Текущая сцена: {SceneManager.GetActiveScene().name}");
            GUILayout.Space(10);
            GUILayout.Label("Горячие клавиши:");
            GUILayout.Label($"N - Следующая сцена");
            GUILayout.Label($"P - Предыдущая сцена");
            GUILayout.Label($"R - Перезагрузить сцену");
            GUILayout.Space(10);
            GUILayout.Label("Или используйте триггеры в сцене");
            GUILayout.EndArea();
        }
    }
}
