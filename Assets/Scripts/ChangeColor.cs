using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    public bool trigger=true;

    void Update()
    {
        if (gameObject.activeSelf && trigger)
        {
            Slider slider = GetComponent<Slider>();
            slider.value += Time.deltaTime;
            if (slider.value >= slider.maxValue)
            {
                Image image = slider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
                image.color = Color.green;
                trigger = false;
            }
        }
    }
}
