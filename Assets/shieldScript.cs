using UnityEngine;

public class shieldScript : MonoBehaviour
{
    public float velocidadeRotacao = 100f; // Velocidade da rotação
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         transform.Rotate(0, 0, velocidadeRotacao * Time.deltaTime);
    }
}
