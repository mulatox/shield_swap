using System.Collections;
using UnityEngine;

public class inimigo2Script : MonoBehaviour
{
    public int vida = 3; // Monstro morre após ser atingido 3 vezes
    public GameObject player; // Referência ao jogador
    public GameObject tiroPrefab; // Prefab da bolinha de tiro
    public float velocidade = 2f; // Velocidade do monstro
    public float tempoEntreTiros = 2f; // Tempo entre os ataques
    public float raioCirculo = 1.5f; // Raio do movimento circular
    public float velocidadeRotacao = 2f; // Velocidade de rotação
    private Rigidbody2D rb;
    private float angulo; // Ângulo para movimento circular
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Pegamos o SpriteRenderer para o efeito de piscar
        InvokeRepeating(nameof(Atirar), 1f, tempoEntreTiros);
    }

    private void Update()
    {
        if (player != null)
        {
            // Direção para o jogador
            Vector2 direcao = (player.transform.position - transform.position).normalized;

            // Atualiza o ângulo para criar um movimento circular
            angulo += velocidadeRotacao * Time.deltaTime;
            float deslocamentoX = Mathf.Cos(angulo) * raioCirculo;
            float deslocamentoY = Mathf.Sin(angulo) * raioCirculo;

            // Calcula a posição final combinando a direção ao player + deslocamento circular
            Vector2 novaPosicao = (Vector2)player.transform.position - direcao * raioCirculo + new Vector2(deslocamentoX, deslocamentoY);

            // Move o inimigo para a nova posição
            rb.MovePosition(Vector2.Lerp(transform.position, novaPosicao, velocidade * Time.deltaTime));
        }
    }

    private void Atirar()
    {
        if (player == null) return;

        for (int i = 0; i < 3; i++) // Disparar 3 bolinhas
        {
            GameObject tiro = Instantiate(tiroPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rbTiro = tiro.GetComponent<Rigidbody2D>();

            if (rbTiro != null)
            {
                Vector2 direcao = (player.transform.position - transform.position).normalized;
                float angulo = (i - 1) * 10f; // -10, 0 e 10 graus para espalhar os tiros
                Vector2 direcaoRotacionada = Quaternion.Euler(0, 0, angulo) * direcao;

                rbTiro.linearVelocity = direcaoRotacionada * 5f;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monstro")) // Verifica se está colidindo com outro monstro
        {
            Vector2 afastar = (transform.position - collision.transform.position).normalized;
            rb.AddForce(afastar * 5f, ForceMode2D.Impulse);
        }
    }

    // ✅ Método para tomar dano e piscar
    public void TomarDano()
    {
        StartCoroutine(PiscarDano()); // Ativa a piscada quando o monstro for atingido

        vida--;
        if (vida <= 0)
        {
            Destroy(gameObject); // Destroi o monstro quando a vida chega a 0
        }
    }

    // ✅ Efeito de piscar rapidamente quando atingido
    private IEnumerator PiscarDano()
    {
        for (int i = 0; i < 3; i++) // Pisca 3 vezes rapidamente
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f); // Deixa transparente
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white; // Volta ao normal
            yield return new WaitForSeconds(0.1f);
        }
    }

    // ✅ Detectar colisão com o tiro do jogador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TiroPlayer")) // Certifique-se de que seu tiro tem essa tag!
        {
            TomarDano(); // Monstro perde 1 de vida
            Destroy(collision.gameObject); // O tiro desaparece
        }
    }
}
