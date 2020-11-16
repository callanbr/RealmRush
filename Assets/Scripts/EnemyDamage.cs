using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
  [SerializeField] int hitpoints = 10;
  [SerializeField] ParticleSystem hitParticlePrefab;
  [SerializeField] ParticleSystem killParticlePrefab;
  [SerializeField] AudioClip enemyDamageSFX;
  [SerializeField] AudioClip enemyDeathSFX;

  AudioSource myAudioSource;

  private void Start()
  {
    myAudioSource = GetComponent<AudioSource>();
  }

  private void OnParticleCollision(GameObject other)
  {

    ProcessHit();
    if (hitpoints <= 0)
    {
      KillEnemy();
    }
  }

  void ProcessHit()
  {
    hitpoints--;
    myAudioSource.PlayOneShot(enemyDamageSFX);
    hitParticlePrefab.Play();
  }
  private void KillEnemy()
  {
    // myAudioSource.PlayOneShot(enemyDeathSFX);
    var vfx = Instantiate(killParticlePrefab, transform.position, Quaternion.identity);
    vfx.Play();
    float destroyDelay = vfx.main.duration;
    Destroy(vfx.gameObject, destroyDelay);
    Destroy(gameObject);
  }

}
