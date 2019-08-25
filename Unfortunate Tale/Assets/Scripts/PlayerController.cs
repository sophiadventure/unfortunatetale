using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public string playerName;
    public float moveSpeed;
    public Animator animator;
    public Rigidbody2D rigidBody;
    public string dropLocationName; // Exit we JUST used
    public bool allowZRotation;
    public bool canMove = true;

    private bool justEntered = false;

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    private Vector2 mapStartLocation;

    private bool isMovedExternally = false;
    public static PlayerController INSTANCE; // this doesn't show up in the UI because it is static

    private void Start()
    {
        Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
        AreaEntrance entrance = FindObjectOfType<AreaEntrance>();

        rb.constraints = allowZRotation ? RigidbodyConstraints2D.None : RigidbodyConstraints2D.FreezeRotation;

        // Only instantiate when the game starts running
        if(INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if(entrance != null)
        {
            this.goThroughDoor(entrance.transform.position);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");



        this.handlePlayerEntrance();

        if (!this.isMovedExternally)
        {
            // Move the player
            rigidBody.velocity = new Vector2(horizontalMove * moveSpeed, verticalMove * moveSpeed);
        }

        this.handleanimator();
    }

    private void handleanimator()
    {
        float moveThreshold = 0.5f;
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");
        bool isRight = horizontalMove > moveThreshold;
        bool isLeft = horizontalMove < -moveThreshold;
        bool isUp = verticalMove > moveThreshold;
        bool isDown = verticalMove < -moveThreshold;
        // No move as far as animator is concerned if we are moving externally (e.g. platform)
        bool isHorizontalMove = !this.isMovedExternally && (isRight || isLeft);
        bool isVerticalMove = !this.isMovedExternally && (isUp || isDown);
        bool isMove = isHorizontalMove || isVerticalMove;
        bool isDance = Input.GetButton("Fire1");

        // Set animator variables below
        if (isMove)
        {
            // The 2 variables below are used to determine the orientation of the player
            // Only set these two when the player is actually moving (not to 0, so we can remember orientation)
            animator.SetFloat("LastMoveX", horizontalMove);
            animator.SetFloat("LastMoveY", verticalMove);
        }

        animator.SetFloat("MoveX", horizontalMove);
        animator.SetFloat("MoveY", verticalMove);
        animator.SetBool("IsPlayerMoving", isMove);
        animator.SetBool("IsPlayerDancing", isDance);
    }

    private void LateUpdate()
    {
        // Keep player within bounds
        //this.transform.position = new Vector3(
        //    Mathf.Clamp(this.transform.position.x, this.bottomLeftLimit.x, this.topRightLimit.x),
        //    Mathf.Clamp(this.transform.position.y, this.bottomLeftLimit.y, this.topRightLimit.y),
        //    this.transform.position.z
        //);
    }

    void FixedUpdate()
    {

    }


    // Call this from other scripts 
    // Player went through a doorway of some kind
    public void goThroughDoor(Vector2 mapStartLocation)
    {
        this.mapStartLocation = mapStartLocation;
        this.justEntered = true;
    }

    // Handle the player going through a doorway of some kind
    // Called by the player update loop
    private void handlePlayerEntrance()
    {
        if (this.justEntered)
        {
            INSTANCE.transform.position = this.mapStartLocation;
            this.justEntered = false;
        }
    }


    public void setBounds(Vector3 bottomLeftBound, Vector3 topRightBound)
    {
        CircleCollider2D c = GetComponent<CircleCollider2D>();
        Vector3 offset = GetComponent<Renderer>().bounds.size ;

        // Prevent mina head from going off screen
        this.bottomLeftLimit = bottomLeftBound + new Vector3(offset.x/2, offset.y/2, 0f);
        this.topRightLimit = topRightBound + new Vector3(-offset.x/2, -offset.y/2, 0f);
    }

    // Make the player unable to move
    public void disableMove()
    {
        this.canMove = false;
    }

    // Make the player able to move
    public void enableMove()
    {
        this.canMove = true;
        this.isMovedExternally = false;
    }

    // Move the player to where the collider provided is
    public void moveWithObject(Rigidbody2D other)
    {
        this.isMovedExternally = true;
        this.rigidBody.velocity = other.velocity;
    }
}
