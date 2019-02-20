using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collider)
    {
        var col = collider.gameObject;

        Destroy(gameObject);

        if (col.CompareTag("DestructableWall"))
            Destroy(col);
    }
}
