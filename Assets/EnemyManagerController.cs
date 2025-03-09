using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManagerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created{
    private int totalMonstros;

    public string proximaCena="ScenaTeste";

    void Start()
    {
        // Conta quantos monstros existem na cena no início
        totalMonstros = GameObject.FindGameObjectsWithTag("Monstro").Length;
    }

    public void MonstroMorreu()
    {
        totalMonstros--;

        // Se todos os monstros morrerem, carregar a próxima cena
        if (totalMonstros <= 0)
        {
            IniciarProximaCena();
        }
    }

    void IniciarProximaCena()
    {
        Debug.Log("Todos os monstros foram derrotados! Carregando próxima cena...");
        SceneManager.LoadScene(proximaCena); // Substitua pelo nome real da sua próxima cena
    }
}
