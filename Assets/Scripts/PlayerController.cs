using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject weapon;
    public GameObject enemy;
    private Rigidbody playerRb;
    public GameObject playerSprite;
    public SpriteRenderer playerSpriteRenderer;
    public Animator playerAnimator;
    public GameObject enemyWeapon;
    public Enemy enemyScript;
    public float speed = 5;
    public bool isOnGround;
    public float jumpForce = 5;
    public bool gameOver;
    private float mapBottom = -5;
    private float attackCoolDown = 2f;
    public float playerHp = 100;
    private bool enemyCollision;
    public bool facingLeft;
    private float jumpCoolDown = 5f;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = playerAnimator.GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        weapon.SetActive(false);
        playerSprite.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHp <= 0)
        {
            gameOver = true;
            playerAnimator.SetBool("dead", true);
        }
        if (!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround) //jump with space
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.Q)) // attack with q
            {
                Attack();
                
            }

            if (Input.GetKey(KeyCode.A)) //facing left
            {
                playerSpriteRenderer.flipX = true;
                facingLeft = true;
                playerAnimator.SetBool("run", true);
            }


            if (Input.GetKey(KeyCode.D)) //facing right
            {
                playerSpriteRenderer.flipX = false;
                facingLeft = false;
                playerAnimator.SetBool("run", true);
            }

            if(!facingLeft)
            {
                playerSprite.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }

            else if (facingLeft)
            {
                playerSprite.transform.position = new Vector3(transform.position.x -0.22f, transform.position.y, transform.position.z);
            }

            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKeyDown(KeyCode.Q) && !Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerAnimator.SetBool("run", false);
            }
        }


        if (transform.position.y < mapBottom) //check if player fell off the map
        {
            gameOver = true;
        }

        if (enemyCollision)
        {
            playerHp -= 0;
        }



    }

    private void FixedUpdate()
    {
        if (!gameOver)
        {
            //movement
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * speed * Time.fixedDeltaTime);
        }
    }
    private void Jump() //jump
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //add force to jump
        isOnGround = false;
        playerAnimator.SetBool("isOnGround", false);
        playerAnimator.SetTrigger("jump");

        


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject) //check if on ground
        {
            isOnGround = true;
            playerAnimator.SetBool("isOnGround", true);
            StartCoroutine(JumpCoolDown(jumpCoolDown));
        }

        if (collision.gameObject.CompareTag("Enemy")) //check if collided with enemy
        {
            enemyCollision = true;
            playerAnimator.SetTrigger("enemyCollision");
        }

    }

    IEnumerator AttackCoolDown(float attackCoolDown) //attack cooldown
    {
        
        yield return new WaitForSeconds(attackCoolDown);

        
        


    }

    private void Attack() //attack
    {
        if (facingLeft)
        {
            weapon.transform.position = new Vector3(transform.position.x - 0.33f, transform.position.y - 0.1f, transform.position.z);
        }

        else if (!facingLeft)
        {
            weapon.transform.position = new Vector3(transform.position.x + 0.11f, transform.position.y - 0.1f, transform.position.z);
        }
        weapon.SetActive(true);
        playerAnimator.SetTrigger("attack");
        weapon.SetActive(false);
    }

    IEnumerator JumpCoolDown(float jumpCoolDown) //attack cooldown
    {
        yield return new WaitForSeconds(jumpCoolDown);
    }

}
