using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusicInstance : MonoBehaviour
{
    private static BackGroundMusicInstance _instance;

    // Start is called before the first frame update
    void Awake()
    {

        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(this);
            return;
        }
        
        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        GetComponent<AudioSource>().Play();
    }
    
}
