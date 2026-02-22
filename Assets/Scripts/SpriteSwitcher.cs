using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpriteSwitcher : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite[] characterStateList = new Sprite[9];
    public RoundSpriteSet[] roundSpriteSets;

    public enum CharacterState { Idle, Attack, Hit, Death, Victory };
    public CharacterState currentState = CharacterState.Idle;

    [Header("Settings")]
    public float interval = 0.5f;

    private Image uiImage;
    private Coroutine activeRoutine;

    void Awake()
    {
        uiImage = GetComponent<Image>();
    }

    void Start()
    {
        SetState(CharacterState.Idle);
    }
    public void LoadRound(int roundIndex)
    {
        if (roundSpriteSets != null && roundSpriteSets.Length != 0)
        {
            int clampedIndex = roundIndex % roundSpriteSets.Length;
            characterStateList = roundSpriteSets[clampedIndex].sprites;
        }
        SetState(CharacterState.Idle);
    }

    public void SetState(CharacterState newState)
    {
        if (activeRoutine != null)
            StopCoroutine(activeRoutine);

        currentState = newState;

        switch (currentState)
        {
            case CharacterState.Idle:
                activeRoutine = StartCoroutine(SwitchSprites(characterStateList[0..2]));
                break;
            case CharacterState.Attack:
                activeRoutine = StartCoroutine(SwitchSprites(characterStateList[2..5]));
                break;
            case CharacterState.Hit:
                activeRoutine = StartCoroutine(SwitchSprites(characterStateList[5..7]));
                break;
            case CharacterState.Death:
                uiImage.sprite = characterStateList[7];
                break;
            case CharacterState.Victory:
                activeRoutine = StartCoroutine(SwitchSprites(characterStateList[8..10]));
                break;
        }
    }

    IEnumerator SwitchSprites(Sprite[] sprites)
    {
        int i = 0;
        while (true)
        {
            uiImage.sprite = sprites[i];
            i = (i + 1) % sprites.Length;
            yield return new WaitForSeconds(interval);
        }
    }
}

[System.Serializable]
public class RoundSpriteSet
{
    public string characterName;
    public Sprite[] sprites;
}