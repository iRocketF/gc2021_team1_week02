using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatAudio : MonoBehaviour
{
    public AudioSource treatSound;

    void Awake()
    {
        treatSound = GetComponent<AudioSource>();
    }

    public void SetSound(string treatName)
    {
        if (treatName == "treat_apple")
        {
            // Debug.Log("Loaded apple sounds");
            treatSound.clip = Resources.Load<AudioClip>("Sounds/Treats/treat_bite2");
            treatSound.Play();
        }
        else if (treatName == "treat_cookie")
        {
            // Debug.Log("Loaded cookie sounds");
            treatSound.clip = Resources.Load<AudioClip>("Sounds/Treats/treat_bite3");
            treatSound.Play();
        }
        else if (treatName == "treat_cupcake")
        {
            // Debug.Log("Loaded apple sounds");
            treatSound.clip = Resources.Load<AudioClip>("Sounds/Treats/treat_bite4");
            treatSound.Play();
        }
    }

}
