using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyInput : MonoBehaviour, IAgentInput
{
    public UnityEvent OnFireButtonPressed { get; set; }
    public UnityEvent OnFireButtonReleased { get; set; }
    public UnityEvent<Vector3> OnMovementKeyPressed { get; set; }
    public UnityEvent<Vector3> OnPointerPositionChange { get; set; }
    public UnityEvent OnReloadButtonPressed { get; set; }
}
