using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class AmbienceManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip mainWind;
    [SerializeField]
    private AudioClip secondWind;
    //[SerializeField]
    //private AudioClip animals;
    [SerializeField]
    private AudioSource ambience;

    void FixedUpdate()
    {
        if (!ambience.isPlaying)
        {
            int randNum = Random.Range(0, 5);
            Debug.Log(randNum);

            if(randNum < 3)
                ambience.clip = mainWind;
            else
                ambience.clip = secondWind;

            ambience.Play();
        }
    }
}
