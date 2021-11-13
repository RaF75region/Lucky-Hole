using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform Floor;
    public float SpeedMOve = 1f;
    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * SpeedMOve;
        float zMove = Input.GetAxis("Vertical") * Time.deltaTime * SpeedMOve;
        Vector3 position = transform.position;
        position.x += xMove;
        position.z += zMove;
        transform.position = position;

        



        print(transform.GetChild(0).GetComponent<Camera>().fieldOfView);
    }
}
