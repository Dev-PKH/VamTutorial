using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public float speed;
    public Rigidbody2D target; 

    private bool isLive = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!isLive) return;

        Vector2 dirVec = target.position - rb.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + nextVec);
        rb.linearVelocity = Vector2.zero; // 이전에 가해진 힘을 0으로 초기화
        // 단 속도가 0이므로 이동을 안해야하지만, movePosition이 이를 강제 실행
    }

    private void LateUpdate()
    {
        if (!isLive) return;

        spriteRenderer.flipX = target.position.x < rb.position.x;
    }
}
