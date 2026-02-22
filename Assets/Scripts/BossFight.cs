using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;

public class BossFight : MonoBehaviour
{
    [SerializeField] public Player player;
    [SerializeField] public Enemy enemy;

    public SpriteSwitcher playerSwitcher;
    public SpriteSwitcher enemySwitcher;

    [SerializeField] private SoundManager soundManager;
    [SerializeField] Level1Manager level1Manager;

    public Image playerImage;
    public Image enemyImage;

    public Button nextRoundButton;

    public TextMeshProUGUI outcomeText;

    void Start()
    {
        // playerImage.gameObject.SetActive(false);
        // enemyImage.gameObject.SetActive(false);
        nextRoundButton.gameObject.SetActive(false);
    }

    public void PlayerSideFight()
    {
        StartCoroutine(FightSequenceRoutine());
    }

    private IEnumerator FightSequenceRoutine()
    {
        playerImage.gameObject.SetActive(true);
        enemyImage.gameObject.SetActive(true);
        level1Manager.backgroundImage.sprite = level1Manager.backgroundsList[2];
        int baseAmount = level1Manager.bettedHealthPoints;
        int tier = level1Manager.quickTimeTier;

        int healthChanger;
        if (tier == 2)
            healthChanger = Mathf.RoundToInt(baseAmount * 1.5f);
        else
            healthChanger = baseAmount;

        if (level1Manager.currentActionState == Level1Manager.ActionState.Attack)
        {
            if (level1Manager.quickTimeEventWin)
            {
                soundManager.playAttack();
                enemy.health = Math.Max(0, enemy.health - healthChanger);
                playerSwitcher.SetState(SpriteSwitcher.CharacterState.Attack);
                enemySwitcher.SetState(SpriteSwitcher.CharacterState.Hit);
                outcomeText.text = tier == 2 ? "PERFECT hit! " + healthChanger + " damage!" : "Good hit! " + healthChanger + " damage!";
            }
            else
            {
                soundManager.playHit();
                player.health = Math.Max(0, player.health - baseAmount);
                playerSwitcher.SetState(SpriteSwitcher.CharacterState.Hit);
                enemySwitcher.SetState(SpriteSwitcher.CharacterState.Attack);
                outcomeText.text = "Failed QTE! Took " + baseAmount + " damage.";
            }
        }
        else if (level1Manager.currentActionState == Level1Manager.ActionState.Heal)
        {
            if (level1Manager.quickTimeEventWin)
            {
                soundManager.playAttack();
                player.health = Math.Min(player.maxHealth, player.health + healthChanger);
                playerSwitcher.SetState(SpriteSwitcher.CharacterState.Victory);
                enemySwitcher.SetState(SpriteSwitcher.CharacterState.Idle);
            }
            else
            {
                soundManager.playHit();
                enemy.health = Math.Min(enemy.maxHealth, enemy.health + baseAmount);
                playerSwitcher.SetState(SpriteSwitcher.CharacterState.Idle);
                enemySwitcher.SetState(SpriteSwitcher.CharacterState.Victory);
            }
        }
        level1Manager.playerHealthBar.TakeDamage(player.health);
        level1Manager.enemyHealthBar.TakeDamage(enemy.health);

        CheckVictory();
        nextRoundButton.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
    }

    public void NextRound()
    {
        level1Manager.quickTimeEventWin = false;
        level1Manager.quickTimeTier = 0;
        level1Manager.bettedHealthPoints = 0;
        level1Manager.currentActionState = Level1Manager.ActionState.Idle;
        playerSwitcher.LoadRound(level1Manager.currentRound);
        enemySwitcher.LoadRound(level1Manager.currentRound);
        nextRoundButton.gameObject.SetActive(false);
    }

    private void CheckVictory()
    {
        if (player.health <= 0)
        {
            level1Manager.backgroundImage.sprite = level1Manager.backgroundsList[4];
            playerSwitcher.SetState(SpriteSwitcher.CharacterState.Death);
            Debug.Log("Enemy wins...");
            soundManager.playLoss();
        }
        else if (enemy.health <= 0)
        {
            level1Manager.backgroundImage.sprite = level1Manager.backgroundsList[3];
            enemySwitcher.SetState(SpriteSwitcher.CharacterState.Death);
            Debug.Log("You WINNNN");
            soundManager.playVictory();
        }
    }
}
