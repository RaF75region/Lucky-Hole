using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Linq;

public class ScriptWaiter : MonoBehaviour
{
    GameManager manager;
    NavMeshAgent agent;
    GameObject clientGameObj;
    GameObject chain;
    ClassClient client;
    [SerializeField]
    WaiterState state;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>(); //GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        workWaiter((ushort)state);
    }

    private void workWaiter(ushort status)
    {
        switch ((WaiterState)status)
        {
            case WaiterState.idle:
                break;
            case WaiterState.moveClient:
                if (clientGameObj)
                {
                    agent.SetDestination(clientGameObj.transform.position);
                    state = WaiterState.idle;
                }
                break;
        }
    }

    private void OnMouseUp()
    {
        IEnumerable<ClassClient> greenClient = manager.clients.Where(p => p.StatusOrder == ClientState.waitWaiter && !p.Dish);
        if (!greenClient.Count().Equals(0))
        {
            client = greenClient.ElementAt(Random.Range(0, greenClient.Count() - 1));
            clientGameObj = client.ObjClient;
            chain = client.Chain;
            state = WaiterState.moveClient;
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(chain))
        {
            agent.isStopped = true;
            client.StatusOrder = ClientState.transferOrder;
            state = WaiterState.idle;
        }
    }
}
