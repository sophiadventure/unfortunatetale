using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    public GameObject player;
    public bool allowZRotation = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject entrance = GameObject.Find("Area Entrance");

        if(PlayerController.INSTANCE == null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (entrance != null)
            {
                playerController.startingPosition = entrance.transform.position;
            }

            playerController.allowZRotation = allowZRotation;
            Instantiate(player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
