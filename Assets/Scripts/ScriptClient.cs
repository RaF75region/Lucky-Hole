using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScriptClient : MonoBehaviour
{
    GameManager manager;
    GameObject prefabPointClient;
    float speed = 5f;
    bool clickUp = false;
    [SerializeField]
    bool sit = false;//no sit

    ushort State = 0;
    ClassClient refClient = new ClassClient();

    Slider slider;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>(); //GetComponent<GameManager>();
        prefabPointClient = GameObject.FindGameObjectWithTag("Point move client");
        refClient = manager.clients.Where(p => p.ObjClient == gameObject).ElementAt(0);//ссылка на текущего клиента из списка
        slider = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
    }
    
    void Update()
    {
        ushort state = (ushort)refClient.StatusOrder;
        clientWork(state);
    }

    private void clientWork(ushort clientState)
    {
        switch ((ClientState)clientState)
        {
            case ClientState.create:
                transform.position = Vector3.MoveTowards(transform.position, prefabPointClient.transform.position, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, prefabPointClient.transform.position).Equals(0))
                    refClient.StatusOrder = ClientState.waiting;
                break;
            case ClientState.sit:
                IEnumerable<GameObject> chainList = GameObject.FindGameObjectsWithTag("Chain").Where(p => p.GetComponent<ScriptChain>().free == true);
                if (!chainList.Count().Equals(0))
                {
                    ScriptChain scriptChain = chainList.ElementAt(0).GetComponent<ScriptChain>();
                    transform.position = chainList.ElementAt(0).transform.position;
                    scriptChain.free = false;
                    manager.createClient = false;
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
        refClient.StatusOrder = ClientState.sit;
    }

    private void SliderToActive()
    {
        slider.gameObject.SetActive(true);
        if (!slider.GetComponent<ChangeColor>().trigger)
        {
           // manager.AddClient(transform.gameObject, true);
            slider.GetComponent<ChangeColor>().trigger = true;
            refClient.StatusOrder = ClientState.waiting;
        }
    }
}
