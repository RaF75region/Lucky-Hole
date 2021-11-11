using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    //[Serializable]
    //public struct Client
    //{
    //    public GameObject ObjClient;
    //    public ClientState StatusOrder;
    //}

    [Header("Client")]
    public GameObject prefabClient;
    public float timerRespawn;
    public bool createClient = false;
    public ushort statusClient = 0;

    [SerializeField]
    public List<ClassClient> clients = new List<ClassClient>();

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
                AddClient(obj, (ClientState)statusClient);
            }
        }
    }

    public void AddClient(GameObject client, ClientState statusClients)
    {
        ClassClient cl =new ClassClient();
        cl.ObjClient = client;
        cl.StatusOrder = statusClients;
        clients.Add(cl);
    }

    public void ChaingeStatus(GameObject client, ushort state)
    {
        IEnumerable<ClassClient> obj = clients.Where(p => p.ObjClient == client);
        if (!obj.Count().Equals(0) && obj.Count().Equals(1))
        {
            //obj.ElementAt(0).StatusOrder = (ClientState)state;
        }
    }
}
