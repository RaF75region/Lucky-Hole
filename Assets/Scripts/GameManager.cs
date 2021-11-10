using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Client")]
    public GameObject prefabClient;
    public float timerRespawn;
    public bool createClient = false;

    void Start()
    {
        timerRespawn = Random.Range(3f, 7f);
    }

    void Update()
    {
        RespawnClient();
    }

    private void RespawnClient()
    {
        if (!createClient)
        {
            timerRespawn -= Time.deltaTime;
            if (timerRespawn < 0)
            {
                Instantiate(prefabClient);
                timerRespawn = Random.Range(3f, 7f);
                createClient = true;
            }
        }
    }
}
