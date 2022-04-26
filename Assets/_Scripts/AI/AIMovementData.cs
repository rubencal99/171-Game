using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will be responsible for direction and target data
public class AIMovementData : MonoBehaviour
{
    [field: SerializeField]
    public Vector3 Direction { get; set; }

    [field: SerializeField]
    public Vector3 PointOfInterest { get; set; }

    [field: SerializeField]
    public Vector3 PointerPosition { get; set; }

    // Steering Variables
    /*public float MaxSpeed;
    public float SteerForce;
    public float LookAhead;
    public int NumRays;

    // Context Arrays
    Vector2[] rayDirections;
    Vector2[] interest;
    Vector2[] danger;

    // Movement Vectors
    Vector2 chosenDirection;
    Vector2 velocity;
    Vector2 acceleration;

    public void Awake()
    {
        MaxSpeed = transform.parent.GetComponent<AgentMovement>().MovementData.maxRunSpeed;
        chosenDirection = Vector2.zero;
        velocity = Vector2.zero;
        acceleration = Vector2.zero;

        ResizeArrays();
    }

    void ResizeArrays()
    {
        rayDirections = new Vector2[NumRays];
        interest = new Vector2[NumRays];
        danger = new Vector2[NumRays];

        for(int i = 0; i < NumRays; i++)
        {
            var angle = i * 2 * Mathf.PI / NumRays;
            rayDirections[i] = Vector2.right * angle;
        }
    }

    void PhysicsProcess(float delta)
    {
        SetInterest();
        SetDanger();
        ChooseDirection();
    }*/

}
