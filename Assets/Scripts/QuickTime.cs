using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class QuickTime : MonoBehaviour
{
    public enum Directions { None, Left, Down, Up, Right }; // arranged in order.

    public Image[] arrowImages;
    public Sprite[] idleSprites;
    public Sprite[] flashSprites;


    private List<Directions> activeDirections = new List<Directions>();
    public int quickTimeScore = 0;

    [SerializeField] SoundManager soundManager;
    [SerializeField] BossFight bossFight;
    [SerializeField] Level1Manager level1Manager;

    void Start()
    {
        foreach (Image img in arrowImages)
        {
            img.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        KeyManager();
    }

    public void StartQuickTime()
    {
        quickTimeScore = 0;
        level1Manager.bettingPanel.SetActive(false);
        foreach (Image img in arrowImages)
        {
            img.gameObject.SetActive(true);
        }
        StartCoroutine(FlashRoutine(10f));
    }

    private void KeyManager()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame) CheckHit(Directions.Up);
        if (Keyboard.current.downArrowKey.wasPressedThisFrame) CheckHit(Directions.Down);
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame) CheckHit(Directions.Left);
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame) CheckHit(Directions.Right);
    }

    void CheckHit(Directions pressed)
    {
        if (activeDirections.Contains(pressed))
        {
            quickTimeScore++;
            activeDirections.Remove(pressed);

            int index = (int)pressed - 1;
            arrowImages[index].sprite = idleSprites[index];

            soundManager.playClick();
            Debug.Log($"Correct! Score: {quickTimeScore}");
        } else
        {
            Debug.Log($"Incorreect! key {pressed}");
            //activeDirections.ForEach(Debug.Log);
            soundManager.playDoubleClick();
        }
    }

    IEnumerator FlashRoutine(float totalTime)
    {
        float speedMultiplier = 1f - Mathf.Clamp(level1Manager.currentRound * 0.08f, 0f, 0.4f);
        float flashOn = 0.8f * speedMultiplier;
        float flashOff = 0.7f * speedMultiplier;
        float cycleTime = flashOn + flashOff;

        float elapsed = 0;
        while (elapsed < totalTime)
        {
            activeDirections.Clear();
            int count = Random.Range(1, 3);

            List<int> pool = new List<int> { 0, 1, 2, 3 }; // Left, down, up, right
            for (int i = 0; i < count; i++)
            {
                int randomIndex = Random.Range(0, pool.Count); // Inlcusive range
                int arrowIdx = pool[randomIndex];

                activeDirections.Add((Directions)(arrowIdx + 1)); // +1 bc Directions starts with None
                arrowImages[arrowIdx].sprite = flashSprites[arrowIdx];

                pool.RemoveAt(randomIndex);
            }

            yield return new WaitForSeconds(flashOn);

            for (int i = 0; i < arrowImages.Length; i++)
            {
                arrowImages[i].sprite = idleSprites[i];
            }
            activeDirections.Clear();

            yield return new WaitForSeconds(flashOff);
            elapsed += cycleTime;
        }

        Debug.Log("Game Finished! Final Score: " + quickTimeScore);

        foreach (Image img in arrowImages)
        {
            img.gameObject.SetActive(false);
        }

        int maxPossibleHits = Mathf.RoundToInt(totalTime / cycleTime) * 2;
        float ratio = maxPossibleHits > 0 ? (float)quickTimeScore / maxPossibleHits : 0f;

        if (ratio >= 0.7f)
        {
            level1Manager.quickTimeTier = 2;
            level1Manager.quickTimeEventWin = true;
        }
        else if (ratio >= 0.35f)
        {
            level1Manager.quickTimeTier = 1;
            level1Manager.quickTimeEventWin = true;
        }
        else
        {
            level1Manager.quickTimeTier = 0;
            level1Manager.quickTimeEventWin = false;
        }

        level1Manager.currentRound++;
        bossFight.PlayerSideFight();
    }
}
