using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Animations;

public class ScriptClient : MonoBehaviour
{
    GameManager manager;
    GameObject prefabPointClient;
    Transform mainCamera;

    float speed = 5f;
    bool clickUp = false;
    [SerializeField]
    bool sit = false;//no sit

    ushort State = 0;
    ClassClient refClient = new ClassClient();

    Slider slider;
    ScriptChain scriptChain;
    NavMeshAgent navMeshAgent;
    RotationConstraint constraintCanvas;
    NavMeshObstacle navMeshObs;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>(); //GetComponent<GameManager>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        prefabPointClient = GameObject.FindGameObjectWithTag("Point move client");
        refClient = manager.clients.Where(p => p.ObjClient == gameObject).ElementAt(0);//ссылка на текущего клиента из списка
        slider = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        constraintCanvas = transform.GetChild(0).GetComponent<RotationConstraint>();
        rotationFixedUI();
    }
    
    void Update()
    {
        ushort state = (ushort)refClient.StatusOrder;
        clientWork(state);
    }
    #region fixed rotation
    private void rotationFixedUI()
    {
        ConstraintSource constraintSource = new ConstraintSource();
        constraintSource.sourceTransform = mainCamera;
        constraintSource.weight = 1;
        constraintCanvas.AddSource(constraintSource);
        constraintCanvas.locked = true;
        constraintCanvas.constraintActive = true;
    }
    #endregion
    private void clientWork(ushort clientState)
    {
        switch ((ClientState)clientState)
        {
            case ClientState.create:
                transform.position = Vector3.MoveTowards(transform.position, prefabPointClient.transform.position, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, prefabPointClient.transform.position).Equals(0))
                    refClient.StatusOrder = ClientState.waiting;
                break;
            case ClientState.searchFreeChain:
                IEnumerable<GameObject> chainList = GameObject.FindGameObjectsWithTag("Chain").Where(p => p.GetComponent<ScriptChain>().free == true);
                if (!chainList.Count().Equals(0))
                {
                    //scriptChain = chainList.ElementAt(0).GetComponent<ScriptChain>();
                    scriptChain= chainList.ElementAt(Random.Range(0,chainList.Count()-1)).GetComponent<ScriptChain>();
                    scriptChain.free = false;
                    manager.createClient = false;
                    refClient.StatusOrder = ClientState.sit;
                }
                break;
            case ClientState.sit:
                navMeshAgent = GetComponent<NavMeshAgent>();
                navMeshAgent.SetDestination(scriptChain.gameObject.transform.position);
                
                if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance.Equals(0))
                {
                    refClient.StatusOrder = ClientState.thinkOrder;
                }
                break;
            case ClientState.thinkOrder:
                SliderToActive();
                break;
        }
    }

    private void OnMouseUp()
    {
        refClient.StatusOrder = ClientState.searchFreeChain;
    }

    private void SliderToActive()
    {
        slider.gameObject.SetActive(true);
        if (!slider.GetComponent<ChangeColor>().trigger)
        {
            slider.GetComponent<ChangeColor>().trigger = true;
            refClient.StatusOrder = ClientState.waiting;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == scriptChain.gameObject)
        {
            navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        }
    }
}
