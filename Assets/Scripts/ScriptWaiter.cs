using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.Animations;

public class ScriptWaiter : MonoBehaviour
{
    public Transform Cook;

    GameManager manager;
    NavMeshAgent agent;
    GameObject clientGameObj;
    GameObject chain;
    ClassClient client;
    [SerializeField]
    WaiterState state;
    Transform mainCamera;
    RotationConstraint constraintCanvas;
    Slider slider;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>(); //GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        mainCamera= GameObject.FindGameObjectWithTag("MainCamera").transform;
        constraintCanvas = transform.GetChild(0).GetComponent<RotationConstraint>();
        slider = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        rotationFixedUI();
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
                    //state = WaiterState.idle;
                    if (agent.isStopped)
                    {
                        
                    }
                }
                break;
            case WaiterState.toTakeOrder:
                SliderToActive();
                break;
            case WaiterState.moveCook:
                break;
        }
    }

    private void SliderToActive()
    {
        slider.gameObject.SetActive(true);
        if (!slider.GetComponent<ChangeColor>().trigger)
        {
            slider.GetComponent<ChangeColor>().trigger = true;
            if (slider.value == slider.maxValue)
            {
                state = WaiterState.moveCook;
            }
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
            state = WaiterState.toTakeOrder;
        }
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
}
