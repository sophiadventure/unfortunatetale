using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // LateUpdate is called once per frame after
    // Use this to prevent camera stuttering because of physics calculations
    void LateUpdate()
    {
        PlayerController player = PlayerController.INSTANCE;
        target = player ? player.transform : null;
        if (target)
        {
            this.transform.position = new Vector3(target.position.x, target.position.y, this.transform.position.z);
        }
    }
}
