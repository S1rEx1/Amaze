using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 15f;
    [SerializeField] private float rotationSpeed = 15f;
    
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 smoothVelocity;
    private bool isMovementLocked;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (isMovementLocked) return;
        
        // Получаем ввод
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        
        // Нормализуем, чтобы диагональное движение не было быстрее
        moveInput = moveInput.normalized;
        
        // Управление анимацией
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        if (isMovementLocked) return;
        
        // Плавное ускорение/замедление
        smoothVelocity = Vector2.Lerp(
            smoothVelocity, 
            moveInput * moveSpeed, 
            (moveInput.magnitude > 0 ? acceleration : deceleration) * Time.fixedDeltaTime
        );
        
        // Применяем движение
        rb.velocity = smoothVelocity;
        
        // Плавный поворот в сторону движения
        if (moveInput.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.Lerp(
                transform.rotation, 
                targetRotation, 
                rotationSpeed * Time.fixedDeltaTime
            );
        }
    }

    private void UpdateAnimation()
    {
        bool isMoving = moveInput.magnitude > 0.1f;
        animator.SetBool("IsMoving", isMoving);
        
        // Отражение спрайта по горизонтали
        if (moveInput.x > 0) spriteRenderer.flipX = false;
        else if (moveInput.x < 0) spriteRenderer.flipX = true;
    }

    public void LockMovement(bool state) => isMovementLocked = state;
}
