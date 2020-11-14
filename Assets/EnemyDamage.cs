using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {
    [SerializeField] int hitpoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem killParticlePrefab;

    private void OnParticleCollision(GameObject other){
        ProcessHit();
        if (hitpoints <= 0)
        {
            KillEnemy();
        }
    }

    void ProcessHit(){
        hitpoints--;
        hitParticlePrefab.Play();
    }
    private void KillEnemy() {
        var vfx = Instantiate(killParticlePrefab, transform.position, Quaternion.identity);
        vfx.Play();
        Destroy(gameObject);
    }

}
