using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Level1Manager : MonoBehaviour
{

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip lossSound;
    public AudioClip victorySound;

    public GameObject choicePanel;

    public GameObject bettingPanel;

    public QuickTime qt;

    public int bettedHealthPoints = 0;

    public enum ActionState {Idle, Decision, Attack, Heal};

    public ActionState currentActionState = ActionState.Idle;

    private bool tWasPressed = false;

    [SerializeField] QuickTime quickTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        qt.Start();

        Player player = new();
        player.Initialize(5);

        Enemy enemy = new();
        player.Initialize(5);
        // if (audioSource && hitSound)
        // {
        //     audioSource.PlayOneShot(hitSound);
        // }
    }

    void Update()
    {
        GamePlay();
        KeyManager();
    }

    void GamePlay()
    {
        switch(currentActionState)
        {
            case ActionState.Idle:
                choicePanel.SetActive(true);
                break;
            case ActionState.Decision:
                bettingPanel.SetActive(true);
                break;
            case ActionState.Attack:
            case ActionState.Heal:
                quickTime.startQuickTime();
                break;
            default:
                break;     

        }
    }

    public void SetAttackState()
    {
        currentActionState = ActionState.Attack;
        choicePanel.SetActive(false);
    }

    public void SetHealState()
    {
        currentActionState = ActionState.Heal;
        choicePanel.SetActive(false);
    }

    public void SetBettedHealthPoints(int points)
    {
        bettedHealthPoints = points;
        bettingPanel.SetActive(false);
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