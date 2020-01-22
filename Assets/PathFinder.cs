using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour{

    [SerializeField] Waypoint startWaypoint, endWaypoint;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    Waypoint searchCenter;

    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    void Start(){
        LoadBlocks();
        ColorStartAndEnd();
        PathFind();
        // ExploreNeighbours();
    }

    private void PathFind(){
        queue.Enqueue(startWaypoint);
        while(queue.Count > 0 && isRunning){
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            HaltIfEndFound();
            ExploreNeighbours();
        }
    }

    private void HaltIfEndFound(){
        if (searchCenter == endWaypoint)
        {
            print("Start and end on same block: " + searchCenter); // todo remove log
            isRunning = false;
        }
    }

    private void ExploreNeighbours(){
        if(!isRunning){return;}
        foreach (Vector2Int direction in directions){
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            try
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
            catch {} //Do nothing for now.
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if(neighbour.isExplored || queue.Contains(neighbour)){ //do nothing
        } else {
            // neighbour.SetTopColor(Color.blue); //Changes exploted color
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }
    }

    private void ColorStartAndEnd(){
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    private void LoadBlocks(){
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints){
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos)) {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            } else {
                grid.Add(gridPos, waypoint);
            }
        }
    }

    void Update(){
        
    }
}
