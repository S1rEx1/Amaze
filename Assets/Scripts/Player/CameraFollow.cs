using UnityEngine;

namespace Amaze
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target; // Цель для следования (игрок)
        public float smoothSpeed = 5f; // Скорость сглаживания
        public Vector3 offset = new Vector3(0, 0, -10); // Смещение камеры от игрока
        
        private void Start()
        {
            // Если цель не указана, ищем игрока по тегу
            if (target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    target = player.transform;
                    Debug.Log("CameraFollow: Игрок найден автоматически");
                }
                else
                {
                    Debug.LogError("CameraFollow: Игрок не найден! Убедитесь, что у игрока есть тег 'Player'");
                }
            }
        }
        
        private void LateUpdate()
        {
            if (target == null) return;
            
            // Вычисляем желаемую позицию камеры
            Vector3 desiredPosition = target.position + offset;
            
            // Плавно перемещаем камеру к желаемой позиции
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
