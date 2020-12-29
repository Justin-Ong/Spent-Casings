using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionBehaviour : MonoBehaviour
{
    public Vector3 maxSize;
    public float lifetime;

    private float currLifetime;

    // Start is called before the first frame update
    void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        currLifetime += Time.deltaTime;
        transform.localScale = Vector3.Lerp(Vector3.zero, maxSize, currLifetime / lifetime);
        if (currLifetime > lifetime)
        {
            Destroy(gameObject);
        }
    }
}
