using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testNavigation : MonoBehaviour
{

    public Transform point;
    NavMeshAgent navMeshAgent; 
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(point.position);
        //navMeshAgent.
        if (navMeshAgent.pathPending)
        {
            print("bum");
        }
    }

}
