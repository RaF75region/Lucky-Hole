using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    bool trigger=true;
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && trigger)
        {
            Slider slider = GetComponent<Slider>();
            slider.value += Time.deltaTime;
            if (slider.value >= slider.maxValue)
            {
                Image image = slider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
                print(image.color);
                image.color = Color.green;
                trigger = false;
            }
        }
    }
}
