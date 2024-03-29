using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform Floor;
    public float SpeedMove = 10f;
    public float boundsX = 10f;
    public int boundsZ = 10;
    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * SpeedMove;
        float zMove = Input.GetAxis("Vertical") * Time.deltaTime * SpeedMove;
        Vector3 position = transform.position;
        position.x += xMove;
        position.z += zMove;
        position.x = Mathf.Clamp(position.x, 0, boundsX);
        position.z = Mathf.Clamp(position.z, 0, boundsZ);
        transform.position = position;

    }
}
