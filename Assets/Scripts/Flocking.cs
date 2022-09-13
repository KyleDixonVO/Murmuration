using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    public float speed = 10.0f;
    public float assimilationWeight;
    public CursorToWorldPosition cursorCalc;
    public Vector3 averageVelocity;

    public List<Rigidbody2D> flockMembers;

    // Start is called before the first frame update
    void Start()
    {
        cursorCalc = GameObject.Find("Main Camera").GetComponent<CursorToWorldPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = new Vector2(cursorCalc.cursorWorldPos.x, cursorCalc.cursorWorldPos.y);
        
        transform.position += new Vector3 (cursorCalc.cursorWorldPos.x, cursorCalc.cursorWorldPos.y, 0) * Time.deltaTime;

        transform.position = Vector2.LerpUnclamped(transform.position, cursorCalc.cursorWorldPos, Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        flockMembers.Remove(collision.GetComponent<Rigidbody2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        flockMembers.Add(collision.GetComponent<Rigidbody2D>());
    }


}
