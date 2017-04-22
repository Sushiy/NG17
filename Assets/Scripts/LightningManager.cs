using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
    public Light lightning_Left;
    public Light lightning_Right;

    //How much time between lightningstrike groups
    public float minLightningDelay = 5.0f;
    public float maxLightningDelay = 10.0f;

    //How much time between each strike
    public float strikingDelay = 1.0f;

    public AudioClip windupLightning;
    public AudioClip instantLightning;

    public bool isLightningStriking = false;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(LightningLoop());
	}

    private void Update()
    {
        //reset islighntingstriking on the next frame
        if (isLightningStriking)
            isLightningStriking = false;
    }

    private IEnumerator LightningLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minLightningDelay, maxLightningDelay));
            Debug.Log("Lightning should now strike");
            isLightningStriking = true;
            bool leftLightning = Random.value > 0.5f;
            Light activeLight;
            if (leftLightning)
                activeLight = lightning_Left;
            else
                activeLight = lightning_Right;

            int numberOfStrikes = Random.Range(1, 4);
            StartCoroutine(LightningStrike(numberOfStrikes, activeLight));
        }

    }

    IEnumerator LightningStrike(int numberOfStrikes, Light activeLight)
    {
        AudioSource audiosrc = activeLight.GetComponent<AudioSource>();
        audiosrc.clip = windupLightning;
        audiosrc.Play();
        for (int i = 0; i < numberOfStrikes; i++)
        {
            if (i > 0)
            {
                audiosrc.clip = instantLightning;
                audiosrc.Play();
            }
            activeLight.intensity = 4.0f;
            yield return new WaitForSeconds(Random.Range(0.05f, 0.25f));
            activeLight.intensity = 0.4f;
            yield return null;
        }
    }
}
