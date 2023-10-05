using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Vuforia;

public class VolumeController : MonoBehaviour
{

    [SerializeField] AudioSource backgroundMusic;
    Quaternion startingRotation;
    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic.enabled = true;
        backgroundMusic.volume = 1;
        startingRotation = transform.rotation;
        Debug.Log(startingRotation);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.x > startingRotation.x)
        {
            increaseVolume();
        }
        else if (transform.rotation.x < startingRotation.x)
        {
            decreaseVolume();
        } 
        else //if rotation is == startingRotation
        {
            backgroundMusic.volume = 1;
        }
    }

    private void increaseVolume()
    {
        if (backgroundMusic.volume < 1)
        {
            backgroundMusic.volume++;
        }

    }

    private void decreaseVolume()
    {
        if (backgroundMusic.volume > 0)
        {
            backgroundMusic.volume--;
        }
        
    }
}
