using UnityEngine;

// manager for the quick time events


public class Enemy : MonoBehaviour
{
    public int timeCurrent = 5000; // in ms, time left to hit the keys
    public int timeMax = 5000;
    public const int timeMargin = 20; // time in margin after officially over that we still accept input (TODO: implement logic)

    void Start()
    {
    }

    void keyHit(string key) // when key is hit ("up", "down", "left", "right")
    {
        // logic: check to see if key is correct

        // run other functions to do vfx, sfx, etc.

    }

    void runfx(Player player)
    {
        // Logic goes here
    }

}