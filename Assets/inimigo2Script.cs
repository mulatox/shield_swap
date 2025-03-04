using System.Collections;
using UnityEngine;

public class inimigo2Script : MonoBehaviour
{
    public GameObject player; // Referência ao jogador
    public GameObject tiroPrefab; // Prefab da bolinha de tiro
    public float velocidade = 2f; // Velocidade do monstro
    public float tempoEntreTiros = 2f; // Tempo entre os ataques
    public float raioCirculo = 1.5f; // Raio do movimento circular
    public float velocidadeRotacao = 2f; // Velocidade de rotação
    private Rigidbody2D rb;
    private float angulo; // Ângulo para movimento circular

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
