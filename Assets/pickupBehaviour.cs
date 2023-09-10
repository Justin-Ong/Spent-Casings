using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeTime;
    public string pickupType;

    protected float currTime = 0;

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        Color pickupMaterialColour = this.GetComponent<Renderer>().material.color;
        float fadeAmount = Mathf.Lerp(1, 0, currTime / lifeTime);
        this.GetComponent<Renderer>().material.color = new Color(pickupMaterialColour.r, pickupMaterialColour.g, pickupMaterialColour.b, fadeAmount);

        if (currTime > lifeTime)
        {
            gameObject.SetActive(false);
        }
    }
}
