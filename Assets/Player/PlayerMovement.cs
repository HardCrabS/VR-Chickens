using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool moveForward = true;
    [SerializeField] float speed = 10f;

    [SerializeField] bool moveInPath = false;
    [SerializeField] Transform path;

    List<Transform> waypoints;

    int currWaypointIndex = 0;
    float closeToWaypointRadius = 3f;
    Rigidbody myRb;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();

        waypoints = new List<Transform>();
        foreach (Transform waypoint in path)
        {
            waypoints.Add(waypoint);
        }

        if (moveInPath)
            StartCoroutine(MoveByWaypoints());
    }

    void Update()
    {
        if (moveForward)
            MoveForward();
    }

    IEnumerator MoveByWaypoints()
    {
        myRb.constraints = RigidbodyConstraints.FreezePosition;
        yield return new WaitForSeconds(7);
        myRb.constraints = RigidbodyConstraints.None;
        myRb.constraints = RigidbodyConstraints.FreezeRotation;
        while (true)
        {
            if (Vector3.Distance(transform.position, waypoints[currWaypointIndex].position) < closeToWaypointRadius)
            {
                ChangeWaypoint();
                myRb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                yield return new WaitForSeconds(3);
                myRb.constraints = RigidbodyConstraints.None;
                myRb.constraints = RigidbodyConstraints.FreezeRotation;
            }
            myRb.velocity = (waypoints[currWaypointIndex].position - transform.position).normalized * speed * Time.deltaTime;

            yield return null;
        }
    }

    void MoveForward()
    {
        myRb.velocity = Vector3.right * speed * Time.deltaTime;
    }

    void ChangeWaypoint()
    {
        if(currWaypointIndex + 1 < waypoints.Count)
        {
            currWaypointIndex++;
        }
    }
}