using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject weapon;
    public GameObject enemy;
    public GameObject ground;
    private Rigidbody playerRb;
    public GameObject playerSprite;
    public SpriteRenderer playerSpriteRenderer;
    private BoxCollider playerBoxCollider;
    public float speed = 5;
    public bool isOnGround;
    public float jumpForce = 5;
    public bool gameOver;
    private float mapBottom = -5;
    private float attackCoolDown = 0.2f;
    public float playerHp = 100;
    private bool enemyCollision;
    public bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerBoxCollider = GetComponent<BoxCollider>();
        weapon.SetActive(false);
        playerSprite.transform.position = transform.position;
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
                facingRight = true;

            }


            if (Input.GetKey(KeyCode.D)) //facing right
            {
                playerSpriteRenderer.flipX = false;
                facingRight = false;

            }

            if (!facingRight)
            {
                playerSprite.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }

            else
            {
                playerSprite.transform.position = new Vector3(transform.position.x - 0.22f, transform.position.y, transform.position.z);
            }
        }


        if (transform.position.y < mapBottom) //check if player fell off the map
        {
            gameOver = true;
        }

        if (enemyCollision)
        {
            playerHp -= 25;
        }


    }

    void FixedUpdate()
    {
        //movement
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.fixedDeltaTime);
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
            enemyCollision = true;
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

}
