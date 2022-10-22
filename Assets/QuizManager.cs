using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAnswers> ListQuestions;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject InitialPanel;
    public GameObject QuizPanel;
    public GameObject GoPanel;
    public GameObject SoundPanel;

    public Button FirstButton;
    public Button FirstSoundButton;
    public Button RetryButton;

    public AudioSource audioSource;

    public AudioClip correct;
    public AudioClip wrong;
    public AudioClip end;

    public Text QuestionText;
    public Text scoreTxt;

    public AccessibilityManager accessibilityManager;

    int totalQuestions = 0;
    public int score;

    private void Start() 
    {
        InitialPanel.SetActive(true);
        QuizPanel.SetActive(false);
        GoPanel.SetActive(false);
        SoundPanel.SetActive(false);

        FirstButton.Select();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F1))
            accessibilityManager.ReadText(QuestionText);
    }
    public void StartQuiz() 
    {   
        totalQuestions = ListQuestions.Count;
        score = 0;
        
        InitialPanel.SetActive(false);
        QuizPanel.SetActive(true);
        GoPanel.SetActive(false);
        
        generateQuestion();
        // StartCoroutine(generateQuestionWait());
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        InitialPanel.SetActive(true);
        QuizPanel.SetActive(false);
        GoPanel.SetActive(false);
    }

    public void SoundMenu()
    {
        SoundPanel.SetActive(true);
        InitialPanel.SetActive(false);
        QuizPanel.SetActive(false);
        GoPanel.SetActive(false);

        FirstSoundButton.Select();
    }

    public void QuitGame()
    {
        Debug.Log("Sair do jogo...");
        Application.Quit();
    }

    public void isCorrect() 
    {
        PlayAudio(correct);
        score += 1;
        ListQuestions.RemoveAt(currentQuestion);
        StartCoroutine(generateQuestionWait());
    }

    public void isWrong()
    {
        PlayAudio(wrong);
        ListQuestions.RemoveAt(currentQuestion);
        StartCoroutine(generateQuestionWait());
    }

    public void GameOver()
    {
        PlayAudio(end);

        for (int i=0; i < 5000; i++); // wait some time

        QuizPanel.SetActive(false);
        GoPanel.SetActive(true);
        scoreTxt.text = "Pontuação: \n" + score + " de " + totalQuestions;

        accessibilityManager.ReadText(scoreTxt);

        RetryButton.Select();
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].GetComponent<Image>().color = options[i].GetComponent <AnswerScript>().startColor;
            options[i].transform.GetChild(0).GetComponent<Text>().text = ListQuestions[currentQuestion].Answers[i];

            if(ListQuestions[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }

        for (int i=0; i < 5000; i++); // wait some time

        options[0].GetComponent<Button>().Select();
    }

    void generateQuestion()
    {
        currentQuestion = Random.Range(0, ListQuestions.Count);
        QuestionText.text = ListQuestions[currentQuestion].Question;
        accessibilityManager.ReadText("Pergunta:" + ListQuestions[currentQuestion].Question);
        
        SetAnswer();
    }

    IEnumerator generateQuestionWait() 
    {
        Debug.Log("waiting...");

        yield return new WaitForSeconds(2);


        if(ListQuestions.Count > 0) 
        {
            currentQuestion = Random.Range(0, ListQuestions.Count);
            accessibilityManager.ReadText("Pergunta:" + ListQuestions[currentQuestion].Question);
            QuestionText.text = ListQuestions[currentQuestion].Question;
            
            yield return new WaitForSeconds(2);
            SetAnswer();
        }
        else
        {
            Debug.Log("Fim das questões.");
            GameOver();
        }

        Debug.Log("finished...");
    }

    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
