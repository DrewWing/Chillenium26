using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class BossFight : MonoBehaviour
{
    [SerializeField] public Player player;
    [SerializeField] public Enemy enemy;

    [SerializeField] public Level1Manager level1Manager;
    public void PlayerSideFight()
    {
        Debug.Log("Before");
        Debug.Log("Player health: " + player.health);
        Debug.Log("Enemy health: " + enemy.health);
        int healthChanger = level1Manager.bettedHealthPoints;
        if (level1Manager.currentActionState == Level1Manager.ActionState.Attack)
        {
            if (level1Manager.quickTimeEventWin)
            {
                enemy.health = Math.Max(0, enemy.health - healthChanger);
            }
            else
            {
                player.health = Math.Max(0, enemy.health - healthChanger);
            }
        }
        else if (level1Manager.currentActionState == Level1Manager.ActionState.Heal)
        {
            if (level1Manager.quickTimeEventWin)
            {
                player.health += healthChanger;
            }
            else
            {
                enemy.health += healthChanger;
            }
        }
        CheckVictory();
        Debug.Log("After");
        Debug.Log("Player health: " + player.health);
        Debug.Log("Enemy health: " + enemy.health);
        level1Manager.quickTimeEventWin = false;
        level1Manager.bettedHealthPoints = 0;
        level1Manager.currentActionState = Level1Manager.ActionState.Idle;
    }

    private void CheckVictory()
    {
        if (player.health == 0)
        {
            Debug.Log("Enemy wins...");
        }
        else if (enemy.health == 0)
        {
            Debug.Log("You WINNNN");
        }
    }
}