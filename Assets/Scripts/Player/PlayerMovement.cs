using UnityEngine;
using UnityEngine.InputSystem;

namespace Amaze
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public Rigidbody2D rb;
        public Camera cam;

        private InputAction moveAction;
        private Vector2 movement;

        void Start()
        {
            // Проверяем, что у игрока есть тег Player
            if (gameObject.tag != "Player")
            {
                gameObject.tag = "Player";
                Debug.Log("PlayerMovement: Установлен тег 'Player'");
            }
            
            // Проверяем компоненты
            if (rb == null)
            {
                rb = GetComponent<Rigidbody2D>();
                if (rb == null)
                {
                    Debug.LogError("PlayerMovement: Rigidbody2D не найден!");
                }
            }
            
            if (cam == null)
            {
                cam = Camera.main;
                if (cam == null)
                {
                    Debug.LogWarning("PlayerMovement: Главная камера не найдена!");
                }
            }
        }

        void Awake()
        {
            // Создаем InputAction для движения
            moveAction = new InputAction("Move", InputActionType.Value, "<Gamepad>/leftStick");
            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
            
            moveAction.Enable();
        }

        void Update()
        {
            // Получаем значение движения из нового Input System
            movement = moveAction.ReadValue<Vector2>();
        }

        void FixedUpdate()
        {
            // Движение обрабатывается в FixedUpdate для работы с физикой
            if (rb != null)
            {
                rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
            }
        }

        void OnDestroy()
        {
            if (moveAction != null)
                moveAction.Dispose();
        }
    }
}
