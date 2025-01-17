using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject ball;
    public float ballForce;

    public Transform target;

    public int totalBalls;
    public bool readyToshoot;
    public GameObject[] allLevels;

    public int currentLevel;
    
    Plane plane = new Plane(Vector3.forward,0);
    // public GameObject [] canSetGRP;

    public Ball ballScript;
    public bool gameHasStarted;

    public int shootedBall;

    public GameObject gameOverUI;

    private Dictionary<Transform, Vector3> initialPositions = new Dictionary<Transform, Vector3>();

// Use this for initialization

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            // Destroy(this.gameObject);
            Destroy(this);
        }
    }

    void Start()
    {
        SaveInitialPositions();
        StartGame();
  
    }

    public void StartGame()
    {
        gameHasStarted = true;
        readyToshoot = true;
    }


    // void Update()
    // {
    //     Vector3 dir = target.position - ball.transform.position;
    //     Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,5));

    //     if (Input.GetMouseButtonDown(0) && readyToshoot)
    //     {
    //         ball.GetComponent<Animator>().enabled = false;
    //         ball.transform.position = new Vector3(mousePos.x,ball.transform.position.y,ball.transform.position.z);
    //     }

    //     if (Input.GetMouseButtonUp(0) && readyToshoot)
    //     {
    //         // Shoot the ball
    //         ball.GetComponent<Rigidbody>().AddForce(dir * ballForce, ForceMode.Impulse);
    //         readyToshoot = false;
    //         shootedBall++;
    //         totalBalls --;
    //         UIManager.instance.UpdatedBallIcons();
    //         // UIManager.instance.B_Start();
    //         if(totalBalls <= 0)
    //         {
    //             //check game over
    //             print("GameOver");
    //         }
    //     }

    //     float dist;
    //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     if (plane.Raycast(ray, out dist))
    //     {
    //         Vector3 point = ray.GetPoint(dist);
    //         target.position = new Vector3(point.x, point.y, 0);
    //     }
    //     if (totalBalls <= 0)
    //         {
    //             // Trigger Game Over sequence
    //             print("Game Over");
    //             GameOver();
    //         }


    // }

    void Update()
    {
        // بررسی اینکه آیا کلیک روی UI انجام شده است
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return; // اگر روی UI کلیک شده، عملیات را متوقف کن
        }

        Vector3 dir = target.position - ball.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));

        if (Input.GetMouseButtonDown(0) && readyToshoot)
        {
            ball.GetComponent<Animator>().enabled = false;
            ball.transform.position = new Vector3(mousePos.x, ball.transform.position.y, ball.transform.position.z);
        }

        if (Input.GetMouseButtonUp(0) && readyToshoot)
        {
            // Shoot the ball
            ball.GetComponent<Rigidbody>().AddForce(dir * ballForce, ForceMode.Impulse);
            readyToshoot = false;
            shootedBall++;
            totalBalls--;
            UIManager.instance.UpdatedBallIcons();

            if (totalBalls <= 0)
            {
                print("GameOver");
            }
        }

        float dist;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out dist))
        {
            Vector3 point = ray.GetPoint(dist);
            target.position = new Vector3(point.x, point.y, 0);
        }

        if (totalBalls <= 0)
        {
            print("Game Over");
            GameOver();
        }
    }



    public void GroundFallenCheck()
    {
        if(AllGrounded())
        {
            //load next level
            print("load next level");
            LoadNextLevel();
        }
    }

    bool AllGrounded()
    {
        Transform canSet = allLevels[currentLevel].transform;

        foreach(Transform t in canSet)
        {
            if(t.GetComponent<Can>().hasFallen == false)
            {
                return false;
            }

        }
        return true;
    }
    public void LoadNextLevel()
    {
        if(gameHasStarted)
        {
            StartCoroutine(LoadNextLevelRoutine());
        }
    }
    IEnumerator LoadNextLevelRoutine()
{
    Debug.Log("Loading Next Level");
    yield return new WaitForSeconds(1.5f);

    UIManager.instance.ShowBlackFade();
    readyToshoot = false;

    // Deactivate current level
    Debug.Log($"Deactivating level {currentLevel}");
    allLevels[currentLevel].SetActive(false);

    // Increment level
    currentLevel++;
    if (currentLevel >= allLevels.Length) currentLevel = 0;

    // Activate next level
    Debug.Log($"Activating level {currentLevel}");
    allLevels[currentLevel].SetActive(true);

    yield return new WaitForSeconds(1.0f);
    UIManager.instance.UpdateScoreMultiplier();
    shootedBall = 0;
    UIManager.instance.UpdatedBallIcons();
    ballScript.RepositionBall();
}
private void ResetBall()
{
    ball.GetComponent<Rigidbody>().velocity = Vector3.zero; // توقف حرکت توپ
    ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; // توقف چرخش توپ
    ball.transform.position = new Vector3(0, 1, 0); // بازگردانی موقعیت اولیه توپ
    ball.GetComponent<Animator>().enabled = false; // غیرفعال کردن انیمیشن
}



