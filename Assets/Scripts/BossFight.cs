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

    void Start()
    {
        // playerImage.gameObject.SetActive(false);
        // enemyImage.gameObject.SetActive(false);
    }

    public void PlayerSideFight()
    {
        StartCoroutine(FightSequenceRoutine());
    }

    private IEnumerator FightSequenceRoutine()
    {
        playerImage.gameObject.SetActive(true);
        enemyImage.gameObject.SetActive(true);

        int healthChanger = level1Manager.bettedHealthPoints;

        if (level1Manager.currentActionState == Level1Manager.ActionState.Attack)
        {
            if (level1Manager.quickTimeEventWin)
            {
                enemy.health = Math.Max(0, enemy.health - healthChanger);
                playerSwitcher.SetState(SpriteSwitcher.CharacterState.Attack);
                enemySwitcher.SetState(SpriteSwitcher.CharacterState.Hit);
            }
            else
            {
                player.health = Math.Max(0, player.health - healthChanger);
                playerSwitcher.SetState(SpriteSwitcher.CharacterState.Hit);
                enemySwitcher.SetState(SpriteSwitcher.CharacterState.Attack);
            }
        }
        else if (level1Manager.currentActionState == Level1Manager.ActionState.Heal)
        {
            if (level1Manager.quickTimeEventWin)
            {
                player.health += healthChanger;
                playerSwitcher.SetState(SpriteSwitcher.CharacterState.Victory);
                enemySwitcher.SetState(SpriteSwitcher.CharacterState.Idle);
            }
            else
            {
                enemy.health += healthChanger;
                playerSwitcher.SetState(SpriteSwitcher.CharacterState.Idle);
                enemySwitcher.SetState(SpriteSwitcher.CharacterState.Victory);
            }
        }

        CheckVictory();

        level1Manager.quickTimeEventWin = false;
        level1Manager.bettedHealthPoints = 0;

        yield return new WaitForSeconds(10f);

        // playerImage.gameObject.SetActive(false);
        // enemyImage.gameObject.SetActive(false);
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