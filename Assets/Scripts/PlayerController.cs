using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject weapon;
    public GameObject enemy;
    public GameObject ground;
    private Rigidbody playerRb;
    public float speed = 10;
    public bool isOnGround;
    public float jumpForce = 10;
    public bool gameOver;
    private float mapBottom = -5;
    private float attackCoolDown = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        weapon = GameObject.Find("Weapon");
        weapon.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //movement
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space) && isOnGround) //jump with space
        {
            Jump();
        }

        if (transform.position.y < mapBottom) //check if player fell off the map
        {
            gameOver = true;
        }


        if (Input.GetKeyDown(KeyCode.Q)) // attack with q
        {
            weapon.SetActive(true);
            StartCoroutine(AttackCoolDown(attackCoolDown));
        }

    }

    private void Jump()
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
            gameOver = true;
        }

    }

    IEnumerator AttackCoolDown(float attackCoolDown) //attack cooldown
    {
        yield return new WaitForSeconds(attackCoolDown);

        weapon.SetActive(false);
    }

}
