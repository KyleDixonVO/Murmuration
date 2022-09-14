using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorToWorldPosition : MonoBehaviour
{
    public Vector3 cursorWorldPos;
    private GameObject cameraObj;
    // Start is called before the first frame update
    void Start()
    {
        cameraObj = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        cursorWorldPos = cameraObj.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    }
}
