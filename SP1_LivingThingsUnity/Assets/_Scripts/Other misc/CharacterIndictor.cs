using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterIndictor : MonoBehaviour
{

    public GameObject otter;
    public Image otterIndicator;
    public Camera mainCamera;

    Vector2 center;


    void Start()
    {
        center = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    void Update()
    {

    }

    void UpdateIndicators()
    {
        Vector3 centerWorldPoint = mainCamera.ViewportToWorldPoint(center);

    }
}
