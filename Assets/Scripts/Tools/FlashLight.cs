using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLight : Tool
{
    [SerializeField] private Light flashLight;
    private AudioSource _audio;
    
    
    // Start is called before the first frame update
    void Start()
    {
        flashLight = flashLight == null ? GetComponentInChildren<Light>() : flashLight;
        _audio = gameObject.GetComponent<AudioSource>();
        Debug.Log(_audio);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void UseTool(InputActionPhase phase)
    {
        if (phase == InputActionPhase.Performed)
        {
            _audio.Play();
            flashLight.enabled = !flashLight.enabled;
        }
    }

    public override void UseToolSecondary(InputActionPhase phase)
    {
        
    }
}