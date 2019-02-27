using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterIndictor : MonoBehaviour
{

    public Camera mainCamera;
    [Space]
    [Header("Otter Indicator")]
    public Transform otter, otterArrow;
    public Image otterIndicator;
    [Space]
    [Header("Offets and max distance")]
    public float offset;
    public float maxDistanceX, maxDistanceY;

    Vector2 center;
    Vector3 centerWorldPoint;

    void Start()
    {
        center = new Vector2(0.5f, 0.5f);
    }

    void Update()
    {
        UpdateIndicators(otter, otterIndicator, otterArrow);
    }

    void UpdateIndicators(Transform Object, Image indicator, Transform arrow)
    {
        centerWorldPoint = mainCamera.ViewportToWorldPoint(center);
        Vector2 dir = Object.position - centerWorldPoint;
        indicator.rectTransform.localPosition = dir * offset;
        var angleRadians = Mathf.Atan2(dir.y, dir.x);
        var angle = Mathf.Rad2Deg * angleRadians;
        arrow.transform.localRotation = Quaternion.RotateTowards(arrow.transform.localRotation, new Quaternion(0, 0, -angle, 0), 1f);


        if (indicator.rectTransform.localPosition.x > maxDistanceX)
            indicator.rectTransform.localPosition = new Vector2(maxDistanceX, indicator.rectTransform.localPosition.y);
        else if (indicator.rectTransform.localPosition.x < -maxDistanceX)
            indicator.rectTransform.localPosition = new Vector2(-maxDistanceX, indicator.rectTransform.localPosition.y);

        if (indicator.rectTransform.localPosition.y > maxDistanceY)
            indicator.rectTransform.localPosition = new Vector2(indicator.rectTransform.localPosition.x, maxDistanceY);
        else if (indicator.rectTransform.localPosition.y < -maxDistanceY)
            indicator.rectTransform.localPosition = new Vector2(indicator.rectTransform.localPosition.x, -maxDistanceY);


    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(maxDistanceX * 2, maxDistanceY * 2));
    }
}
