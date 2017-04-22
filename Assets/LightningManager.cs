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

    public AnimationCurve curve;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(LightningStrikes());
	}

    private IEnumerator LightningStrikes()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minLightningDelay, maxLightningDelay));
            Debug.Log("Lightning should now strike");
            bool leftLightning = Random.value > 0.5f;
            Light activeLight;
            if (leftLightning)
                activeLight = lightning_Left;
            else
                activeLight = lightning_Right;

            int numberOfStrikes = Random.Range(1, 4);
            Quaternion standardRotation = activeLight.transform.localRotation;
            for (int i = 0; i < numberOfStrikes; i++)
            {
                Quaternion q = Quaternion.Euler(Random.Range(-5.0f, 5.0f), Random.Range(-15.0f, 15.0f), 0.0f);
                activeLight.transform.localRotation = standardRotation * q;
                float lerpTime = 0.0f;
                
                activeLight.intensity = 4.0f;
                yield return new WaitForSeconds(Random.Range(0.05f,0.25f));
                activeLight.intensity = 0.4f;
                while (lerpTime <= 1.0f)
                {
                    activeLight.intensity = curve.Evaluate(lerpTime) * 4.0f;
                    lerpTime += Time.deltaTime;
                }
                yield return null;
            }
            activeLight.transform.localRotation = standardRotation;

        }

    }
}
