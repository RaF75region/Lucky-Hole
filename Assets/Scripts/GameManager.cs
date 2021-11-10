using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public struct Client
    {
        public GameObject ObjClient { get; set; }
        public bool StatusOrder { get; set; }
    }

    [Header("Client")]
    public GameObject prefabClient;
    public float timerRespawn;
    public bool createClient = false;
    public List<Client> clients = new List<Client>();    

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

    public void AddClient(GameObject client, bool statusOrder)
    {
        Client cl=new Client();
        cl.ObjClient = client;
        cl.StatusOrder = statusOrder;
        clients.Add(cl);
    }
}
