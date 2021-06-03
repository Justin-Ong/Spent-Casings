using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBehaviour : MonoBehaviour
{
    public GameObject countdown;
    public GameObject bombCounter;
    public GameObject timeSurvivedDisplay;

    private float timeSurvived = 0;
    private Text UIText;

    void Awake()
    {
        References.canvas = gameObject;
        References.countdown = Instantiate(countdown, References.canvas.transform);
        References.bombCounter = Instantiate(bombCounter, References.canvas.transform);
        References.timeSurvivedDisplay = Instantiate(timeSurvivedDisplay, References.canvas.transform);
        UIText = References.timeSurvivedDisplay.GetComponent<UnityEngine.UI.Text>();
    }

    void LateUpdate()
    {
        if (References.isAlive)
        {
            timeSurvived += Time.deltaTime;
            UIText.text = timeSurvived.ToString();
            References.addedSpeed = Mathf.Ceil(timeSurvived / 10);
            References.maxSpeed = References.initialSpeed + References.addedSpeed;
        }
    }
}
