using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class ScriptChain : MonoBehaviour
{
    public bool free = true;
    GameManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>(); //GetComponent<GameManager>();
    }

    private void OnCollisionExit(Collision collision)
    {
        free = true;
        //NavMeshObstacle obs = gameObject.GetComponent<NavMeshObstacle>();
        //obs.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //IEnumerable<ClassClient> cl = manager.clients.Where(p => p.ObjClient == other.gameObject && p.Chain==gameObject);
        //if (!cl.Count().Equals(0))
        //{
        //    NavMeshObstacle obs = gameObject.GetComponent<NavMeshObstacle>();
        //    obs.enabled = false;
        //}
    }
}
