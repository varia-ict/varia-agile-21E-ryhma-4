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
    private bool hasPlayerPosition;
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
    private Rigidbody enemyRB;
    private Animator enemyAnim;




    void Start()
    {
        idleMoveDirection.Normalize();
        attackMoveDirection.Normalize();
        enemyRB = GetComponent<Rigidbody>();
        enemyAnim = GetComponent<Animator>();
    }

    void Update()
    {
        isTouchingUp = Physics2D.OverlapCircle(groundCheckUp.position, groundCheckRadius, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(groundCheckDown.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(groundCheckWall.position, groundCheckRadius, groundLayer);
        
        IdleState();
        AttackUpNDown();       
        FlipTowardsPlayer();

    }
   
    
    //Randomly selects between idle and attack animation
    void randomStatePicker()
    {
        int randomState = Random.Range(0, 2);
        if (randomState == 1)
        {
            //attackupndown animation
           enemyAnim.SetTrigger("AttackUpNDown");

        }
    }


    
    public void IdleState()
    {
        //changes direction of movement upon hitting the walls
        if(isTouchingUp && goingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !goingUp)
        {
            ChangeDirection();
        }
        //flips boss upon hitting walls
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
        //changes direction of movement upon hitting the walls
        if (isTouchingUp && goingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !goingUp)
        {
            ChangeDirection();
        }

        //flips boss upon hitting walls
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

    //Not in use
    public void AttackPlayer()
    {
        if (!hasPlayerPosition)
        {
            playerPosition = player.position - transform.position;
            playerPosition.Normalize();
            hasPlayerPosition = true;
        }
        if (hasPlayerPosition)
        {
            enemyRB.velocity = playerPosition * attackPlayerSpeed;
        }
        if (isTouchingWall || isTouchingDown)
        {
            enemyRB.velocity = Vector2.zero;
            hasPlayerPosition = false;
            enemyAnim.SetTrigger("Slam");
        }


    }

    //makes the boss face the player
    void FlipTowardsPlayer()
    {
        float playerDirection = player.position.x - transform.position.x;

        if (playerDirection > 0 && facingLeft)
        {
            Flip();
        }
        else if (playerDirection < 0 && !facingLeft)
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

    //draws gizmos on scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheckUp.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckDown.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckWall.position, groundCheckRadius);
    }

    //destroys player on collision
    private void OnTriggerEnter(Collider collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.name == "Player")
        {
            Destroy(collisionGameObject);
        }
    }
}
