using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public Scanner Scanner { get; private set; }

    public Vector2 InputVec { get; private set; }

    public float speed = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Scanner = GetComponent<Scanner>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + InputVec * speed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        InputVec = InputManager.Instance.GetMovementNormalized();
    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", InputVec.magnitude);

        if (InputVec.x != 0)
        {
            spriteRenderer.flipX = InputVec.x < 0;
        }
    }
}
