using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ExpandLight : MonoBehaviour
{
    public Light2D light2D;
    public float expansionDuration = 3f;
    public float maxIntensity = 1f;

    private bool isExpanding = false;
    public bool isMother;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isExpanding)
        {
            StartCoroutine(BurstLight());
            if (isMother)
            {
                SoundManager.PlaySound(SoundManager.Sound.Call, false, false);
            }
            else
            {
                SoundManager.PlaySound(SoundManager.Sound.Child_Call, false, false);
            }
        }
    }

    IEnumerator BurstLight()
    {
        isExpanding = true;

        // Initial intensity is 0
        light2D.intensity = 0f;

        // Gradually increase intensity over duration
        float timer = 0f;
        while (timer < expansionDuration)
        {
            light2D.intensity = Mathf.Lerp(0f, maxIntensity, timer / expansionDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        // Wait for a short duration with the light at max intensity
        yield return new WaitForSeconds(0.5f);

        timer = 0f;
        while (timer < 0.5f)
        {
            light2D.intensity = Mathf.Lerp(maxIntensity, 0f, timer / 0.5f);
            timer += Time.deltaTime;
            yield return null;
        }

        light2D.intensity = 0f;

        isExpanding = false;
    }
}
