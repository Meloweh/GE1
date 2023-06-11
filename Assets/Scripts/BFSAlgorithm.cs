using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BFSAlgorithm {
    private List<Vector2Int> path;
    private Vector2Int[] directions = { new (0, -1), new (-1, 0), new (0, 1), new (1, 0) };
    private byte defaultLayer = 0;

    public bool CanWalk(Vector3Int position) {
        Tilemap[] tilemaps = UnityEngine.Object.FindObjectsOfType<Tilemap>();

        foreach (Tilemap tilemap in tilemaps) {
            if (tilemap.HasTile(position)) {
                // You would replace "Walkable" and "NonWalkable" with the names of your actual layers
                if (tilemap.gameObject.layer == LayerMask.NameToLayer("Alpha")) {
//                    Debug.LogWarning(tilemap.gameObject.name);
                    return false;
                }
            }
        }

        return true;
    }
    
    public bool CanWalk(Vector2Int position) {
        return CanWalk(new Vector3Int(position.x, position.y, 0));
    }

    public bool CanWalk(Vector2 position) {
        return CanWalk(Vector2Int.RoundToInt(position));
    }
    
    public Vector2Int[] ShuffleDirections() {
        for (int i = directions.Length - 1; i > 0; i--) {
            int j = UnityEngine.Random.Range(0, i + 1);
            (directions[i], directions[j]) = (directions[j], directions[i]);
        }

        return directions;
    }
    public void Calculate(Vector2Int start, Vector2Int target) {
        Queue<Vector2Int> frontier = new Queue<Vector2Int>();
        frontier.Enqueue(start);
        //var maxDist = (int)(1.5f * Vector2Int.Distance(start, target));

        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        cameFrom[start] = start;

        while (frontier.Count > 0) {
            Vector2Int current = frontier.Dequeue();
            //ShuffleDirections();
            foreach (Vector2Int dir in directions) { // directions is a list of adjacent cells (up, down, left, right)
                Vector2Int next = current + dir;

                if (!cameFrom.ContainsKey(next) && CanWalk(new Vector3Int(next.x, next.y, 0))) { // CanWalk() is a function that checks if a tile is walkable
                    frontier.Enqueue(next);
                    cameFrom[next] = current;

                    // Stop if we reached our target
                    if (next == target) {
                        break;
                    }
                }
            }
        }
        
        path = new List<Vector2Int>();
        Vector2Int currentTile = target;
        while (currentTile != start) {
            path.Add(currentTile);
            if (cameFrom.ContainsKey(currentTile)) {
                currentTile = cameFrom[currentTile];
            }
            else {
                break;
            }
            
        }
        path.Reverse();
    }

    public Vector2Int Poll() {
        if (path.Count < 1) return Vector2Int.zero;
        Vector2Int next = path[0];
        path.RemoveAt(0);
        return next;
    }

    public bool HasNext() {
        return path != null && path.Count > 0;
    }
}
