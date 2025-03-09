using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false); // ✅ Começa desativado
    }

    public void AtivarGameOver()
    {   
        Debug.Log("Ativar Gameover!");
        gameObject.SetActive(true); // ✅ Ativa o painel de Game Over
        Time.timeScale = 0f; // ✅ Pausa o jogo
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // ✅ Retorna ao normal antes de reiniciar
        SceneManager.LoadScene("ScenaTeste2"); // ✅ Reinicia a cena atual
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f; // ✅ Retorna ao normal antes de ir para o menu
        SceneManager.LoadScene("StartScene"); // ✅ Nome da cena do menu principal
    }
}
