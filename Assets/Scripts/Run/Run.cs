using Kryz.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    private RunTypeScriptableObject        currentRunType;
    private RunSegmentScriptableObject    currentRunSegment;
    private int RunSegmentIndex;

    void Start()
    {
        gameManager = (GameManager)FindAnyObjectByType(typeof(GameManager));
        playerController = GetComponent<PlayerController>();
        rigidbody = GetComponent<Rigidbody2D>();
        velocity = inputDirection = Vector2.zero;
        RunSegmentIndex = 0;
        time = moveSpeed = facingDirectionTimeStamp= 0.0f;
        hasInput = false;
    }

    void Update()
    {
        // Get Input
        float inX, inY;
        inX = 0; 
        inY = 0;
        if (playerController.input.inputDevice == InputDevice.keyboard) 
        {
            if (Input.GetKey(playerController.input.upKeyCode))
            {
                inY = 1.0f;
            }
            if (Input.GetKey(playerController.input.downKeyCode))
            {
                inY = -1.0f;
            }
            if (Input.GetKey(playerController.input.rightKeyCode)) 
            {
                inX = 1.0f;
            }
            if (Input.GetKey(playerController.input.leftKeyCode))
            {
                inX = -1.0f;
            }
        }

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
            if (RunSegmentIndex < 0) RunSegmentIndex = 0;
            else if (RunSegmentIndex > currentRunType.accelerationSegments.Length - 1) RunSegmentIndex = currentRunType.accelerationSegments.Length - 1;
            
            currentRunSegment = currentRunType.accelerationSegments[RunSegmentIndex];
            
            if (Mathf.Abs(velocity.magnitude) < currentRunSegment.velocityMinThreshold) RunSegmentIndex--;
            else if (Mathf.Abs(velocity.magnitude) > currentRunSegment.velocityMaxThreshold) RunSegmentIndex++;
            
            moveSpeed = currentRunSegment.speed;
            time = EasingFunctions.GetEasingFunctionFromEnum(currentRunSegment.easingFunction, Time.deltaTime);
        }
        else 
        {
            if (RunSegmentIndex < 0) RunSegmentIndex = 0;
            else if (RunSegmentIndex > currentRunType.decelerationSegments.Length - 1) RunSegmentIndex = currentRunType.decelerationSegments.Length - 1;
            
            currentRunSegment = currentRunType.decelerationSegments[RunSegmentIndex];
            
            if (Mathf.Abs(velocity.magnitude) < currentRunSegment.velocityMinThreshold) RunSegmentIndex--;
            else if (Mathf.Abs(velocity.magnitude) > currentRunSegment.velocityMaxThreshold) RunSegmentIndex++;
            
            moveSpeed = 0;
            time = EasingFunctions.GetEasingFunctionFromEnum(currentRunSegment.easingFunction, Time.deltaTime * currentRunSegment.speed);
        }

        // Set Velocity
        if (currentRunSegment.easingFunction == EasingFunctionEnum.none) 
        {
            velocity = inputDirection * moveSpeed;
        } 
        else 
        {
            velocity = Vector2.Lerp(velocity, inputDirection * moveSpeed, time);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = velocity;
    }

    public void ChangeRunType(RunTypeScriptableObject _RunType) 
    {
        RunSegmentIndex = 0;
        currentRunType = _RunType;
    }
}
