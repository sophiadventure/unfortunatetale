using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public string playerName;
    public float moveSpeed;
    public Animator animator;
    public Rigidbody2D rigidBody;
    public string areaTransitionName; // Exit we JUST used
    public Vector2 startingPosition;
    public bool allowZRotation;

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    public static PlayerController INSTANCE; // this doesn't show up in the UI because it is static

    private void Start()
    {
        Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
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

        if (startingPosition != null)
        {
            INSTANCE.transform.position = startingPosition;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        float moveThreshold = 0.5f;
        bool isRight = horizontalMove > moveThreshold;
        bool isLeft = horizontalMove < -moveThreshold;
        bool isUp = verticalMove > moveThreshold;
        bool isDown = verticalMove < -moveThreshold;
        bool isHorizontalMove = isRight || isLeft;
        bool isVerticalMove = isUp || isDown;
        bool isMove = isHorizontalMove || isVerticalMove;
        bool isDance = Input.GetButton("Fire1");

        // Move the player
        rigidBody.velocity = new Vector2(horizontalMove * moveSpeed, verticalMove * moveSpeed);


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
        this.transform.position = new Vector3(
            Mathf.Clamp(this.transform.position.x, this.bottomLeftLimit.x, this.topRightLimit.x),
            Mathf.Clamp(this.transform.position.y, this.bottomLeftLimit.y, this.topRightLimit.y),
            this.transform.position.z
        );
    }

    void FixedUpdate()
    {

    }

    public void setBounds(Vector3 bottomLeftBound, Vector3 topRightBound)
    {
        CircleCollider2D c = GetComponent<CircleCollider2D>();
        Vector3 offset = GetComponent<Renderer>().bounds.size ;
        this.bottomLeftLimit = bottomLeftBound + new Vector3(offset.x/2, offset.y/2, 0f);
        this.topRightLimit = topRightBound + new Vector3(-offset.x/2, -offset.y/2, 0f);
    }
}
