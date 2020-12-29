using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBehaviour : MonoBehaviour
{
    public GameObject countdown;
    public GameObject bombCounter;
    public GameObject timeSurvivedDisplay;

    private float timeSurvived = 0;

    void Awake()
    {
        References.canvas = gameObject;
        References.countdown = Instantiate(countdown, References.canvas.transform);
        References.bombCounter = Instantiate(bombCounter, References.canvas.transform);
        References.timeSurvivedDisplay = Instantiate(timeSurvivedDisplay, References.canvas.transform);
    }

    void Update()
    {
        timeSurvived += Time.deltaTime;
        References.timeSurvivedDisplay.GetComponent<UnityEngine.UI.Text>().text = timeSurvived.ToString();
    }
}
