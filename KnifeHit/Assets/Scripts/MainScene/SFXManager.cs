using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    public ParticleSystem ImpactParticle;

    
    private void OnEnable() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void playImpact(Vector3 hitPoint) {
        ImpactParticle.transform.position = hitPoint;
        ImpactParticle.Play();
    }
}
