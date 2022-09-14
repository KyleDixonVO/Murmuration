using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockBrain : MonoBehaviour
{

    public int numberOfMembers = 100;
    public GameObject flockPrefab;
    public CursorToWorldPosition cursorCalculator;
    public bool MousePressed;
    public float membersNearby;
    public float speed = 1;

    private GameObject[] flockMembers;
    public Vector3 averageFlockVelocity;
    // Start is called before the first frame update
    void Start()
    {
        cursorCalculator = GameObject.Find("Main Camera").GetComponent<CursorToWorldPosition>();
        flockMembers = new GameObject[numberOfMembers];
        averageFlockVelocity = new Vector3();
        for (int i = 0; i < numberOfMembers; i++)
        {
            flockMembers[i] = Instantiate(flockPrefab);
            flockMembers[i].transform.position = new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0f);
            flockMembers[i].GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5,5), Random.Range(-5, 5));
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            MousePressed = true;  
        }

        if (Input.GetMouseButtonUp(0))
        {
            MousePressed = false;
        }


        for (int i = 0; i < numberOfMembers; i++)
        {
            Vector3 aggregateVelocity = new Vector3();
            membersNearby = 0;
            for (int j = 0; j < numberOfMembers; j++)
            {
                if (Vector3.Distance(flockMembers[i].transform.position, flockMembers[j].transform.position) < 50.0f)
                {
                    //Debug.Log("Member Velocity: " + flockMembers[j].GetComponent<Rigidbody2D>().velocity);
                    aggregateVelocity += (Vector3)flockMembers[j].GetComponent<Rigidbody2D>().velocity;
                    Debug.Log("Aggregate Velocity: " + aggregateVelocity);
                    membersNearby++;
                }
            }
            Debug.Log(membersNearby);
            averageFlockVelocity = aggregateVelocity / membersNearby;
            Debug.Log(averageFlockVelocity);

            if (MousePressed)
            {
                flockMembers[i].transform.up = new Vector2(cursorCalculator.cursorWorldPos.x, cursorCalculator.cursorWorldPos.y);

                flockMembers[i].transform.position = Vector2.Lerp(flockMembers[i].transform.position, cursorCalculator.cursorWorldPos, Time.deltaTime);
            }
            else
            {
                flockMembers[i].transform.up = (averageFlockVelocity + ((Vector3)flockMembers[i].GetComponent<Rigidbody2D>().velocity * 0.5f));
                //flockMembers[i].transform.up = averageFlockVelocity;
                //flockMembers[i].transform.position += (averageFlockVelocity / 2) * speed;
                flockMembers[i].transform.position += flockMembers[i].transform.up * speed;
            }


        }
    }
}
