using UnityEngine;
using UnityEngine.Events;

public interface IAgentInput
{
    UnityEvent OnFireButtonPressed { get; set; }
    UnityEvent OnFireButtonReleased { get; set; }
    UnityEvent<Vector3> OnMovementKeyPressed { get; set; }
    UnityEvent<Vector3> OnPointerPositionChange { get; set; }
    UnityEvent OnReloadButtonPressed { get; set; }
}