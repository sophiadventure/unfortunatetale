using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{

    private Collider2D thisCollider;
    private Rigidbody2D rigidbody;
    public float moveSpeed = 2;
    private bool isActive = false;

    // Use this for initialization
    void Start()
    {
        this.thisCollider = GetComponentInParent<Collider2D>();
        this.rigidbody = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isActive)
        {
            this.handlePlatformMove();
            PlayerController.INSTANCE.moveWithObject(this.rigidbody);
        }
    }

    // Called when the moving platform collides with another object
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Player")
        {
            this.isActive = true;
            PlayerController.INSTANCE.disableMove();
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (this.isActive)
        {
            if (otherCollider.tag == "Player")
            {
                this.isActive = false;
                PlayerController.INSTANCE.disableMove();
            }
        }
    }

    private void handlePlatformMove()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        this.rigidbody.velocity = new Vector2(horizontalMove * this.moveSpeed, 0);
    }
}
