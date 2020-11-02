using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music _singletonMusic;

    void Awake()
    {
        if (_singletonMusic == null)
        {
            _singletonMusic = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
            Destroy(gameObject);
    }
}