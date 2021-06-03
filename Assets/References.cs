using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class References
{
    public static GameObject player;
    public static GameObject canvas;
    public static GameObject bombCounter;
    public static GameObject timeSurvivedDisplay;
    public static GameObject countdown;
    public static float score = 0;
    public static bool hasStarted = false;
    public static bool isAlive = true;
    public static float maxSpeed = 15;
    public static float addedSpeed = 0;
    public static float initialSpeed = 15;
}
