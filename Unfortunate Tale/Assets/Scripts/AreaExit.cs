using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;


public class AreaExit : MonoBehaviour
{

    public string areaToLoad;
    public string dropLocationName; // Name of this connection

    public float waitToLoad = 1f;
    private bool shouldLoadAfterFade;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLoadAfterFade)
        {
            waitToLoad -= Time.deltaTime;
            if(waitToLoad <= 0)
            {
                shouldLoadAfterFade = false;
                // Load next map
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            this.shouldLoadAfterFade = true;
            UIFade.INSTANCE.FadeToBlack();
            // Tell the static player control which transition just happened
            PlayerController.INSTANCE.dropLocationName = this.dropLocationName;
        }
    }
}
