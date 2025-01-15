using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ball;
    Plane plane = new Plane(Vector3.forward, 0);
    public Transform target;
    public float ballForce;

    private bool isAdjustingPosition = false;
    private bool isReadyToShoot = false;    

    void Update()
    {
        Vector3 dir = target.position - ball.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));

        if (Input.GetMouseButtonDown(0))
        {
            if (!isReadyToShoot) 
            {
                ball.GetComponent<Animator>().enabled = false;
                ball.GetComponent<Rigidbody>().isKinematic = true; 
                isAdjustingPosition = true; 
            }
        }

        if (Input.GetMouseButton(0) && isAdjustingPosition)
        {
            ball.transform.position = new Vector3(mousePos.x, ball.transform.position.y, ball.transform.position.z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isAdjustingPosition)
            {
                isAdjustingPosition = false;
                isReadyToShoot = true; 
                ball.GetComponent<Rigidbody>().isKinematic = true; 
            }
            else if (isReadyToShoot)
            {
                ball.GetComponent<Rigidbody>().isKinematic = false; 
                ball.GetComponent<Rigidbody>().AddForce(dir * ballForce, ForceMode.Impulse);
                isReadyToShoot = false; 
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
}
