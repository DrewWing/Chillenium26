using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 5;
    public void Initialize(int health)
    {
        this.health = health;
    }
}