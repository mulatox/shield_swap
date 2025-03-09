using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoController2 : MonoBehaviour
{
    public GameObject painelDialogo;
    public Image portraitImage;
    public TextMeshProUGUI dialogoText;
    public float velocidadeTexto = 0.05f;
    public Sprite personagemPortrait; // Imagem inicial do personagem

    private Queue<string> falas;
    private bool textoCompleto = false;

    void Start()
    {
        falas = new Queue<string>();
        painelDialogo.SetActive(false);

        // ✅ Chama o diálogo automaticamente ao iniciar a cena
        string[] falasIniciais = {
            "Iniciando Fase 2 do treinamento...",
            "Use tecla espaço para ativar o protocolo shield swap!",
            "Boa Sorte!"
        };

        IniciarDialogo(personagemPortrait, falasIniciais);
        
    }

    public void IniciarDialogo(Sprite portrait, string[] falasArray)
    {
        painelDialogo.SetActive(true);
       
        portraitImage.sprite = portrait;
        falas.Clear();

        foreach (string fala in falasArray)
        {
            falas.Enqueue(fala);
        }

        MostrarProximaFala();
    }

    public void MostrarProximaFala()
    {
        if (falas.Count == 0)
        {
            FecharDialogo();
            return;
        }

        string falaAtual = falas.Dequeue();
        StopAllCoroutines();
        StartCoroutine(EscreverTexto(falaAtual));
    }

    IEnumerator EscreverTexto(string fala)
    {
        textoCompleto = false;
        dialogoText.text = "";

        foreach (char letra in fala.ToCharArray())
        {
            dialogoText.text += letra;
            yield return new WaitForSecondsRealtime(velocidadeTexto);
        }

        textoCompleto = true;
        yield return new WaitForSecondsRealtime(1.8f);
        MostrarProximaFala();
    }

    public void FecharDialogo()
    {
         
        painelDialogo.SetActive(false);
    }

    public void AvancarDialogo()
    {
        if (textoCompleto)
        {
            MostrarProximaFala();
        }
        else
        {
            StopAllCoroutines();
            dialogoText.text = falas.Peek();
            textoCompleto = true;
        }
    }
}
