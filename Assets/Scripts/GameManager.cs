using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject ball;
    Plane plane = new Plane(Vector3.forward, 0);
    public Transform target;
    public float ballForce;

    void Update()
    {
        Vector3 dir = target.position - ball.transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            // Shoot the ball
            ball.GetComponent<Rigidbody>().AddForce(dir * ballForce, ForceMode.Impulse);
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
