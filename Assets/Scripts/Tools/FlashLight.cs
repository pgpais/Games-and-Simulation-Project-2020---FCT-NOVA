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
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void UseTool(InputActionPhase phase)
    {
        if (phase == InputActionPhase.Performed)
        {
            flashLight.enabled = !flashLight.enabled;
            _audio.Play();
        }
            
    }

    public override void UseToolSecondary(InputActionPhase phase)
    {
        
    }
}