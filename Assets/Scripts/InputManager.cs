using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerInputActions playerInputActions; // Input Action Asset으로 생성한 클래스

    private void Awake()
    {
        if(Instance == null) Instance = this;

        playerInputActions = new PlayerInputActions(); // 입력 클래스 생성
        playerInputActions.Player.Enable(); // 입력 클래스 활성화
    }

    /// <summary>
    /// 움직임에 대한 방향을 정규화하여 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public Vector2 GetMovementNormalized()
    {
        Vector2 moveVec = playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
        return moveVec;
    }
}
