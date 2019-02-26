using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public Vector2 targetPosition;
    public Vector2 startPosition;

    void Start()
    {
        targetPosition = transform.position;
        startPosition = transform.position;
    }

    public void SetTarget(Vector2 target)
    {
        targetPosition = target;
    }

    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * 5);
    }

}
