using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject ball;
    Plane plane = new Plane(Vector3.forward, 0);
    public Transform target;
    public float ballForce;
    public int totalBalls;
    public bool readyToshoot;
    public int currentLevel;
    public GameObject [] canSetGRP;

    public Ball ballScript;
    public bool gameHasStarted;


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

    void Start()
    {

    }

    void StartGame()
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
    }

    public void GroundFallenCheck()
    {
        if(AllGrounded())
        {
            //load next level
            print("load next level");
        }
    }

    bool AllGrounded()
    {
        Transform currentSet = canSetGRP[currentLevel].transform;

        foreach(Transform t in currentSet)
        {
            if(t.GetComponent<Can>().hasFallen == false)
            {
                return false;
            }

        }
        return true;
    }
}
