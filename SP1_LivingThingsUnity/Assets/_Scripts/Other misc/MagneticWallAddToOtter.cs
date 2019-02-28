using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticWallAddToOtter : MonoBehaviour {

    bool One = true;


    // Update is called once per frame
    void Update()
    {
        if (One)
        {
            if (EventManager.instance.OnStartAddMagneticWall != null)
            {
                Debug.Log("Wall");
                EventManager.instance.OnStartAddMagneticWall(gameObject.GetComponent<Rigidbody2D>());
            }
            One = false;
        }
    }
}
