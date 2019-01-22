using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public string playerName;
    public float moveSpeed;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        bool isRight = horizontalMove > 0.5f;
        bool isLeft = horizontalMove < -0.5f;
        bool isUp = verticalMove > 0.5f;
        bool isDown = verticalMove < -0.5f;
        bool isHorizontalMove = isRight || isLeft;
        bool isVerticalMove = isUp || isDown;
        // bool isMove = isHorizontalMove || isVerticalMove;
        float verticalTranslate = verticalMove * moveSpeed * Time.deltaTime;
        float horizontalTranslate = horizontalMove * moveSpeed * Time.deltaTime;

        if (isHorizontalMove)
        {
            transform.Translate(new Vector3(horizontalMove, 0f, 0f));
        }
        if (isVerticalMove)
        {
            transform.Translate(new Vector3(0f, verticalMove, 0f));
        }

        animator.SetFloat("MoveX", horizontalMove);
        animator.SetFloat("MoveY", verticalMove);

    }

    void FixedUpdate()
    {
        
    }
}
