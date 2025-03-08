using UnityEngine;
using UnityEngine.SceneManagement;

public class VoltarMenuScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Voltar()
    {
        SceneManager.LoadScene("StartScene"); // Substitua pelo nome exato da sua cena do menu
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("StartScene");
        }
        
    }
}
