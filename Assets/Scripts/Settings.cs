using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Settings")]
public class Settings : ScriptableObject {

    [Header("Paddle setting")]
    public float paddleMaxPositionY;
    public float paddleMinPositionY;
    public float paddlePositionX;
    public float paddleSpeed;

    [Header("Ball setting")]
    public float minBallSpeed;  // per axis
    public float maxBallSpeed;  // per axis
    public float updateBallSpeedInterval;
    public float updateBallSpeedFactor;

    [Header("General setting")]
    public int pointsPerGoal;
    public int pointsToWin;
    public float messageDisplayTime;
    public KeyCode pauseKey;

    [Header("Input setting")]
    public KeyCode player1UpKey;
    public KeyCode player1DownKey;
    public KeyCode player2UpKey;
    public KeyCode player2DownKey;
}
