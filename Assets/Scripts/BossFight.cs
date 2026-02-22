using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class BossFight : MonoBehaviour
{
    [SerializeField] public Player player;
    [SerializeField] public Enemy enemy;

    public SpriteSwitcher playerSwitcher;
    public SpriteSwitcher enemySwitcher;

    [SerializeField] public Level1Manager level1Manager;

    public Image playerImage;
    public Image enemyImage;

    public Button nextRoundButton;

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
                enemy.health = Math.Max(0, enemy.health - healthChanger);
                playerSwitcher.SetState(SpriteSwitcher.CharacterState.Attack);
                enemySwitcher.SetState(SpriteSwitcher.CharacterState.Hit);
                Debug.Log(tier == 2 ? "PERFECT hit! " + healthChanger + " damage!" : "Good hit! " + healthChanger + " damage!");
            }
            else
            {
                player.health = Math.Max(0, player.health - baseAmount);
                playerSwitcher.SetState(SpriteSwitcher.CharacterState.Hit);
                enemySwitcher.SetState(SpriteSwitcher.CharacterState.Attack);
                Debug.Log("Failed QTE! Took " + baseAmount + " damage.");
            }
        }
        else if (level1Manager.currentActionState == Level1Manager.ActionState.Heal)
        {
            if (level1Manager.quickTimeEventWin)
            {
                player.health = Math.Min(player.maxHealth, player.health + healthChanger);
                playerSwitcher.SetState(SpriteSwitcher.CharacterState.Victory);
                enemySwitcher.SetState(SpriteSwitcher.CharacterState.Idle);
            }
            else
            {
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
    }

    private void CheckVictory()
    {
        if (player.health <= 0)
        {
            playerSwitcher.SetState(SpriteSwitcher.CharacterState.Death);
            Debug.Log("Enemy wins...");
        }
        else if (enemy.health <= 0)
        {
            enemySwitcher.SetState(SpriteSwitcher.CharacterState.Death);
            Debug.Log("You WINNNN");
        }
    }
}
