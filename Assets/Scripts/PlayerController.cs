using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    Vector2 playerMovement;
    float moveX;
    float moveY;

    float speedDash = 20f;

    float dashDuration = 0.2f;

    float dashCoolDown = 1;

    float nextTimeDash = 0;

    bool isDashing = false;

    public TextMeshProUGUI healthPlayerText;
    float healthPlayer = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.Mouse1) && Time.time >= nextTimeDash)
        {
            Dash();
            healthPlayerText.text = healthPlayer.ToString();
        }
    }
    void FixedUpdate()
    {
        
        moveX = Input.GetAxisRaw("Horizontal");
        
        
        moveY = Input.GetAxisRaw("Vertical");
        
        playerMovement = new Vector2(moveX,moveY);
        
        playerMovement.Normalize();

       if(!isDashing)
       {
        rb.MovePosition(rb.position + playerMovement * speed * Time.fixedDeltaTime);

       }

    }

    void Dash()
    {
        isDashing = true;
        //Vector2 dashDirection = new Vector2(moveX,moveY);
        nextTimeDash = Time.time + dashCoolDown;
        rb.linearVelocity = playerMovement * speedDash;
        Invoke("StopDash",dashDuration);
        healthPlayer--;
    }

    void StopDash()
    {
        rb.linearVelocity = Vector2.zero;
        isDashing = false;
    }
}
