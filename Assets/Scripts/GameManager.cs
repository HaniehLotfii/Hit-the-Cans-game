using UnityEngine;
using System.Collections;

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
  
    }

    public void StartGame()
    {
        gameHasStarted = true;
        readyToshoot = true;
    }


    void Update()
    {
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
            // UIManager.instance.B_Start();
            if(totalBalls <= 0)
            {
                //check game over
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
    // Trigger Game Over sequence
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
// ازینجا
public void GameOver()
{

    readyToshoot = false; // Stop the player from shooting
    gameHasStarted = false;
    UIManager.instance.ShowBlackFade();
    gameOverUI.SetActive(true); // Show the Game Over panel
}

public void RestartGame()
{
    Debug.Log("Restart button clicked!");

    // Reset game state
    currentLevel = 0;
    totalBalls = 5;
    shootedBall = 0;

    // Reset score multiplier via UIManager
    UIManager.instance.ScoreMultiplier = 1;

    // Deactivate all levels
    foreach (GameObject level in allLevels)
    {
        level.SetActive(false); // Deactivate the levels
    }

    // Activate the first level and its cans
    allLevels[currentLevel].SetActive(true);

    // Reset cans in the current level
    ResetCansInLevel(currentLevel);

    // Reset ball
    ballScript.RepositionBall(); // Reset ball position
    UIManager.instance.UpdatedBallIcons(); // Update UI
    UIManager.instance.UpdateScoreMultiplier(); // Update multiplier display
    UIManager.instance.ResetScore(); // Reset score

    gameOverUI.SetActive(false); // Hide Game Over UI
    StartGame();
}

private void ResetCansInLevel(int levelIndex)
{
    // Get the transform of the current level
    Transform canSet = allLevels[levelIndex].transform;

    // Iterate through the cans and reset their states
    foreach (Transform can in canSet)
    {
        if (can.CompareTag("Can"))
        {
            Can canScript = can.GetComponent<Can>();
            if (canScript != null)
            {
                canScript.hasFallen = false; // Reset the fallen state
                can.gameObject.SetActive(true); // Reactivate the can if it's deactivated
                // Optionally, reset can's position if needed:
                // can.position = initialPosition; 
            }
        }
    }
}



}
