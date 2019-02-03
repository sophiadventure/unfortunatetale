using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{

    public Image fadeScreen;
    public float fadeSpeed = 1;
    private float fadeEpsilon = 0.000000001f; // error margin

    public bool shouldFadeToBlack = false;
    public bool shouldFadeFromBlack = false;

    public static UIFade INSTANCE;

    // Start is called before the first frame update
    void Start()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, this.fadeSpeed * Time.deltaTime));
            if(System.Math.Abs(fadeScreen.color.a - 1f) < this.fadeEpsilon) // this means fadeScreen.color.a == 1f
            {
                this.shouldFadeToBlack = false;
            }
        }

        if (this.shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, this.fadeSpeed * Time.deltaTime));
            if (System.Math.Abs(fadeScreen.color.a) < this.fadeEpsilon) // this means fadeScreen.color.a == 0f
            {
                this.shouldFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack() {
        this.shouldFadeToBlack = true;
        this.shouldFadeFromBlack = false;
    }

    public void FadeFromBlack() {
        this.shouldFadeFromBlack = true;
        this.shouldFadeToBlack = false;
    }
}
