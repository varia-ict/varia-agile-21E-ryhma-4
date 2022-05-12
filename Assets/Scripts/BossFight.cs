using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    //For Idle Stage
    [Header("Idle")]
    [SerializeField] float idleMoveSpeed;
    [SerializeField] Vector2 idleMoveDirection;
    // For Attack Up and Down Stage
    [Header("AttackUpNDown")]
    [SerializeField] float attackMoveSpeed;
    [SerializeField] Vector2 attackMoveDirection;
    // For Attack Player Stage
    [Header("AttackPlayer")]
    [SerializeField] float attackPlayerSpeed;
    [SerializeField] Transform player;
    private Vector2 playerPosition;
    // Other
    [Header("Other")]
    [SerializeField] Transform groundCheckUp;
    [SerializeField] Transform groundCheckDown;
    [SerializeField] Transform groundCheckWall;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    private bool isTouchingUp;
    private bool isTouchingDown;
    private bool isTouchingWall;
    private bool goingUp = true;
    private bool facingLeft = true;
    private Rigidbody2D enemyRB;





    void Start()
    {
        idleMoveDirection.Normalize();
        attackMoveDirection.Normalize();
        enemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingUp = Physics2D.OverlapCircle(groundCheckUp.position, groundCheckRadius, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(groundCheckDown.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(groundCheckWall.position, groundCheckRadius, groundLayer);
        IdleState();
        AttackUpNDown();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttackPlayer();
        }
        FlipTowardsPlayer();
    }

    public void IdleState()
    {
        if(isTouchingUp && goingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !goingUp)
        {
            ChangeDirection();
        }

        if (isTouchingWall)
        {
            if (facingLeft)
            {
                Flip();
            }
            else if (!facingLeft)
            {
                Flip();
            }
        }
        enemyRB.velocity = idleMoveSpeed * idleMoveDirection;
    }    
    
    public void AttackUpNDown()
    {
        if(isTouchingUp && goingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !goingUp)
        {
            ChangeDirection();
        }

        if (isTouchingWall)
        {
            if (facingLeft)
            {
                Flip();
            }
            else if (!facingLeft)
            {
                Flip();
            }
        }
        enemyRB.velocity = attackMoveSpeed * attackMoveDirection;
    }

    public void AttackPlayer()
    {
        //take player position
        playerPosition = player.position - transform.position;
        //normalize player position
        playerPosition.Normalize();
        //attack on that position
        enemyRB.velocity = playerPosition * attackPlayerSpeed;

    }

    void FlipTowardsPlayer()
    {
        float playerDirection = player.position.x - transform.position.x;

        if(playerDirection>0 && facingLeft)
        {
            Flip();
        }
        else if (playerDirection<0 && !facingLeft)
        {
            Flip();
        }
    }

    void ChangeDirection()
    {
        goingUp = !goingUp;
        idleMoveDirection.y *= -1;
        attackMoveDirection.y *= -1;
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        idleMoveDirection.x *= -1;
        attackMoveDirection.x *= -1;
        transform.Rotate(0, 180, 0);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheckUp.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckDown.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckWall.position, groundCheckRadius);
    }

}
