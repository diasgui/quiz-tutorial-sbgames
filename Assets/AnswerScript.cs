using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    public Color startColor;

    private void Start() 
    {
        startColor = GetComponent<Image>().color;
    }

    public void Answer() {
        if(isCorrect)
        {
            GetComponent<Image>().color = Color.green;
            // dark green
            quizManager.isCorrect();
            // GetComponent<Image>().color = Color.white; 
            // GetComponent<Image>().color = new Color(162, 255, 162, 1);
            Debug.Log("Correto");
        }
        else
        {
            GetComponent<Image>().color = Color.red; 
            // dark red
            quizManager.isWrong();
            // GetComponent<Image>().color = new Color(255, 163, 163, 1); 

            Debug.Log("Errado");
        }

        // GetComponent<Image>().color = startColor;
    }
}
