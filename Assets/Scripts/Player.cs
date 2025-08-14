using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public Hand[] hands;

    public Scanner Scanner { get; private set; }

    public Vector2 InputVec { get; private set; }

    public float speed = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true); // 비활성화 객체도 추가
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + InputVec * speed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        if (!GameManager.Instance.isLive) return;

        InputVec = InputManager.Instance.GetMovementNormalized();
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.isLive) return;

        animator.SetFloat("Speed", InputVec.magnitude);

        if (InputVec.x != 0)
        {
            spriteRenderer.flipX = InputVec.x < 0;
        }
    }
}
