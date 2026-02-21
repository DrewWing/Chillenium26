using UnityEngine;

// manager for the quick time events


public class QuickTime : MonoBehaviour
{
    public float timeLeft = 5f; // in sec, time left to hit the keys
    public float timeMax = 5f;
    //public const float timeMargin = 0.2f; // time in margin after officially over that we still accept input (TODO: implement logic)

    public float timeStart = 0.0f;
    public float timeEnd = 0.0f;

    void Start()
    {
        // TODO
    }

    void keyHit(string key) // when key is hit ("up", "down", "left", "right")
    {
        // logic TODO: check to see if key is correct

        // run other functions to do vfx, sfx, etc.

    }

    void startQuickTime(int numbKeys)
    {
        // creates a quick time event using a number of randomly selected keys.
        // starts timer.
        timeStart = timeStart.time;
        timeEnd = timeStart + timeMax;
        timeLeft = timeEnd - Time.time;
        
        // TODO; create keys
    }

    void FixedUpdate()
    {
        timeLeft = timeEnd - Time.time;


        // Time up
        if (timeLeft <= 0.0f)
        {
            endQuickTime();
            return;
        }

        // Otherwise, process keys

        // TODO: process keys here.

    }

}