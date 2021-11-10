using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScriptWaiter : MonoBehaviour
{
 
    void Update()
    {
       //tag ImageSilderClient
    }

    private void OnMouseDown()
    {
        IEnumerable<GameObject> goodOrder = GameObject.FindGameObjectsWithTag("ImageSilderClient").Where(p => p.GetComponent<Image>().color.Equals(Color.green));
        if (!goodOrder.Count().Equals(0))
        {
            print("заказ принял");
        }
    }
}
