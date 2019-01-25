using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        GameObject entrance = GameObject.Find("Area Entrance");

        if(PlayerController.INSTANCE == null)
        {
            if (entrance != null)
            {
                player.GetComponent<PlayerController>().startingPosition = entrance.transform.position;
            }
            Instantiate(player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
