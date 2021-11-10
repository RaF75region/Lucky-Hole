using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public struct Client
    {
        public GameObject ObjClient;
        public ClientState StatusOrder;
    }

    [Header("Client")]
    public GameObject prefabClient;
    public float timerRespawn;
    public bool createClient = false;

    [SerializeField]
    List<Client> clients=new List<Client>();    

    void Start()
    {
        timerRespawn = UnityEngine.Random.Range(3f, 7f);
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
                GameObject obj= Instantiate(prefabClient);
                timerRespawn = UnityEngine.Random.Range(3f, 7f);
                createClient = true;
                AddClient(obj, ClientState.waiting);
            }
        }
    }

    public void AddClient(GameObject client, ClientState statusClients)
    {
        Client cl=new Client();
        cl.ObjClient = client;
        cl.StatusOrder = statusClients;
        clients.Add(cl);
    }
}
