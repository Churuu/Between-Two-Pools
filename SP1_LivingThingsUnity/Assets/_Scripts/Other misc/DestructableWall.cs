using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : MonoBehaviour
{

    public GameObject[] pieces;

    public void ExplodeWall()
    {
        foreach (var piece in pieces)
        {
            GameObject _piece = Instantiate(piece, transform.position, transform.rotation);
            _piece.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
            _piece.GetComponent<Rigidbody2D>().AddForce(335 * new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)));
        }
        Destroy(gameObject);
    }
}
