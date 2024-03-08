using System.Collections;
using System.Collections.Generic;
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

    private bool sKeyPressed = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (circleS != null && pressedSprite != null)
            {
                circleS.sprite = pressedSprite;
                sKeyPressed = true; 
            }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (circleS != null && defaultSprite != null)
            {
                sKeyPressed = false; 
                StartCoroutine(ResetSpriteAfterDelay(circleS, defaultSprite, waitSeconds)); 
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (circleD != null && pressedSprite != null)
            {
                circleD.sprite = pressedSprite;
                sKeyPressed = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            if (circleD != null && defaultSprite != null)
            {
                sKeyPressed = false;
                StartCoroutine(ResetSpriteAfterDelay(circleD, defaultSprite, waitSeconds));
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (circleK != null && pressedSprite != null)
            {
                circleK.sprite = pressedSprite;
                sKeyPressed = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            if (circleK != null && defaultSprite != null)
            {
                sKeyPressed = false;
                StartCoroutine(ResetSpriteAfterDelay(circleK, defaultSprite, waitSeconds));
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (circleL != null && pressedSprite != null)
            {
                circleL.sprite = pressedSprite;
                sKeyPressed = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            if (circleL != null && defaultSprite != null)
            {
                sKeyPressed = false;
                StartCoroutine(ResetSpriteAfterDelay(circleL, defaultSprite, waitSeconds));
            }
        }

        IEnumerator ResetSpriteAfterDelay(Image image, Sprite newSprite, float delay)
        {
            yield return new WaitForSeconds(delay);

            if (!sKeyPressed && image != null && newSprite != null)
            {
                image.sprite = newSprite;
            }
        }
    }
}
