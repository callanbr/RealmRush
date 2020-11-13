using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {
    [SerializeField] int hitpoints = 10;

    private void OnParticleCollision(GameObject other){
        ProcessHit();
        if (hitpoints <= 0)
        {
            KillEnemy();
        }
    }

    void ProcessHit(){
        hitpoints--;
    }
    private void KillEnemy() {
        Destroy(gameObject);
    }

}
