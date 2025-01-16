using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{



public static UIManager instance;
public GameObject[] allBallImg;
public Sprite enabledBallImg;
public GameObject blackFg;
public GameObject HomeUI, GameUI;
public GameObject gameScene;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
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
            allBallImg[i].SetActive(true); // فعال کردن تصویر
            allBallImg[i].GetComponent<Image>().sprite = enabledBallImg; // نمایش تصویر فعال
        }
        else
        {
            allBallImg[i].SetActive(false); // غیرفعال کردن تصویر
        }
        }
    }

    void Start()
    {
        HomeUI.SetActive(true);
        gameScene.SetActive (false);
    }
    public void B_Start()
    {
        StartCoroutine(StartGameRoutine());
    }
    IEnumerator StartGameRoutine()
    {
        StartCoroutine(FadeRoutine());
        yield return new WaitForSeconds (0.5f);
        HomeUI.SetActive(false);
        gameScene.SetActive(true);
        GameUI.SetActive(true);
        GameManager.instance.readyToshoot = true;
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
}
