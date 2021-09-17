using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SaveScript.weaponID < 3)
            ParticleAndLightEffects();
    }

    private void ParticleAndLightEffects()
    {
        float weaponId = SaveScript.weaponID;
        bool fireType = weaponId == 1 ? Input.GetMouseButtonDown(0) : Input.GetMouseButton(0);
        if (Input.GetMouseButton(1) && fireType)
        {
            if (!particleSystem.isPlaying && weaponId != 0)
            {
                particleSystem.Play();
            }
        }
        else
        {
            if (particleSystem.isPlaying)
            {
                particleSystem.Stop();
            }
        }
    }
}
