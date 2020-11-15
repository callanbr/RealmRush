﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
  [Range(0.1f, 60f)]
  [SerializeField] float secondsBetweenSpans = 5f;
  [SerializeField] EnemyMovement enemyPrefab;
  [SerializeField] Transform enemyParentTransform;
  [SerializeField] Text spawnedEnemies;
  int score;

  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(RepeatedlySpawnEnemies());
    spawnedEnemies.text = score.ToString();
  }

  IEnumerator RepeatedlySpawnEnemies()
  {
    while (true) //forever
    {
      AddScore();
      var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
      newEnemy.transform.parent = enemyParentTransform;
      yield return new WaitForSeconds(secondsBetweenSpans);
    }
  }

  private void AddScore()
  {
    score++;
    spawnedEnemies.text = score.ToString();
  }
}
