using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class Level1Manager : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip hitSound;
    public AudioClip lossSound;
    public AudioClip victorySound;
    public AudioClip clickSound;
    public AudioClip clickDoubleSound;

    public GameObject choicePanel;

    public GameObject bettingPanel;

    public int bettedHealthPoints = 0;

    public TextMeshProUGUI displayBettedHealthPoints;

    public enum ActionState {Idle, Attack, Heal};

    public bool quickTimeEventWin = false;

    public ActionState currentActionState = ActionState.Idle;

    [SerializeField] private QuickTime quickTime;
    [SerializeField] public Player player;
    [SerializeField] public Enemy enemy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.Initialize(5);

        enemy.Initialize(5);
        // if (audioSource && hitSound)
        // {
        //     audioSource.PlayOneShot(hitSound);
        // }

        choicePanel.SetActive(false);
        bettingPanel.SetActive(false);
    }

    void Update()
    {
        GamePlay();
        KeyManager();
        displayBettedHealthPoints.text = bettedHealthPoints.ToString();
    }

    void GamePlay()
    {
        switch(currentActionState)
        {
            case ActionState.Idle:
                choicePanel.SetActive(true);
                break;
            case ActionState.Attack:
            case ActionState.Heal:
                if (bettedHealthPoints == 0)
                {
                    bettingPanel.SetActive(true);
                }
                break;
            default:
                break;     

        }
    }

    public void playClick()
    {
        if (audioSource && clickSound)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void playDoubClick()
    {
        if (audioSource && clickDoubleSound) { audioSource.PlayOneShot(clickDoubleSound); }
    }

    public void SetAttackState()
    {
        //if (audioSource && attackSound) // happens whenever "strike" button hit???
        //{
        //    audioSource.PlayOneShot(attackSound);
        //}
        currentActionState = ActionState.Attack;
        choicePanel.SetActive(false);
    }

    public void SetHealState()
    {
        currentActionState = ActionState.Heal;
        choicePanel.SetActive(false);
    }

    public void IncreaseBettedHealthPoints()
    {
        if (bettedHealthPoints < player.health) {
            playClick();
            bettedHealthPoints++; 
        }
        else { playDoubClick();  }
    }

    public void DecreaseBettedHealthPoints()
    {
        if (bettedHealthPoints > 0) {
        playClick();
        bettedHealthPoints--;
        } else
        {
            playDoubClick();
        }
    }

    public void BetAllPoints()
    {
        playClick();
        bettedHealthPoints = player.health;
    }

    public void LoadSceneByName(string sceneName)
    {
        Debug.Log(sceneName + " loaded");
        SceneManager.LoadScene(sceneName);
    }

    private void KeyManager()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            DoExitGame();
        }
        else if (false && Keyboard.current.tKey.isPressed)
        {
            Debug.Log("t pressed.");
            playClick();
        }
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