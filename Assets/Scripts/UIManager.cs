using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{


public static UIManager instance;

public GameObject blackFg;
public GameObject HomeUI, GameUI;
public GameObject gameScene;
    void Start()
    {
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
