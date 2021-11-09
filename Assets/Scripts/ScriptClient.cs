using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptClient : MonoBehaviour
{
    GameManager manager;
    GameObject prefabPointClient;
    private float speed = 5f;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>(); //GetComponent<GameManager>();
        prefabPointClient = GameObject.FindGameObjectWithTag("Point move client");
    }
    // Update is called once per frame
    void Update()
    {
        if (manager.createClient)
        {
            transform.position = Vector3.MoveTowards(transform.position, prefabPointClient.transform.position, speed*Time.deltaTime);
        }
    }
}
