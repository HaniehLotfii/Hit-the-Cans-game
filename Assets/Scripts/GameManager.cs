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

    public Dictionary<Transform, Vector3> initialPositions = new Dictionary<Transform, Vector3>();

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
        StartGame();
        
        if (UIManager.isRestart)
        {
            UIManager.isRestart = false;
            GameManager.instance.StartGame();
        }

  
    }

    public void StartGame()
    {
        gameHasStarted = true;
        readyToshoot = true;
        gameHasStarted = true;
        totalBalls = 5; // مقداردهی تعداد توپ‌ها
        shootedBall = 0; // مقداردهی توپ‌های شلیک‌شده
        UIManager.instance.UpdatedBallIcons(); // به‌روزرسانی UI
    }

    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        Vector3 dir = target.position - ball.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,5));

        if (Input.GetMouseButtonDown(0) && readyToshoot)
        {
            ball.GetComponent<Animator>().enabled = false;
            ball.transform.position = new Vector3(mousePos.x,ball.transform.position.y,ball.transform.position.z);
        }

        if (Input.GetMouseButtonUp(0) && readyToshoot)
        {
            // Shoot the ball
            ball.GetComponent<Rigidbody>().AddForce(dir * ballForce, ForceMode.Impulse);
            readyToshoot = false;
            shootedBall++;
            totalBalls --;
            UIManager.instance.UpdatedBallIcons();

            SoundManager.instance.PlaySound(SoundManager.instance.ballThrowSound);

            // SoundManager.instance.PlayFx(FxTypes.BALLTHROW);

            // UIManager.instance.B_Start();
            if(totalBalls <= 0)
            {
                //check game over
                print("GameOver");
                SoundManager.instance.PlaySound(SoundManager.instance.gameOverSound);
                StartCoroutine(CheckGameOver());
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
                // Trigger Game Over sequence
                print("Game Over");
                
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
        allLevels[currentLevel].SetActive(false);

        // Increment level
        currentLevel++;
        if (currentLevel > allLevels.Length) currentLevel = 0;

        yield return new WaitForSeconds(1.0f);
        UIManager.instance.UpdateScoreMultiplier();
        shootedBall = 0;
        allLevels[currentLevel].SetActive(true);
        UIManager.instance.UpdatedBallIcons();
        ballScript.RepositionBall();
    }

    IEnumerator CheckGameOver()
    {
        yield return new WaitForSeconds(2);
        if (AllGrounded() == false)
        {
            UIManager.instance.gameOverUI.SetActive(true);
        }

    }
}
