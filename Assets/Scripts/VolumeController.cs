using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private AudioSource backgroundMusic;
    Vector3 startingRotation;
    Vector3 currentRotation;
    public Text volumeLevel;

    // Start is called before the first frame update
    void Start()
    {
        volumeLevel.text = "Volume: 50";
        backgroundMusic = GetComponent<AudioSource>();
        backgroundMusic.enabled = true;
        backgroundMusic.volume = 0.5f;
        startingRotation = transform.eulerAngles;
        //Debug.Log(startingRotation);
    }
    
    // Update is called once per frame
    void Update()
    {
        volumeLevel.text = "Volume: " + (int)(backgroundMusic.volume * 100);
        currentRotation = transform.eulerAngles;
        //Debug.Log(currentRotation);
        if (currentRotation.x < startingRotation.x)
        {
            increaseVolume();
        }
        else if (currentRotation.x > startingRotation.x)
        {
            decreaseVolume();
        } 
        else //if rotation is == startingRotation
        {
            backgroundMusic.volume = 0.5f;
        }
    }

    private void increaseVolume()
    {
        if (backgroundMusic.volume < 1)
        {
            backgroundMusic.volume += 0.001f;
        }

    }

    private void decreaseVolume()
    {
        if (backgroundMusic.volume > 0)
        {
            backgroundMusic.volume -= 0.001f;
        }
        
    }
}
