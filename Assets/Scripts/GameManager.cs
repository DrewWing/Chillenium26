using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Cover")]
    public float flashImageDuration = 3.0f;

    public GameObject gameCoverPanel;

    [Header("Main Menu")]
    public GameObject menuPanel;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    public QuickTime qt;

    private bool tWasPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        qt.Start();

        if (gameCoverPanel != null)
        StartCoroutine(FlashImage(gameCoverPanel));
        // if (audioSource && clickSound)
        // {
        //     audioSource.PlayOneShot(clickSound);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        KeyManager();
    }

    IEnumerator FlashImage(GameObject targetImage)
    {
        targetImage.SetActive(true);
        yield return new WaitForSeconds(flashImageDuration);
        targetImage.SetActive(false);
    }

    public void LoadSceneByName(string sceneName)
    {
        Debug.Log(sceneName + " loaded");
        // SceneManager.LoadScene(sceneName);
    }

    private void KeyManager()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            Debug.Log("Space key was pressed!");
        }
        else if (Keyboard.current.wKey.isPressed)
        {
            Debug.Log("W was pressed!");
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            Debug.Log("A was pressed!");
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            Debug.Log("S was pressed!");
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            Debug.Log("D was pressed!");
        }
        else if (Keyboard.current.upArrowKey.isPressed)
        {
            Debug.Log("Up was pressed!");
        }
        else if (Keyboard.current.escapeKey.isPressed)
        {
            DoExitGame();
        }
        else if (Keyboard.current.tKey.isPressed && tWasPressed != true) // TODO: for testing quicktime, delete before release.
        {
            Debug.Log("T was pressed! clearing old quicktime and making new one...");
        }

        tWasPressed = Keyboard.current.tKey.isPressed;
    }

    public void DoExitGame()
    {
        Application.Quit();
    }
}



/*
    Game Start
    Title Screen Flash
    Main Menu
    - Start Button
    - Exit button
    - Options (Volume up down)
    
    Start Button
    - Cut scene (optional)
    - front of arcade machine screen
    - WASD keys control, arrow keys control
    -- W: 
    -- A: 
    -- S:
    -- D: 
    -- Up: 
    -- Down:
    -- Left:
    -- Right:

    1 player, 3 enemies
    Player
    - Health = 5 hitpoints
    points Reset every level

    hitpoints select damage level with keys
    quick time event 
    - win, move to next level 
*/