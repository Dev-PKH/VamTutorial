using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerInputActions playerInputActions; // Input Action Asset���� ������ Ŭ����

    private void Awake()
    {
        if(Instance == null) Instance = this;

        playerInputActions = new PlayerInputActions(); // �Է� Ŭ���� ����
        playerInputActions.Player.Enable(); // �Է� Ŭ���� Ȱ��ȭ
    }

    /// <summary>
    /// �����ӿ� ���� ������ ����ȭ�Ͽ� �������� �Լ�
    /// </summary>
    /// <returns></returns>
    public Vector2 GetMovementNormalized()
    {
        Vector2 moveVec = playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
        return moveVec;
    }
}
