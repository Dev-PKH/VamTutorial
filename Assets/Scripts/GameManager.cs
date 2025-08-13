using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player player;
    public PoolManager poolManager;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }
}
