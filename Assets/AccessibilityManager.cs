using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DavyKager;

public class AccessibilityManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Tolk.Unload();
        Load();
    }

    public void Load()
    {
        if (!Tolk.IsLoaded())
        {
            Debug.Log("Inicializando...");
            Tolk.Load();
            Debug.Log("Procurando por leitores de tela no dispositivo...");

            string name = Tolk.DetectScreenReader();

            if (name == null) Tolk.TrySAPI(true);

            if (name != null)
            {
                Debug.Log("O leitor ativo é: " + name);
            }
            else
            {
                // caso não tenha SAPI
                Debug.Log("Nenhum leitor está rodando");
            }
        }

        ReadText("Bem vindo ao jogo quiz: tutorial");
    }

    public void ReadText(string text)
    {
        Tolk.Speak(text);
    }

    public void ReadText(Text text)
    {
        Tolk.Speak(text.text);
    }
}
