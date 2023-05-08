using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEditor;
using UnityEngine;

public class GrindingRail : MonoBehaviour, IInteractable
{
    // Array of transform points that define the rail's path
    public Transform[] railPoints;

    // Called when the player interacts with the rail
    public void Interact(PlayerController player)
    {
        Debug.Log(("Interacted with the rail"));
        player.EnterGrinding(railPoints);
    }

    // Checks if a position is within the x bounds of the rail
    public static bool IsWithinXBounds(Vector3 pos, Transform[] rail)
    {
        // Returns true if the position's x value is between the first and last point's x values of the rail
        return pos.x < rail[^1].position.x && pos.x > rail[0].position.x;
    }

    // Returns the position on the rail closest to the given position
    public static Vector3 GetGrindPosition(Vector3 pos, Transform[] rail)
    {
        // Finds the points on the rail behind and in front of the position
        Transform from = GetBehind(pos, rail);
        Transform to = GetFront(pos, rail);

        if (from == null || to == null)
        {
            return pos;
        }
        
        // Calculates the position on the rail using linear interpolation
        return Vector3.Lerp(from.position, to.position, ProgressBetweenPoints(pos, from, to));
    }

    // Calculates the progress between two points on the x axis based on a given position
    private static float ProgressBetweenPoints(Vector3 pos, Transform from, Transform to)
    {
        // Calculates the full distance between the two points
        float distance = DistanceBetweenPointsX(from.position, to.position);

        // Calculates the distance from the "from" point to the position
        float playerDistance = DistanceBetweenPointsX(from.position, pos);

        // Returns the progress between the two points based on the player's distance
        return playerDistance / distance;
    }
    
    // Calculates the distance between two points on the x axis
    private static float DistanceBetweenPointsX(Vector3 from, Vector3 to)
    {
        return Mathf.Abs(from.x - to.x);
    }

    // Finds the point on the rail behind the given position
    private static Transform GetBehind(Vector3 pos, Transform[] rail)
    {
        for (int i = rail.Length - 1; i >= 0; i--)
        {
            // Returns the point if it is behind the position
            if (rail[i].position.x < pos.x) return rail[i];
        }
        
        // Returns null if no point is found
        return null;
    }

    // Finds the point on the rail in front of the given position
    private static Transform GetFront(Vector3 pos, Transform[] rail)
    {
        for (int i = 0; i < rail.Length; i++)
        {
            // Returns the point if it is in front of the position
            if (rail[i].position.x > pos.x) return rail[i];
        }

        // Returns null if no point is found
        return null;
    }
}
