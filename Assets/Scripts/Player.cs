using UnityEngine;

public class Player : MonoBehaviour
{
    private int health = 5;
    public void Initialize(int health)
    {
        this.health = health;
    }
}