using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    Animator anim;
    Rigidbody2D rb;

    public float speedX;
    public float jumpSpeedY;
    public int lives = 3;
    public Text coinText;
    private int count;

    bool OnGround;
    bool facingRight, Jumping;
    float speed;

	
	void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;
        count = 0;
        setCoin();
	}

    void setCoin()
    {
        coinText.text = count.ToString();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }


    void Update ()
    {
        
        MovePlayer(speed);
        Flip();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            speed = -speedX;
            
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            speed = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            speed = speedX;
            
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            speed = 0;
        }

        if (OnGround && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jumping = true;
            rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));
            anim.SetInteger("State", 2);
        }
        

    }

    void MovePlayer(float playerSpeed)
    {
        if (playerSpeed < 0 && !Jumping || playerSpeed > 0 && !Jumping)
        {
            anim.SetInteger("State", 1);
        }
        if (playerSpeed == 0 && !Jumping)
        {
            anim.SetInteger("State", 0); 
        }

        rb.velocity = new Vector3(speed, rb.velocity.y, 0);
    }

    void Flip()
    {
        if (speed > 0 && !facingRight || speed < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            Jumping = false;
            anim.SetInteger("State", 0);
        }
    }

    public void WalkLeft()
    {
        speed = -speedX;
    }

    public void WalkRight()
    {
        speed = speedX;
    }

    public void StopMoving()
    {
        speed = 0;
    }

    public void Jump()
    {
        if (OnGround)
        {
            Jumping = true;
            rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));
            anim.SetInteger("State", 2);
        }
    }

    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        OnGround = colliders.Length > 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            lives--;
            setLose();
        }

        if (other.tag == "Coin")
        {            
            Destroy(other.gameObject);
            count++;
            setCoin();
        }

        if (other.tag == "Life")
        {
            lives++;
            Destroy(other.gameObject);
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(5, 5, 150, 50), "Life = " + lives);
    }

    void setLose()
    {
        if (lives == 0)
        {
            SceneManager.LoadScene(6);
        }
    }

    

}
