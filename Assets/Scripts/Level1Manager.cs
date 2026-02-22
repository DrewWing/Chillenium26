using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class Level1Manager : MonoBehaviour
{

    public GameObject choicePanel;
    public GameObject bettingPanel;

    public int bettedHealthPoints = 0;
    public int currentRound = 0;

    public TextMeshProUGUI displayBettedHealthPoints;

    public enum ActionState { Idle, Attack, Heal };

    public bool quickTimeEventWin = false;
    public int quickTimeTier = 0;

    public ActionState currentActionState = ActionState.Idle;

    public HealthBar playerHealthBar;
    public HealthBar enemyHealthBar;
    
    [SerializeField] private SoundManager soundManager;

    [SerializeField] private QuickTime quickTime;
    [SerializeField] public Player player;
    [SerializeField] public Enemy enemy;

    void Start()
    {
        player.Initialize(5);
        enemy.Initialize(5);
        playerHealthBar.Initialize(5);
        enemyHealthBar.Initialize(5);

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
        switch (currentActionState)
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
        SceneManager.LoadScene(sceneName);
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
