using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{

    public string areaToLoad;
    public string otherTag;

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

        //otherTag = other.tag;        
        //if(other.tag == "Player")
        //{
        //    SceneManager.LoadScene(areaToLoad);
        //}
    }
}
