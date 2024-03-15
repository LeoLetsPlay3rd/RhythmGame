using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KeyPressedFeedback : MonoBehaviour
{
    public Image circleS;
    public Image circleD;
    public Image circleK;
    public Image circleL;

    public Sprite defaultSprite;
    public Sprite pressedSprite;

    public float waitSeconds = 0.05f;

    private timeDSPscript timeDSP;

    private void Start()
    {
        timeDSP = GetComponent<timeDSPscript>();
    }

    private void Update()
    {
        HandleKeyPress(KeyCode.S, circleS);
        HandleKeyPress(KeyCode.D, circleD);
        HandleKeyPress(KeyCode.K, circleK);
        HandleKeyPress(KeyCode.L, circleL);
    }

    private void HandleKeyPress(KeyCode key, Image circle)
    {
        bool keyPressed = Input.GetKeyDown(key);
        bool keyReleased = Input.GetKeyUp(key);

        if (circle != null && defaultSprite != null)
        {
            if (keyPressed && !GetSuccessfulHitFlag(key))
            {
                circle.sprite = pressedSprite;
            }
            else if (keyReleased)
            {
                StartCoroutine(ResetSpriteAfterDelay(circle, defaultSprite, waitSeconds));
            }
        }
    }

    private IEnumerator ResetSpriteAfterDelay(Image image, Sprite newSprite, float delay)
    {
        yield return new WaitForSeconds(delay);
        image.sprite = newSprite;
    }

    private bool GetSuccessfulHitFlag(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.S:
                return timeDSP != null && timeDSP.successfullHitS;
            case KeyCode.D:
                return timeDSP != null && timeDSP.successfullHitD;
            case KeyCode.K:
                return timeDSP != null && timeDSP.successfullHitK;
            case KeyCode.L:
                return timeDSP != null && timeDSP.successfullHitL;
            default:
                return false;
        }
    }
}
