using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEditor;
using UnityEngine;

public class GrindingRail : MonoBehaviour, IInteractable
{
    public Transform[] railPoints;

    public void Interact(PlayerController player)
    {
        Debug.Log(("Interacted with the rail"));
        player.EnterGrinding(railPoints);
    }

    public static bool IsWithinXBounds(Vector3 pos, Transform[] rail)
    {
        return pos.x < rail[rail.Length - 1].position.x && pos.x > rail[0].position.x;
    }

    public static Vector3 GetGrindPosition(Vector3 pos, Transform[] rail)
    {
        Transform from = GetBehind(pos, rail);
        Transform to = GetFront(pos, rail);
        
        return Vector3.Lerp(from.position, to.position, ProgressBetweenPoints(pos, from, to));
    }

    private static float ProgressBetweenPoints(Vector3 pos, Transform from, Transform to)
    {
        //full distance
        float distance = DistanceBetweenPointsX(from.position, to.position);

        //form to player
        float playerDistance = DistanceBetweenPointsX(from.position, pos);

        return playerDistance / distance;
    }
    
    private static float DistanceBetweenPointsX(Vector3 from, Vector3 to)
    {
        return Mathf.Abs(from.x - to.x);
    }

    private static Transform GetBehind(Vector3 pos, Transform[] rail)
    {
        for (int i = rail.Length - 1; i >= 0; i--)
        {
            if (rail[i].position.x < pos.x) return rail[i];
        }
        
        return null;
    }
    private static Transform GetFront(Vector3 pos, Transform[] rail)
    {
        for (int i = 0; i < rail.Length; i++)
        {
            if (rail[i].position.x > pos.x) return rail[i];
        }

        return null;
    }
}
