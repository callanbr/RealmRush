using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  [Range(1f, 30f)]
  [SerializeField] float enemySpeed = 10f;
  [SerializeField] ParticleSystem goalParticle;

  void Start()
  {
    PathFinder pathfinder = FindObjectOfType<PathFinder>();
    var path = pathfinder.GetPath();
    StartCoroutine(FollowPath(path));
  }

  IEnumerator FollowPath(List<Waypoint> path)
  {
    print("Starting patrol...");
    foreach (Waypoint waypoint in path)
    {
      // transform.position = waypoint.transform.position;
      // yield return new WaitForSeconds(1f);
      yield return StartCoroutine(SmoothMove(waypoint)); // Use for smooth movement
    }
    SelfDestruct();
  }
  private void SelfDestruct()
  {
    var vfx = Instantiate(goalParticle, transform.position, Quaternion.identity);
    vfx.Play();
    float destroyDelay = vfx.main.duration;
    Destroy(vfx.gameObject, destroyDelay);
    Destroy(gameObject);
  }

  IEnumerator SmoothMove(Waypoint wayPoint)
  {
    while (Vector3.Distance(this.transform.position, wayPoint.transform.position) > enemySpeed * Time.deltaTime)
    {
      this.transform.position = Vector3.MoveTowards(this.transform.position, wayPoint.transform.position, enemySpeed * Time.deltaTime);
      yield return null;
    }
    this.transform.position = wayPoint.transform.position;
  }

}
