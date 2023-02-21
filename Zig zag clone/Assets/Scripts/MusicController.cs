using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController musicController { get; private set; }

    private void Awake()
    {
        if(musicController == null)
        {
            musicController = this;
        }else if(musicController != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
    }
}
