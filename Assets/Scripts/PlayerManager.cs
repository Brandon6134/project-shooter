using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Player = gameObject;
    }
}
