using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : Tool
{

    [SerializeField] private Light flashLight;
    // Start is called before the first frame update
    void Start()
    {
        flashLight = flashLight == null ? GetComponentInChildren<Light>() : flashLight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void UseTool()
    {
        flashLight.enabled = !flashLight.enabled;
    }
}
