using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 5;
    public void Initialize(int health)
    {
        this.health = health;
    }

}