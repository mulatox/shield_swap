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

    float speedDash = 8f;

    float dashDuration = 0.2f;

    float dashCoolDown = 0.2f;

    float nextTimeDash = 0;

     private GameObject currentShield;

    bool isBlueShieldActive = false;

    GameObject blueshield;

    GameObject redShield;

    public Sprite shieldSpriteRedShield;

    public  Sprite shieldSpriteBlueShield;

    public Transform spawnShield;

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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateShield();
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


    void CreateShield()
    {
            
            
            if(isBlueShieldActive)
            {
                currentShield = Instantiate(blueshield,spawnShield.position,spawnShield.rotation,spawnShield.transform);
                currentShield.GetComponent<SpriteRenderer>().sprite = shieldSpriteBlueShield;
            }
            else
            {
                currentShield = Instantiate(redShield,spawnShield.position,spawnShield.rotation,spawnShield.transform);
                currentShield.GetComponent<SpriteRenderer>().sprite = shieldSpriteRedShield;
            }

            isBlueShieldActive = !isBlueShieldActive;

    }
    
}
