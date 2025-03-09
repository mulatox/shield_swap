using System.Collections;
using UnityEngine;

public class MonstroSemTiro : MonoBehaviour
{
    public int vida = 3; // Monstro morre após ser atingido 3 vezes
    private SpriteRenderer spriteRenderer;
    public GameObject player; // Referência ao jogador
    public float velocidade = 2f; // Velocidade do monstro
    public float tempoEntreTiros = 2f; // Tempo entre os ataques
    public EnemyManagerController enemyManager;

    private void Start()
    {
        // Inicia o ataque repetitivo
        spriteRenderer = GetComponent<SpriteRenderer>(); // Pegamos o SpriteRenderer para o efeito de piscar
       // InvokeRepeating(nameof(Atirar), 1f, tempoEntreTiros);
    }

    private void Update()
    {
        // Faz o monstro seguir o jogador
        if (player != null)
        {
            Vector2 direcao = (player.transform.position - transform.position).normalized;
            transform.position += (Vector3)direcao * velocidade * Time.deltaTime;
        }
    }

   

     public void TomarDano()
    {
        StartCoroutine(PiscarDano()); // Ativa a piscada quando o monstro for atingido

        vida--;
        if (vida <= 0)
        {
            if (enemyManager != null)
        {
            enemyManager.MonstroMorreu(); // ✅ Avisa ao EnemyManager que morreu
        }    

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
