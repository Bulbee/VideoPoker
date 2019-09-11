using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardObject : MonoBehaviour
{
    [SerializeField]
    private Sprite cardBack;
    public Card cardData;
    public bool held = false;

    [SerializeField]
    private Image displayImage;

    [SerializeField]
    private Outline outline;

    public void Flip()
    {
        displayImage.sprite = cardData.sprite;
    }

    public void Hide()
    {
        displayImage.sprite = cardBack;
        outline.enabled = false;
    }

    public void Tap()
    {
        //TODO: Some sort of effect here
        if(held == false)
        {
            held = true;
            outline.enabled = true;
        }
        else
        {
            held = false;
            outline.enabled = false;
        }
    }
}
