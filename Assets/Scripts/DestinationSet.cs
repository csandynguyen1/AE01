using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class DestinationSet : MonoBehaviour, MovePosition
{
    private AIPath aiPath;

    private void Awake()
    {
        aiPath = GetComponent<AIPath>();
    }
    public void SetMovePosition(Vector3 movePosition, Action onReachedMovePosition)
    {
        aiPath.destination = movePosition;
    }

}
