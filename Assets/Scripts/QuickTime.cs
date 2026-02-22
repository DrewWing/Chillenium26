using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem;
// manager for the quick time events



public class QuickTime : MonoBehaviour
{
    // to keep track of what's flashing
    public enum Directions {None, Up, Down, Left, Right};

    [SerializeField] TimerDisplay timerDisplay;


    public void Start()
    {
        
    }

    void Update()
    {
        KeyManager();
    }

    public void StartQuickTime()
    {
        Debug.Log("hahahahahahhaa");
        

    }

    private void KeyManager()
    {
        if (Keyboard.current.upArrowKey.isPressed)
        {
            Debug.Log("Up was pressed!");
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            Debug.Log("Down was pressed!");
        }
        else if (Keyboard.current.leftArrowKey.isPressed)
        {
            Debug.Log("Left was pressed!");
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            Debug.Log("Right was pressed!");
        }
    }

    void Flash()
    {
        
    }

}