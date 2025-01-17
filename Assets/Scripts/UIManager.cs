using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{



public static UIManager instance;
public GameObject[] allBallImg;
public Sprite enabledBallImg;
public GameObject blackFg;
public GameObject HomeUI, GameUI;
public GameObject gameScene;

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
            // Destroy(this.gameObject);
            Destroy(gameObject);
        }
    }
    void Start()
    {
        HomeUI.SetActive(true);
        GameUI.SetActive(false);
        gameScene.SetActive (false);
    }
    public void UpdatedBallIcons()
    {
        int ballcount = GameManager.instance.totalBalls;
        for (int i = 0; i < 5; i++)
        {
            Image image = allBallImg[i].GetComponent<Image>();
        if (i < ballcount)
        {
            allBallImg[i].SetActive(true); // فعال کردن تصویر
            allBallImg[i].GetComponent<Image>().sprite = enabledBallImg; // نمایش تصویر فعال
        }
        else
        {
            allBallImg[i].SetActive(false); // غیرفعال کردن تصویر
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
    }
    IEnumerator StartGameRoutine()
    {
        ShowBlackFade();
        yield return new WaitForSeconds (1f);
        gameScene.SetActive(true);
        HomeUI.SetActive(false);
        GameUI.SetActive(true);
        // GameManager.instance.readyToshoot = true;
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

    // Update is called once per frame
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
    //ازینجا

    public void OnRestartButtonClicked()
{
    // Call the RestartGame method from the GameManager to reset everything
    GameManager.instance.RestartGame();
}

    public void ResetScore()
{
    score = 0;
    scoreText.text = score.ToString();
}


}
