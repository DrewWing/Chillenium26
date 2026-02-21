using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 5;
    public int maxHealth = 5;

    void Start()
    {
        // Start animation
    }

    void FixedUpdate()
    {
        // do animation if necessary
        // maybe another script?
    }

    void takeAction(Player player)
    {
        // Logic goes here
    }

    bool isDead() { return health <= 0; }
    bool isAlive() { return health > 0; }


}