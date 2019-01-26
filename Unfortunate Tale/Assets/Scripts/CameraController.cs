using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public int PPU = 16;
    public Tilemap theMap;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    private float halfHeight;
    private float halfWidth;

    // For camera issues look at https://hackernoon.com/making-your-pixel-art-game-look-pixel-perfect-in-unity3d-3534963cad1d
    // https://blogs.unity3d.com/2015/06/19/pixel-perfect-2d/
    // https://www.youtube.com/watch?v=0bgux-JWnRQ
    // https://gamedev.stackexchange.com/questions/114150/how-do-i-snap-the-unity-camera-to-a-2d-tile-based-map
    // Keywords for solutions: warp, flicker, show seams, snap to grid


    private void Start()
    {
        this.halfHeight = Camera.main.orthographicSize;
        this.halfWidth = this.halfHeight * Camera.main.aspect;


        // halfHeight and halfWidth adjustment are there to prevent camera from showing anything out of bounds 
        this.bottomLeftLimit = theMap.localBounds.min + new Vector3(this.halfWidth, this.halfHeight, 0f);
        this.topRightLimit = theMap.localBounds.max + new Vector3(-this.halfWidth, -this.halfHeight, 0f);

        PlayerController.INSTANCE.setBounds(theMap.localBounds.min, theMap.localBounds.max);
    }


    // LateUpdate is called once per frame after
    // Use this to prevent camera stuttering because of physics calculations
    void LateUpdate()
    {
        PlayerController player = PlayerController.INSTANCE;
        target = player ? player.transform : null;
        if (target)
        {
            //this.transform.position = new Vector3(target.position.x, target.position.y, this.transform.position.z);
            float x = (Mathf.Round(target.position.x * PPU) / PPU);
            float y = (Mathf.Round(target.position.y * PPU) / PPU);
            this.transform.position = new Vector3(x, y, this.transform.position.z);

            // Keep camera within bounds
            this.transform.position = new Vector3(
                Mathf.Clamp(this.transform.position.x, this.bottomLeftLimit.x, this.topRightLimit.x),
                Mathf.Clamp(this.transform.position.y, this.bottomLeftLimit.y, this.topRightLimit.y),
                this.transform.position.z
            );
        }
    }

}
