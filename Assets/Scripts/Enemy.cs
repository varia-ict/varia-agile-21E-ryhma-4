using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject player;
    public GameObject weapon;
    public SpriteRenderer spriteRenderer;
    public GameObject enemySprite;
    public Animator enemyAnimator;
    public PlayerController playerControllerScript;
    public Rigidbody playerRb;
    public float enemySpeed = 0.5f;
    public float enemyRange = 2f;
    private float enemyAttackCoolDown = 1f;
    private bool facingLeft;
    public float enemyAttackRange = 0.5f;
    public bool playerInRange;
    public bool playerCollision;
    public float playerKickBack = 1.5f;
    public float enemyFlipOffset = 0.31f;
    public float enemyHp = 50f;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = enemyAnimator.GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerRb = player.GetComponent<Rigidbody>();
        weapon.SetActive(false);
        enemySprite.transform.position = transform.position;

    }
        // Update is called once per frame
        void Update()
        {

            if (!playerControllerScript.gameOver) //check if game not over
            {
                if (player.transform.position.x < transform.position.x + enemyRange) //walk at player if in range
                {
                    Vector3 enemyVector = transform.position - player.transform.position;
                    transform.Translate(enemySpeed * Time.deltaTime * -enemyVector.normalized);
                }

                if (transform.position.x < player.transform.position.x) //flip sprite to look at player
                {
                    spriteRenderer.flipX = false;
                    facingLeft = false;
                }
                else  //flip sprite to look at player
                {
                    spriteRenderer.flipX = true;
                    facingLeft = true;

                }

                if (player.transform.position.x > transform.position.x - enemyAttackRange && player.transform.position.x < transform.position.x + enemyAttackRange) //check if player in attack range
                {
                    playerInRange = true;
                    EnemyAttack();
                    enemyAnimator.SetBool("playerInRange", true);
                }

                else
                {
                    playerInRange = false;
                    enemyAnimator.SetBool("playerInRange", false);
                }


                
            }

        if (!facingLeft) //set sprite postition to hitbox position
        {
            enemySprite.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        else if (facingLeft) //set sprite postition to hitbox position
        {
            enemySprite.transform.position = new Vector3(transform.position.x - enemyFlipOffset, transform.position.y, transform.position.z);
        }

    }
    IEnumerator EnemyAttackCoolDown(float enemyAttackCoolDown) //enemy attack cooldown
    {
        yield return new WaitForSeconds(enemyAttackCoolDown);
    }

    private void EnemyAttack() //attack
    {
        if (facingLeft) //flip weapon left
        {
            weapon.transform.position = new Vector3(transform.position.x - 0.32f, transform.position.y - 0.2f, transform.position.z);
            enemySprite.transform.position = new Vector3(transform.position.x - 0.22f, transform.position.y, transform.position.z);
        }

        if (!facingLeft) //flip weapon right
        {
            weapon.transform.position = new Vector3(transform.position.x + 0.09f, transform.position.y - 0.2f, transform.position.z);
            enemySprite.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
        weapon.SetActive(true);
        enemyAnimator.SetTrigger("attack");

        weapon.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player")) //check if collided with player
        {
            playerCollision = true;
            enemyAnimator.SetTrigger("playerCollision");
            playerControllerScript.playerHp -= 25;


            playerRb.AddForce(new Vector3(player.transform.position.x - transform.position.x, transform.position.y, transform.position.z) * playerKickBack, ForceMode.Impulse);


            



        }


    }
}