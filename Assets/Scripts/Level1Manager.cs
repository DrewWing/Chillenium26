using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class Level1Manager : MonoBehaviour
{

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip lossSound;
    public AudioClip victorySound;

    public GameObject choicePanel;

    public GameObject bettingPanel;

    public int bettedHealthPoints = 0;

    public TextMeshProUGUI displayBettedHealthPoints;

    public enum ActionState {Idle, Decision, Attack, Heal};

    public ActionState currentActionState = ActionState.Idle;

    private bool tWasPressed = false;

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

    public void IncreaseBettedHealthPoints()
    {
        if (bettedHealthPoints < player.health)
            bettedHealthPoints++;
    }

    public void DecreaseBettedHealthPoints()
    {
        if (bettedHealthPoints > 0)
            bettedHealthPoints--;
    }

    public void BetAllPoints()
    {
        bettedHealthPoints = player.health;
    }

    public void LoadSceneByName(string sceneName)
    {
        Debug.Log(sceneName + " loaded");
        // SceneManager.LoadScene(sceneName);
    }

    private void KeyManager()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            DoExitGame();
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