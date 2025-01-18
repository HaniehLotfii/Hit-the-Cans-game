using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{



    public static UIManager instance;
    public GameObject pausePanel;
    public static bool isRestart;
    public GameObject[] allBallImg;
    public Sprite enabledBallImg;
    public GameObject blackFg;
    public GameObject HomeUI, GameUI;
    public GameObject gameScene;
    public GameObject gameOverUI;

    public int score;
    public TextMeshProUGUI scoreText;

    public int ScoreMultiplier = 1;
    public GameObject scoreMultiplierImage;
    public TextMeshProUGUI scoreMultiText;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
        HomeUI.SetActive(true);
        gameScene.SetActive (false);
        if (isRestart)
        {
            isRestart = false;
            HomeUI.SetActive(false);
            gameScene.SetActive (false);
            GameManager.instance.StartGame();
        }

        

    }
    public void UpdatedBallIcons()
    {
        int ballcount = GameManager.instance.totalBalls;
        for (int i = 0; i < 5; i++)
        {
            Image image = allBallImg[i].GetComponent<Image>();
        if (i < ballcount)
        {
            allBallImg[i].SetActive(true); 
            allBallImg[i].GetComponent<Image>().sprite = enabledBallImg;
        }
        else
        {
            allBallImg[i].SetActive(false); 
        }
        }
    }

    
    public void B_Start()
    {
        StartCoroutine(StartGameRoutine());
    }
    public void B_Exit()
{
    Application.Quit();

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
}
    IEnumerator StartGameRoutine()
    {
        ShowBlackFade();
        yield return new WaitForSeconds (1f);
        gameScene.SetActive(true);
        HomeUI.SetActive(false);
        GameUI.SetActive(true);
        GameManager.instance.StartGame();
    }
    public void ShowBlackFade()
    {
        StartCoroutine(FadeRoutine());

    }
    IEnumerator FadeRoutine()
    {
        blackFg.SetActive(true);
        yield return new WaitForSeconds (1.5f);
        blackFg.SetActive (false);
    }

    void Update()
    {
        
    }
    public void UpdateScore()
    {
        score += ScoreMultiplier*1;
        scoreText.text = score.ToString();
    }
    public void UpdateScoreMultiplier()
    {
        if(GameManager.instance.shootedBall == 1)
        {
            ScoreMultiplier++;
            scoreMultiplierImage.SetActive(true);
            scoreMultiText.text = ScoreMultiplier.ToString();

        }else
        {
            ScoreMultiplier = 1;
            scoreMultiplierImage.SetActive(false);
        }
    }

    public void B_Restart()
    {
        StartCoroutine(RestartRoutine());
    }
    IEnumerator RestartRoutine()
    {
        ShowBlackFade();
        isRestart = true;
        yield return new WaitForSeconds(1);
        // SceneManager.LoadScene(0, LoadSceneMode.Additive);
        SceneManager.LoadScene(0, LoadSceneMode.Single);

    }
    public void B_Back(){
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void B_Back_Yes(){

        Time.timeScale = 1;
        pausePanel.SetActive(false);
        HomeUI.SetActive(true);

    }
    public void B_Back_No(){

        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }



}
