using System.Collections;
using UnityEngine;

public class Monstro : MonoBehaviour
{
    public GameObject player; // Referência ao jogador
    public GameObject tiroPrefab; // Prefab da bolinha de tiro
    public float velocidade = 2f; // Velocidade do monstro
    public float tempoEntreTiros = 2f; // Tempo entre os ataques

    private void Start()
    {
        // Inicia o ataque repetitivo
        InvokeRepeating(nameof(Atirar), 1f, tempoEntreTiros);
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

    private void Atirar()
    {
        if (player == null) return;

        for (int i = 0; i < 3; i++) // Disparar 3 bolinhas
        {
            GameObject tiro = Instantiate(tiroPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = tiro.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 direcao = (player.transform.position - transform.position).normalized;
                rb.linearVelocity = Quaternion.Euler(0, 0, i * 10 - 10) * direcao * 5f; // Pequena variação de ângulo
            }
        }
    }
}
