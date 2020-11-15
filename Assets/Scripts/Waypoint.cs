﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

  [SerializeField] Tower towerPrefab;

  public bool isExplored = false;
  public Waypoint exploredFrom;
  public bool isPlaceable = true;

  Vector2Int gridPos;
  const int gridSize = 10;

  public int GetGridSize()
  {
    return gridSize;
  }

  public Vector2Int GetGridPos()
  {
    return new Vector2Int(
        Mathf.RoundToInt(transform.position.x / gridSize),
        Mathf.RoundToInt(transform.position.z / gridSize)
    );
  }
  void OnMouseOver()
  {
    if (Input.GetMouseButtonDown(0))
    {
      if (isPlaceable)
      {
        Instantiate(towerPrefab, transform.position, Quaternion.identity);
        isPlaceable = false;
        print(gameObject.name);
      }
      else
      {
        print("Blocked!");
      }
    }
  }
}
