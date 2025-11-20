using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    // ×´Ì¬¹ÜÀí
    public static PlayerAttributes Instance;
    private int hp = 4;
    
    public int Hp
    {
        get => hp;
    }

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        
    }
}
