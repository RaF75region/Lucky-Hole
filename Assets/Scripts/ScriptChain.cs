using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScriptChain : MonoBehaviour
{
    public bool free = true;

    private void OnCollisionEnter(Collision collision)
    {
        //collision.gameObject.GetComponent<NavMeshObstacle>().enabled = true;
        //collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        free = true;
    }
}
