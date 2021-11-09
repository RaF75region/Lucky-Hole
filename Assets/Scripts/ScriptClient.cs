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
    bool sit = false;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>(); //GetComponent<GameManager>();
        prefabPointClient = GameObject.FindGameObjectWithTag("Point move client");
    }
    // Update is called once per frame
    void Update()
    {
        if (manager.createClient && !sit)
        {
            transform.position = Vector3.MoveTowards(transform.position, prefabPointClient.transform.position, speed*Time.deltaTime);
            if(Vector3.Distance(transform.position, prefabPointClient.transform.position).Equals(0))
                clickUp = true;
        }
        else if(sit)
        {
            Slider slider = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
            slider.enabled = true;
            slider.value += Time.deltaTime;
            if (slider.value > slider.maxValue)
            {
                slider.enabled = false;
            }
        }
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
}
