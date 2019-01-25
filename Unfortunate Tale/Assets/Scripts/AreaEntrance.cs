using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{

    public string areaTransitionName; // Name of this connection

    // Start is called before the first frame update
    void Start()
    {
        PlayerController i = PlayerController.INSTANCE;
        if(i != null)
        {
            if (areaTransitionName == i.areaTransitionName)
            {
                i.transform.position = this.transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
