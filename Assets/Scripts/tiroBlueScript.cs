using UnityEngine;

public class tiroBlueScript : MonoBehaviour
{
       private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("O jogador foi atingido!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Destroy(gameObject, 3f); // Destruir o tiro após 3 segundos
    }
}
