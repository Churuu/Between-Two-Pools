using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Joakim
public class ScrollingCredits : MonoBehaviour {

    [SerializeField] float speed = 15f;

        void Update()
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
}
