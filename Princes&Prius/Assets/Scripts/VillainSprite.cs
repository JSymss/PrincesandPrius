using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillainSprite : MonoBehaviour
{
    public Image villain;
    private void Start()
    {
        villain.CrossFadeAlpha(0, 0, true);
    }

    public void FadeIn()
    {
        Debug.Log("fading in");
        {
            villain.CrossFadeAlpha(1, 0.5f, true);
        }
    }
    public void FadeOut()
    {
        Debug.Log("fading out");
        villain.CrossFadeAlpha(0, 0.5f, true);
    }
}
