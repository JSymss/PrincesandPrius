using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanionSprite : MonoBehaviour
{
    public Image companion;
    private void Start()
    {
        companion.CrossFadeAlpha(0, 0, true);
    }

    public void FadeIn()
    {
        Debug.Log("fading in");
        {
            companion.CrossFadeAlpha(1, 0.5f, true);
        }
    }
    public void FadeOut()
    {
        Debug.Log("fading out");
        companion.CrossFadeAlpha(0, 0.5f, true);
    }
}
