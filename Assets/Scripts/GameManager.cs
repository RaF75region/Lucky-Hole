using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Client")]
    public GameObject prefabClient;
    public float timerRespawn;
    public bool createClient = false;


    // Start is called before the first frame update
    void Start()
    {
        timerRespawn = Random.Range(3f, 7f);
    }

    // Update is called once per frame
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
