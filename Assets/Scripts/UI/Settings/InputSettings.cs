using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputSettings : MonoBehaviour
{

    public GameObject inputPrefab;
    
    [SerializeField] private InputActionAsset playerControls;


    private ReadOnlyArray<InputAction> _inputs;
    private GameObject[] _inputList;
    
    // Start is called before the first frame update
    void Start()
    {
        var gameplayActionMap = playerControls.FindActionMap("Player");

        _inputs = gameplayActionMap.actions;

        Debug.Log($"Found {_inputs.Count} input Actions");

        _inputList = new GameObject[_inputs.Count];
        
        for (var i = 0; i < _inputs.Count; i++)
        {
            _inputList[i] = Instantiate(inputPrefab, this.transform);
            //_inputList[i].transform.parent = this.transform;
            _inputList[i].GetComponent<ControllerKeyValue>().action = _inputs[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
