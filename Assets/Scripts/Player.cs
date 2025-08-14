using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public Hand[] hands;
    public RuntimeAnimatorController[] animatorController;

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

    private void OnEnable()
    {
        speed *= Character.Speed;
        animator.runtimeAnimatorController = animatorController[GameManager.Instance.playerId];
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance.isLive) return;

        GameManager.Instance.health -= Time.deltaTime * 10;

        if(GameManager.Instance.health < 0)
        {
            for(int i = 2; i<transform.childCount; i++) // 자식의 개수를 가져옴
            {
                transform.GetChild(i).gameObject.SetActive(false); // i번째 자식의 객체를 비활성화
            }

            animator.SetTrigger("Dead");
            GameManager.Instance.GameOver();
        }
    }
}
