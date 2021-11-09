using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnClient : MonoBehaviour
{
    public GameObject prefabClient;

    private float timer = 0f;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            Instantiate(prefabClient);
            timer = 0;
        }
    }
}
