using Kryz.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FacingDirection { north, south, east, west, northEast, northWest, southEast, southWest }

public class Run : MonoBehaviour
{
    public PlayerController playerController;
    public GameManager gameManager;
    public Vector2 velocity;

    private new Rigidbody2D rigidbody;

    private Vector2         inputDirection;
    private bool            hasInput;
    private float           moveSpeed;
    private float           time;
    private float           facingDirectionTimeStamp;
    private FacingDirection facingDirection;

    private MovementTypeScriptableObject        currentMovementType;
    private MovementSegmentsScriptableObject    currentMovementSegment;
    private int movementSegmentIndex;

    void Start()
    {
        gameManager = (GameManager)FindAnyObjectByType(typeof(GameManager));
        playerController = GetComponent<PlayerController>();
        rigidbody = GetComponent<Rigidbody2D>();
        velocity = inputDirection = Vector2.zero;
        movementSegmentIndex = 0;
        time = moveSpeed = facingDirectionTimeStamp= 0.0f;
        hasInput = false;
    }

    void Update()
    {
        // Get Movement Type From Stage
        currentMovementType ??= gameManager.currentStage.movementType;
        if (currentMovementType.id != gameManager.currentStage.movementType.id)
        {
            this.transform.position = gameManager.currentRoom.centerPoint.transform.position;
            currentMovementType = gameManager.currentStage.movementType;
            movementSegmentIndex = 0;
        }
        // Get Input
        float inX = Input.GetAxisRaw("Horizontal");
        float inY = Input.GetAxisRaw("Vertical");
        inputDirection = new Vector2(inX, inY).normalized;
        if (inX != 0 || inY != 0) hasInput = true;
        else hasInput = false;

        // Set Facing Direction
        facingDirectionTimeStamp += Time.deltaTime;
        if (facingDirectionTimeStamp > 0.15f) 
        {
            if (hasInput)
            {
                bool angled = false;
                if (inX != 0 && inY != 0) angled = true;
                if (inX > 0)
                {
                    if (angled) 
                    {
                        if (inY > 0) facingDirection = FacingDirection.northEast;
                        else facingDirection = FacingDirection.southEast;
                    } 
                    else facingDirection = FacingDirection.east;
                }
                else if (inX < 0)
                {
                    if (angled)
                    {
                        if (inY > 0) facingDirection = FacingDirection.northWest;
                        else facingDirection = FacingDirection.southWest;
                    }
                    else facingDirection = FacingDirection.west;
                }
                else if (inY > 0) facingDirection = FacingDirection.north;
                else if (inY < 0) facingDirection = FacingDirection.south;
                playerController.facingDirection = facingDirection;
            }
            facingDirectionTimeStamp = 0.0f;
        }

        // Set Acceleration Mode
        if (hasInput)
        {
            if (movementSegmentIndex < 0) movementSegmentIndex = 0;
            else if (movementSegmentIndex > currentMovementType.accelerationSegments.Length - 1) movementSegmentIndex = currentMovementType.accelerationSegments.Length - 1;
            
            currentMovementSegment = currentMovementType.accelerationSegments[movementSegmentIndex];
            
            if (Mathf.Abs(velocity.magnitude) < currentMovementSegment.velocityMinThreshold) movementSegmentIndex--;
            else if (Mathf.Abs(velocity.magnitude) > currentMovementSegment.velocityMaxThreshold) movementSegmentIndex++;
            
            moveSpeed = currentMovementSegment.speed;
            time = EasingFunctions.GetEasingFunctionFromEnum(currentMovementSegment.easingFunction, Time.deltaTime);
        }
        else 
        {
            if (movementSegmentIndex < 0) movementSegmentIndex = 0;
            else if (movementSegmentIndex > currentMovementType.decelerationSegments.Length - 1) movementSegmentIndex = currentMovementType.decelerationSegments.Length - 1;
            
            currentMovementSegment = currentMovementType.decelerationSegments[movementSegmentIndex];
            
            if (Mathf.Abs(velocity.magnitude) < currentMovementSegment.velocityMinThreshold) movementSegmentIndex--;
            else if (Mathf.Abs(velocity.magnitude) > currentMovementSegment.velocityMaxThreshold) movementSegmentIndex++;
            
            moveSpeed = 0;
            time = EasingFunctions.GetEasingFunctionFromEnum(currentMovementSegment.easingFunction, Time.deltaTime * currentMovementSegment.speed);
        }

        // Set Velocity
        velocity = Vector2.Lerp(velocity, inputDirection * moveSpeed, time);
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = velocity;
    }

    public void ChangeMovementType(MovementTypeScriptableObject _movementType) 
    {
        movementSegmentIndex = 0;
        currentMovementType = _movementType;
    }
}
