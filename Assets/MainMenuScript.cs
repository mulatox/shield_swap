using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
  public string nomeDaCenaDoJogo = "ScenaTeste"; // Nome da cena principal do jogo

    public void StartGame()
    {
        SceneManager.LoadScene(nomeDaCenaDoJogo); // Carrega a cena do jogo
    }

    public void OpenCredits()
    {
        Debug.Log("Abrindo Créditos...");
        // Aqui você pode abrir uma nova cena de créditos ou um painel de créditos
    }

    public void ExitGame()
    {
        Debug.Log("Saindo do Jogo...");
        Application.Quit(); // Funciona apenas no jogo compilado
    }
}
