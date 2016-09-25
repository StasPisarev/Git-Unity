using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    public int score = 0;

    [SerializeField] int timer = 60; // время на уровень

    public bool gameover = false;

    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
  
    [SerializeField] Text loseText;
    [SerializeField] Text winText;

  


    public void PlusScore() { // при сборе монет
        score++;
        UpdateScoreText(); 
    }

    void UpdateScoreText() {

        scoreText.text = "Score : " + score;

    }

    void Start() {
        
        TimerManager(); 
    }


    IEnumerator WaitOneSec() { //карутина для таймера уровня

    yield return new WaitForSeconds(1f);
        timer--;
        timeText.text = "Time : " + timer;
        TimerManager();

    }

    void TimerManager() { // менеджер таймера уровня, пока не кончится время или не конец игры запускает карутину на 1 секунду

        if (!gameover && timer > 0)
        {
            StartCoroutine(WaitOneSec());
        }
        else
        {
            StopAllCoroutines();
            GameOver();
        }
    }


    void GameOver() 
    {
        gameover = true;
        scoreText.enabled = false;
        timeText.enabled = false;
        Time.timeScale = 0;

        loseText.enabled = true;
       
    }

    public void Win() { 
        gameover = true;
        scoreText.enabled = false;
        timeText.enabled = false;
        StopAllCoroutines();
        Time.timeScale = 0;
        winText.text = "Win, Score = " + score;
        winText.enabled = true;
       

    }

    public void Restart() { // функционал кнопки рестарта
        Time.timeScale = 1;
        Application.LoadLevel(0);
    }

}
