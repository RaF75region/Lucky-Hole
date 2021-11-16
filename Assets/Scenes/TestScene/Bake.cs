using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        var flag = StaticEditorFlags.NavigationStatic;
        GameObjectUtility.SetStaticEditorFlags(gameObject, flag);
    }
}
