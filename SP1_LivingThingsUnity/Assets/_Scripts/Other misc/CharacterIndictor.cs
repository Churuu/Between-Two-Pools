using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterIndictor : MonoBehaviour
{

    public Transform otter;
    public Image otterIndicator;
    public Camera mainCamera;

    Vector2 center;


    void Start()
    {
        center = new Vector2(0.5f, 0.5f);
    }

    void Update()
    {
        UpdateIndicators();
    }

    void UpdateIndicators()
    {
        Vector3 centerWorldPoint = mainCamera.ViewportToWorldPoint(center);
        Vector2 dir = otter.position - centerWorldPoint;
        print(centerWorldPoint);
        Debug.DrawRay(centerWorldPoint, dir);
        otterIndicator.rectTransform.localPosition = new Vector2(100, 100);
    }
}
