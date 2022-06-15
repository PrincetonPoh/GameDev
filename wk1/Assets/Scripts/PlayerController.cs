using System.Timers;
using System.Threading;
using System.Globalization;
using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameConstants gameConstants;
    public float speed;
    private Rigidbody2D marioBody;
    public float maxSpeed;
    private bool onGroundState = true;
    public float upSpeed;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;
    public GameObject restartButton;

    private  Animator marioAnimator;
    private AudioSource marioAudio;

    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        
        // get components
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator  =  GetComponent<Animator>();
        marioAudio  =  GetComponent<AudioSource>();

        restartButton.gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        // always update animatior's params to match Mario's current params along x-axis
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        marioAnimator.SetBool("onGround", onGroundState);
        
        // toggle state
        if (Input.GetKeyDown("a") && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;
            
            if (Mathf.Abs(marioBody.velocity.x) >  1.0) marioAnimator.SetTrigger("onSkid");
        }

        if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;

            if (Mathf.Abs(marioBody.velocity.x) >  1.0) marioAnimator.SetTrigger("onSkid");
        }
        
        
    }

    void FixedUpdate()
    {
        // dynamic rigidbody
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
                    marioBody.AddForce(movement * speed);
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
            // stop
            marioBody.velocity = Vector2.zero;
        }
        if (Input.GetKeyDown("space") && onGroundState){
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            countScoreState = true;
        }
        }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        { 
            onGroundState = true; // back on ground
            countScoreState = false; // reset score state
            scoreText.text = "Score: " + score.ToString();
            GameObject.Find("Dust").GetComponent<ParticleSystem>().Play();
        };

        if (col.gameObject.CompareTag("Obstacle") && MathF.Abs(marioBody.velocity.y) < 0.01f)
        { 
            onGroundState = true; // back on ground
            marioAnimator.SetBool("onGround", onGroundState);
        };
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Goomba!");
            Time.timeScale = 0.0f;
            restartButton.gameObject.SetActive(true);
        }
    }

    void  PlayJumpSound(){
        marioAudio.PlayOneShot(marioAudio.clip);
    }
}