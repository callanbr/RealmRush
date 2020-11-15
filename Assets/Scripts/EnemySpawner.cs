﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [Range(0.1f, 60f)]
  [SerializeField] float secondsBetweenSpans = 5f;
  [SerializeField] EnemyMovement enemyPrefab;
  [SerializeField] Transform enemyParentTransform;

  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(RepeatedlySpawnEnemies());
  }

  IEnumerator RepeatedlySpawnEnemies()
  {
    while (true) //forever
    {
      var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
      newEnemy.transform.parent = enemyParentTransform;
      yield return new WaitForSeconds(secondsBetweenSpans);
    }
  }
}
