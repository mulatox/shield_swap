using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class DialogoController : MonoBehaviour
{
    public GameObject painelDialogo; // Painel da caixa de diálogo
    public Image portraitImage; // Imagem do personagem
    public TextMeshProUGUI dialogoText; // Texto do diálogo
    public float velocidadeTexto = 0.05f; // Velocidade da escrita do texto

    private Queue<string> falas; // Fila de falas
    private bool textoCompleto = false;

    void Start()
    {
        falas = new Queue<string>(); // Inicializa a fila de falas
        painelDialogo.SetActive(false); // Oculta a caixa de diálogo inicialmente
    }

    // Método para iniciar o diálogo
    public void IniciarDialogo(Sprite portrait, string[] falasArray)
    {
        painelDialogo.SetActive(true); // Mostra a caixa de diálogo
        portraitImage.sprite = portrait; // Define a imagem do personagem
        falas.Clear();

        foreach (string fala in falasArray)
        {
            falas.Enqueue(fala); // Adiciona cada fala na fila
        }

        MostrarProximaFala();
    }

    // Método para mostrar a próxima fala
    public void MostrarProximaFala()
    {
        if (falas.Count == 0)
        {
            FecharDialogo();
            return;
        }

        string falaAtual = falas.Dequeue();
        StopAllCoroutines(); // Para qualquer texto que ainda esteja sendo exibido
        StartCoroutine(EscreverTexto(falaAtual));
    }

    // Animação do texto aparecendo progressivamente
    IEnumerator EscreverTexto(string fala)
    {
        textoCompleto = false;
        dialogoText.text = "";

        foreach (char letra in fala.ToCharArray())
        {
            dialogoText.text += letra;
            yield return new WaitForSeconds(velocidadeTexto);
        }

        textoCompleto = true;
    }

    // Método para fechar a caixa de diálogo
    public void FecharDialogo()
    {
        painelDialogo.SetActive(false);
    }

    // Método para avançar no diálogo ao pressionar um botão
    public void AvancarDialogo()
    {
        if (textoCompleto)
        {
            MostrarProximaFala();
        }
        else
        {
            StopAllCoroutines();
            dialogoText.text = falas.Peek(); // Exibe a fala completa imediatamente
            textoCompleto = true;
        }
    }
}
