using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class SpriteSwitcher : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite[] characterStateList = new Sprite[9];

    public enum CharacterState {Idle, Attack, Hit, Death, Victory};

    public CharacterState currentState = CharacterState.Idle;

    /*
    Character sprite list
    0: Idle1
    1: Idle2
    2: Attack1
    3: Attack2
    4: Attack3
    5: Hit1
    6: Death1
    7: Victory1
    8: Victory2
    */

    [Header("Time between 2 sprites")]
    public float interval = 0.5f;
    
    private Image uiImage;

    void Start()
    {
        uiImage = GetComponent<Image>();
        ControlCharacter();
    }

    // Call when state change
    public void ControlCharacter()
    {
        switch(currentState)
        {
            case CharacterState.Idle:
                StartCoroutine(SwitchSprites(characterStateList[0..2]));
                break;
            case CharacterState.Attack:
                StartCoroutine(SwitchSprites(characterStateList[2..5]));
                break;
            case CharacterState.Hit:
                uiImage.sprite = characterStateList[5];
                break;
            case CharacterState.Death:
                uiImage.sprite = characterStateList[6];
                break;
            case CharacterState.Victory:
                StartCoroutine(SwitchSprites(characterStateList[7..9]));
                break;
            default:
                uiImage.sprite = characterStateList[0];
                break;
        }
    }

    IEnumerator SwitchSprites(Sprite[] sprites)
    {
        while (true)
        {
            foreach(Sprite sprite in sprites) {
                uiImage.sprite = sprite;
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
