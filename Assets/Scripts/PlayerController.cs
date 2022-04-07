using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer playerSpriteRenderer;
    public GameObject weapon;
    public GameObject enemy;
    public GameObject ground;
    private Rigidbody playerRb;
    public float speed = 5;
    public bool isOnGround;
    public float jumpForce = 5;
    public bool gameOver;
    private float mapBottom = -5;
    private float attackCoolDown = 0.2f;
    public float playerHp = 100;
    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        weapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHp <= 0)
        {
            gameOver = true;
        }
        if (!gameOver)
        {
            //movement
            float horizontalInput = Input.GetAxis("Horizontal");
                transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

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
                if(facingRight)
                {
                    Turn();
                }

                facingRight = false;
                
            }


            if (Input.GetKey(KeyCode.D)) //facing right
            {
                if (!facingRight)
                {
                    Turn();
                }

                facingRight = true;
                
            }

        }


        if (transform.position.y < mapBottom) //check if player fell off the map
        {
            gameOver = true;
        }



    }

    private void Jump() //jump
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //add force to jump
        isOnGround = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject) //check if on ground
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Enemy")) //check if collided with enemy
        {
            playerHp -= 25;
        }

    }

    IEnumerator AttackCoolDown(float attackCoolDown) //attack cooldown
    {
        
        yield return new WaitForSeconds(attackCoolDown);
        weapon.SetActive(false);
    }

    private void Attack() //attack
    {
        weapon.SetActive(true);
        StartCoroutine(AttackCoolDown(attackCoolDown));
    }

    private void Turn()
    {
        Debug.Log(transform.position.x + " " + playerSpriteRenderer.transform.position.x);
        if (!facingRight)
        {
            playerSpriteRenderer.flipX = false;
            playerSpriteRenderer.transform.position = new Vector3(playerSpriteRenderer.transform.position.x - 0.15f, transform.position.y, transform.position.z);
        }

        if (facingRight)
        {
            playerSpriteRenderer.flipX = true;
            playerSpriteRenderer.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
