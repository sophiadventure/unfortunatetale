using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{

    public string dropLocationName; // Name of this connection

    // Start is called before the first frame update
    void Start()
    {
        PlayerController p = PlayerController.INSTANCE;
        if(p != null)
        {
            p.goThroughDoor(this.transform.position);
        }
        //UIFade.INSTANCE.FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
