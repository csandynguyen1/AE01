using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public interface MovePosition
{
    void SetMovePosition(Vector3 movePosition, Action onReachedMovePosition);
}
