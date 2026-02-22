using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class QuickTime : MonoBehaviour
{
    public enum Directions { None, Up, Down, Left, Right };

    public Image[] arrowImages;
    public Sprite[] idleSprites; 
    public Sprite[] flashSprites;

    private List<Directions> activeDirections = new List<Directions>();
    public int quickTimeScore = 0;

	[SerializeField] BossFight bossFight;

	[SerializeField] Level1Manager level1Manager;

	void Start()
	{
		foreach(Image img in arrowImages)
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
		foreach(Image img in arrowImages)
		{
			img.gameObject.SetActive(true);
		}
        StartCoroutine(FlashRoutine(30f));
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
            
            Debug.Log($"Correct! Score: {quickTimeScore}");
        }
    }

    IEnumerator FlashRoutine(float totalTime)
    {
        float elapsed = 0;
        while (elapsed < totalTime)
        {
            activeDirections.Clear();
            int count = Random.Range(1, 3);

            List<int> pool = new List<int> { 0, 1, 2, 3 };
            for (int i = 0; i < count; i++)
            {
                int randomIndex = Random.Range(0, pool.Count);
                int arrowIdx = pool[randomIndex];

                activeDirections.Add((Directions)(arrowIdx + 1));
                arrowImages[arrowIdx].sprite = flashSprites[arrowIdx];

                pool.RemoveAt(randomIndex);
            }

            yield return new WaitForSeconds(0.8f);

            for (int i = 0; i < arrowImages.Length; i++)
            {
                arrowImages[i].sprite = idleSprites[i];
            }
            activeDirections.Clear();

            yield return new WaitForSeconds(0.7f);
            elapsed += 1.5f;
        }
        Debug.Log("Game Finished! Final Score: " + quickTimeScore);
		foreach(Image img in arrowImages)
		{
			img.gameObject.SetActive(false);
		}
		if (quickTimeScore > 10)
		{
			level1Manager.quickTimeEventWin = true;
		}
		bossFight.PlayerSideFight();
    }
}