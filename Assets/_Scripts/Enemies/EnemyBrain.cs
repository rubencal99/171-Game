using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : MonoBehaviour, IAgentInput
{
    [field: SerializeField]
    public GameObject Target { get; set; }
    [field: SerializeField]
    public EnemyGun Weapon { get; set; }

    [field: SerializeField]
    public AIState CurrentState { get; set; }

    [field: SerializeField]
    public UnityEvent OnFireButtonPressed { get; set; }

    [field: SerializeField]
    public UnityEvent OnFireButtonReleased { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector2> OnMovementKeyPressed { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector2> OnPointerPositionChange { get; set; }

    [field: SerializeField]
    public UnityEvent OnReloadButtonPressed { get; set; }

    private void Update()
    {
        if (Target == null)
        {
            OnMovementKeyPressed?.Invoke(Vector2.zero);
        }
        CurrentState.UpdateState();
    }

    public void Attack()
    {
        OnFireButtonPressed?.Invoke();
    }

    public void Move(Vector2 movementDirection)
    {
        OnMovementKeyPressed?.Invoke(movementDirection);
    }

    public void Aim(Vector2 targetPosition)
    {   
        OnPointerPositionChange?.Invoke(targetPosition);
    }

    private void Awake()
    {
        Target = FindObjectOfType<Player>().gameObject;
        Weapon = transform.GetComponentInChildren<EnemyGun>();
    }

    private void Start()
    {
        // Target = FindObjectOfType<Player>().gameObject;
    }

    internal void ChangetoState(AIState State)
    {
        CurrentState = State;
    }
}