// ازینجا
public void GameOver()
{
    readyToshoot = false; // جلوگیری از شلیک توپ
    gameHasStarted = false;
    UIManager.instance.ShowBlackFade();
    gameOverUI.SetActive(true); // نمایش پنل Game Over

}


// public void RestartGame()
// {
//     Debug.Log("Restart button clicked!");

//     // Reset game state
//     currentLevel = 0;
//     totalBalls = 5;
//     shootedBall = 0;

//     // Reset score multiplier via UIManager
//     UIManager.instance.ScoreMultiplier = 1;

//     // Deactivate all levels
//     foreach (GameObject level in allLevels)
//     {
//         level.SetActive(false); // Deactivate the levels
//     }

//     // Activate the first level and its cans
//     allLevels[currentLevel].SetActive(true);

//     // Reset cans in the current level
//     ResetCansInLevel(currentLevel);

//     // Reset ball
//     ballScript.RepositionBall(); // Reset ball position
//     UIManager.instance.UpdatedBallIcons(); // Update UI
//     UIManager.instance.UpdateScoreMultiplier(); // Update multiplier display
//     UIManager.instance.ResetScore(); // Reset score

//     gameOverUI.SetActive(false); // Hide Game Over UI
//     StartGame();
// }

public void RestartGame()
{
    Debug.Log("Restart button clicked!");

    // Reset game state
    currentLevel = 0;
    totalBalls = 5; // مقداردهی مجدد تعداد توپ‌ها
    shootedBall = 0;

    // Reset score multiplier via UIManager
    UIManager.instance.ScoreMultiplier = 1;

    // Deactivate all levels
    foreach (GameObject level in allLevels)
    {
        level.SetActive(false);
    }

    // Activate the first level and its cans
    allLevels[currentLevel].SetActive(true);

    // Reset cans in the current level
    ResetCansInLevel(currentLevel);

    // Reset ball
    ResetBall();

    // Reset UI
    UIManager.instance.UpdatedBallIcons();
    UIManager.instance.UpdateScoreMultiplier();
    UIManager.instance.ResetScore();

    gameOverUI.SetActive(false); // مخفی کردن پنل Game Over

    StartCoroutine(DelayReadyToShoot());
}


private IEnumerator DelayReadyToShoot()
{
    readyToshoot = false;
    yield return new WaitForSeconds(0.5f); // تأخیر ۰.۵ ثانیه
    readyToshoot = true;
    StartGame();
}





private void SaveInitialPositions()
{
    foreach (GameObject level in allLevels)
    {
        Transform canSet = level.transform;

        foreach (Transform can in canSet)
        {
            if (can.CompareTag("Can"))
            {
                initialPositions[can] = can.position; // ذخیره موقعیت اولیه
            }
        }
    }
}



// private void ResetCansInLevel(int levelIndex)
// {
//     // Get the transform of the current level
//     Transform canSet = allLevels[levelIndex].transform;

//     // Iterate through the cans and reset their states
//     foreach (Transform can in canSet)
//     {
//         if (can.CompareTag("Can"))
//         {
//             Can canScript = can.GetComponent<Can>();
//             if (canScript != null)
//             {
//                 canScript.hasFallen = false; // Reset the fallen state
//                 can.gameObject.SetActive(true); // Reactivate the can if it's deactivated
//                 // Optionally, reset can's position if needed:
//                 // can.position = initialPosition; 
//             }
//         }
//     }
// }

private void ResetCansInLevel(int levelIndex)
{
    Transform canSet = allLevels[levelIndex].transform;

    foreach (Transform can in canSet)
    {
        if (can.CompareTag("Can"))
        {
            Can canScript = can.GetComponent<Can>();
            if (canScript != null)
            {
                canScript.hasFallen = false; // بازنشانی وضعیت افتادن
                can.gameObject.SetActive(true); // فعال کردن قوطی

                if (initialPositions.ContainsKey(can))
                {
                    can.position = initialPositions[can]; // بازگردانی موقعیت اولیه
                }

                // بازنشانی فیزیک قوطی
                Rigidbody rb = can.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero; // توقف حرکت
                    rb.angularVelocity = Vector3.zero; // توقف چرخش
                    rb.Sleep(); // غیر فعال کردن فیزیک تا برخورد بعدی
                }
            }
        }
    }
}



}
