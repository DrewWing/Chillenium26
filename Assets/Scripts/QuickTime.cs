using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
// manager for the quick time events



public class QuickTime : MonoBehaviour
{
    // TODO: do config stuff 
    private const int keyUp = 0;
    private const int keyDown = 1;
    private const int keyLeft = 2;
    private const int keyRight = 3;

    private const int statusWaiting = 0;
    private const int statusIncorrect = 1;
    private const int statusCorrect = 2;


    public float timeLeft = 5f; // in sec, time left to hit the keys
    public float timeMax = 5f;
    //public const float timeMargin = 0.2f; // time in margin after officially over that we still accept input (TODO: implement logic)

    public float timeStart = 0.0f;
    public float timeEnd = 0.0f;

    protected const int OUT_OF_TIME = 0;
    protected const int SUCCESS = 1;
    protected const int FAIL = 2;

    public List<int> keyQueue = new List<int> {};


    public void Start()
    {
        // TODO
    }

    void keyHit(int key) // user hits the key (up, down, left, right)
    {
        // logic TODO: check to see if key is correct

        // run other functions to do vfx, sfx, etc.

    }

    public void startQuickTime(int numbKeys)
    {
        // creates a quick time event using a number of randomly selected keys.
        // starts timer.
        timeStart = Time.time;
        timeEnd = timeStart + timeMax;
        timeLeft = timeEnd - Time.time;

        Debug.Log("startQuickTime called with numbKeys " + numbKeys);

        // Create keys
        for (uint i = 0; i < numbKeys; i++)
        {
            keyQueue.Add(Random.Range(0,3));
        }


        // Log the keys
        foreach (int i in keyQueue)
        {
            Debug.Log(i);
        }


        // Create the visyal assets and arrange them
        // TODO


    }


    public void endQuickTime(int status = 0)
    {
        // Ends thhe quick time event
        // parameter status:
        //      0 if out of time
        //      1 if success
        //      2 if fail

        // hide/destroy the ui stuff

        // reset timer.

        Console.WriteLine("endQuickTime called.");

        // clear keys
        keyQueue.Clear();

        // reset everything else

    }



    void FixedUpdate()
    {
        timeLeft = timeEnd - Time.time;


        // Time up
        if (timeLeft <= 0.0f)
        {
            endQuickTime(OUT_OF_TIME);
            return;
        }

        // Otherwise, process keys

        // TODO: process keys here.

    }

}