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

    public ushort State = 0;

    public enum ClientState : ushort
    {
        sit=1,
        thinkOrder=2,
        waitWaiter=3,
    }

    Slider slider;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>(); //GetComponent<GameManager>();
        prefabPointClient = GameObject.FindGameObjectWithTag("Point move client");
        slider = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
    }
    
    void Update()
    {
        clientWork((ClientState)State);
        //if (!sit)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, prefabPointClient.transform.position, speed * Time.deltaTime);
        //    if (Vector3.Distance(transform.position, prefabPointClient.transform.position).Equals(0))
        //        clickUp = true;
        //} else
        //{
        //    SliderToActive();
        //}
    }

    private void clientWork(ClientState clientState)
    {

    }

    private void OnMouseUp()
    {
        if (clickUp && !sit)
        {
            IEnumerable<GameObject> chainList = GameObject.FindGameObjectsWithTag("Chain").Where(p => p.GetComponent<ScriptChain>().free == true);
            if (!chainList.Count().Equals(0))
            {
                ScriptChain scriptChain = chainList.ElementAt(0).GetComponent<ScriptChain>();
                transform.position = chainList.ElementAt(0).transform.position;
                scriptChain.free = false;
                manager.createClient = false;
                clickUp = false;
                sit = true;
            }
        }
    }

    private void SliderToActive()
    {
        slider.gameObject.SetActive(true);
        if (!slider.GetComponent<ChangeColor>().trigger)
        {
            manager.AddClient(transform.gameObject, true);
            slider.GetComponent<ChangeColor>().trigger = true;
        }
    }
}
