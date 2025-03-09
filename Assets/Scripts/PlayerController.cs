using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{

    public DashEffect dashEffect;

    public float speed = 5f;
    private Rigidbody2D rb;
    Vector2 playerMovement;
    float moveX;
    float moveY;

    public float speedDash = 8f;

    float dashDuration = 0.2f;

    float dashCoolDown = 0.2f;

    float nextTimeDash = 0;

     private GameObject currentShield;

    bool isBlueShieldActive = false;

    public GameObject blueshield;

    public GameObject redShield;

    public Sprite shieldSpriteRedShield;

    public  Sprite shieldSpriteBlueShield;

    public Transform spawnShield;

    bool isDashing = false;

    public AudioClip dashSound; //  Som do Dash
    
    public AudioClip damageSound; //  Som ao Receber Dano

    public AudioClip deathSound; //  Som ao Receber Dano

     private AudioSource audioSource;

    public TextMeshProUGUI healthPlayerText;
    float healthPlayer = 10;

    public GameOverController gameOverController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        audioSource = GetComponent<AudioSource>();
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
        if (healthPlayer <= 0){
            return;
        }
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
    nextTimeDash = Time.time + dashCoolDown;
    rb.linearVelocity = playerMovement * speedDash;

    dashEffect.StartDashEffect(); // ✅ Inicia o efeito fantasma
    PlaySound(dashSound); //
    Invoke("StopDash", dashDuration);
}

void StopDash()
{
    rb.linearVelocity = Vector2.zero;
    isDashing = false;
    
    dashEffect.StopDashEffect(); // ✅ Para o efeito fantasma
}

    


    void CreateShield()
    {
            
            if(currentShield != null)
            {
                Destroy(currentShield);
            }
            
            if(isBlueShieldActive)
            {
                currentShield = Instantiate(redShield,spawnShield.position,spawnShield.rotation,spawnShield.transform);
                currentShield.GetComponent<SpriteRenderer>().sprite = shieldSpriteRedShield;
            }
            else
            {
                currentShield = Instantiate(blueshield,spawnShield.position,spawnShield.rotation,spawnShield.transform);
                currentShield.GetComponent<SpriteRenderer>().sprite = shieldSpriteBlueShield;
            }

            isBlueShieldActive = !isBlueShieldActive;

    }

    // ✅ Método para tomar dano quando o player for atingido
    public void TomarDano()
    {
        healthPlayer--; // Reduz a vida do player
        AtualizarHUD(); // Atualiza o texto na tela

        // Efeito de piscar ao ser atingido
        StartCoroutine(PiscarDano());

        if (healthPlayer <= 0)
        {
            PlaySound(deathSound); //
            Debug.Log("Player morreu!");
             gameOverController.AtivarGameOver();
            // Aqui você pode chamar uma tela de Game Over ou reiniciar a cena
        }
        PlaySound(damageSound);
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip); // Toca o som sem cortar o anterior
        }
    }

    // Atualiza o HUD da vida do jogador
    private void AtualizarHUD()
    {
        healthPlayerText.text = healthPlayer.ToString();
    }

    // ✅ Efeito de piscar ao tomar dano
    private IEnumerator PiscarDano()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < 3; i++) // Pisca 3 vezes
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f); // Transparente
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white; // Volta ao normal
            yield return new WaitForSeconds(0.1f);
        }
    }

    // ✅ Detecta se o player foi atingido por um tiro do monstro
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TiroBlue")) // Se for um tiro azul
        {
            if (!isBlueShieldActive) // Só causa dano se o BlueShield NÃO estiver ativo
            {
                Debug.Log("Player atingido pelo Tiro Azul!");
                TomarDano();
            }
            else
            {
                Debug.Log("BlueShield bloqueou o tiro azul!");
            }
            Destroy(collision.gameObject); // O tiro sempre desaparece ao colidir
        }
        else if (collision.CompareTag("TiroRed")) // Se for um tiro vermelho
        {
            if (isBlueShieldActive) // Só causa dano se o RedShield NÃO estiver ativo
            {
                Debug.Log("Player atingido pelo Tiro Vermelho!");
                TomarDano();
            }
            else
            {
                Debug.Log("RedShield bloqueou o tiro vermelho!");
            }
            Destroy(collision.gameObject); // O tiro sempre desaparece ao colidir
        }
    }
    
}
