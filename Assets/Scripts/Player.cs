using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector2 inputVec;

    private float speed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + inputVec * speed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        inputVec = InputManager.Instance.GetMovementNormalized();
    }
}
