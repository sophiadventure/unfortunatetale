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
        if(PlayerController.INSTANCE == null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();

            playerController.allowZRotation = allowZRotation;
            Instantiate(player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
