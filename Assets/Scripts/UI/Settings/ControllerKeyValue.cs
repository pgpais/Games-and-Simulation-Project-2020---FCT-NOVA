using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerKeyValue : MonoBehaviour
{

    public InputAction action;
    
    [SerializeField] 
    private TMP_Text label;
    [SerializeField]
    private TMP_Text value;
    
    // Start is called before the first frame update
    void Start()
    {
        label.text = action.name;
        // This can be, later on parameterized
        value.text = GetMapping(new string[]
        {
            "Keyboard",
            "Mouse"
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string GetMapping(string[] controllers)
    {
        foreach (var bind in action.bindings)
        {
            string[] paths = bind.path.Split('/');

            if (controllers.Any(controller => paths[0].Contains(controller)))
            {
                return paths[1];
            }
        }

        return "NO VALUE";
    }
}
