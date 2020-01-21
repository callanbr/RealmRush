using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour{

    [SerializeField] Waypoint startWaypoint, endWaypoint;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;

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
            var searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            print("Searching from: " + searchCenter); // todo remove log
            HaltIfEndFound(searchCenter);
            ExploreNeighbours(searchCenter);
        }
    }

    private void HaltIfEndFound(Waypoint searchCenter){
        if (searchCenter == endWaypoint)
        {
            print("Start and end on same block: " + searchCenter); // todo remove log
            isRunning = false;
        }
    }

    private void ExploreNeighbours(Waypoint from){
        if(!isRunning){return;}
        foreach (Vector2Int direction in directions){
            Vector2Int neighbourCoordinates = from.GetGridPos() + direction;
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
        if(neighbour.isExplored){ //do nothing
        } else {
            neighbour.SetTopColor(Color.blue); // todo move later
            queue.Enqueue(neighbour);
            print("Queueing: " + neighbour);
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
