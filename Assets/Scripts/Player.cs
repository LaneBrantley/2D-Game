using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

   //Allows vars to be edited in inspector panel in Unity UI
   [SerializeField]
    private float minX, maxX;
    private Transform player;
    private AudioSource audioSource;

    private Vector3 tempPos;
    private float moveForce = 2f;
    private float tempMoveForce;
    
    private float jumpForce = 3f;
    public int life = 3;

    private float movementX;
    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private Animator anim;
    private string WALK_ANIMATION = "Walk";
    private string JUMP_ANIMATION = "Jump";

    private bool isGrounded = true;
    private string GROUND_TAG = "Ground";
    private void Awake() {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
    }

    void LateUpdate() {
        {
        tempPos = transform.position;
        tempPos.x = player.position.x;

        if (tempPos.x < minX) {
            tempPos.x = minX;
        }
        if (tempPos.x > maxX) {
            tempPos.x = maxX;
        }
        transform.position = tempPos;
        }
    }

    void PlayerMoveKeyboard() {
        movementX = Input.GetAxisRaw("Horizontal");
        myBody.velocity = new Vector2(movementX * moveForce, myBody.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            isGrounded = false;
            myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
        }


    }

    void AnimatePlayer() {
        if (movementX > 0) {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        } else if (movementX < 0) {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        } else {
            anim.SetBool(WALK_ANIMATION, false);
        }

        if (Input.GetButtonDown("Jump")) {
            anim.SetBool(JUMP_ANIMATION, true);
            audioSource.Play();
        }
    }

    private IEnumerator SlowDownCoroutine() { //coroutine to slow character
        moveForce = 1f;
        yield return new WaitForSeconds(3f);
        moveForce = 2f;
    }

    private IEnumerator StuckCoroutine() {
        tempMoveForce = moveForce;
        moveForce = 0;
        yield return new WaitForSeconds(4f);
        moveForce = tempMoveForce;
    }

    private Vector2 pointOfContact; //used to detect side of collision with spikes/other obstacles
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(GROUND_TAG) || collision.gameObject.CompareTag("Moving Plat")) {
            pointOfContact = collision.contacts[0].normal;
            if (pointOfContact == new Vector2(0,1)) { //if touched the top of vector
                anim.SetBool(JUMP_ANIMATION, false);
                isGrounded = true;
            }
        } else if (collision.gameObject.CompareTag("Spike")) {
            pointOfContact = collision.contacts[0].normal;
            if (pointOfContact == new Vector2(0,1)) { //if touched the top of vector
                life -= 1;
                isGrounded = true;
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Snare") || collision.gameObject.CompareTag("Water")) {
            StartCoroutine(SlowDownCoroutine());
        } else if (collision.gameObject.CompareTag("Level 1 Door")) {
            SceneManager.LoadScene("Level2");
            maxX = 15.8f;
        } else if (collision.gameObject.CompareTag("Level 2 Door")) {
            SceneManager.LoadScene("Level3");
            maxX = 17.5f;
        } else if (collision.gameObject.CompareTag("Lava")) {
            life -= 2;
        } else if (collision.gameObject.CompareTag("Bear Trap")) {
            StartCoroutine(StuckCoroutine());
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Staff")) {
            SceneManager.LoadScene("MainMenu");
        }
    }

} // class
