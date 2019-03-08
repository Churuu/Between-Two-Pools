using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterIndictor : MonoBehaviour
{

    public Camera mainCamera;
    [Header("Otter Indicator")]
    public Image otterIndicator;
    public Transform otter, otterArrow;
    [Header("Seal Indicator")]
    public Image SealIndicator;
    public Transform seal, sealArrow;
    [Header("Frog Indicator")]
    public Image FrogIndicator;
    public Transform frog, frogArrow;
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
        UpdateIndicators(frog, FrogIndicator, frogArrow);
        UpdateIndicators(seal, SealIndicator, sealArrow);
    }

    void UpdateIndicators(Transform _object, Image indicator, Transform arrow)
    {
        centerWorldPoint = mainCamera.ViewportToWorldPoint(center);
        Vector2 dir = _object.position - centerWorldPoint;
        indicator.rectTransform.localPosition = dir * offset;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrow.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle - 180));


        if (indicator.rectTransform.localPosition.x > maxDistanceX)
            indicator.rectTransform.localPosition = new Vector2(maxDistanceX, indicator.rectTransform.localPosition.y);
        else if (indicator.rectTransform.localPosition.x < -maxDistanceX)
            indicator.rectTransform.localPosition = new Vector2(-maxDistanceX, indicator.rectTransform.localPosition.y);

        if (indicator.rectTransform.localPosition.y > maxDistanceY)
            indicator.rectTransform.localPosition = new Vector2(indicator.rectTransform.localPosition.x, maxDistanceY);
        else if (indicator.rectTransform.localPosition.y < -maxDistanceY)
            indicator.rectTransform.localPosition = new Vector2(indicator.rectTransform.localPosition.x, -maxDistanceY);

        var renderer = _object.GetComponent<Renderer>();
        arrow.gameObject.SetActive(!renderer.isVisible);
        indicator.gameObject.SetActive(!renderer.isVisible);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(transform.position, new Vector2(maxDistanceX * 2, maxDistanceY * 2));
    }
}
