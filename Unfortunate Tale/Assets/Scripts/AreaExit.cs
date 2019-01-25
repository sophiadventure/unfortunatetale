using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;


public class AreaExit : MonoBehaviour
{

    public string areaToLoad;
    public string areaTransitionName; // Name of this connection

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // Load next map
            SceneManager.LoadScene(areaToLoad);
            // Tell the static player control which transition just happened
            PlayerController.INSTANCE.areaTransitionName = this.areaTransitionName;
        }
    }
}
