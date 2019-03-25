using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : MonoBehaviour
{
    public GameObject destroySound;
    public GameObject[] pieces;

    public void ExplodeWall()
    {
        foreach (var piece in pieces)
        {
            GameObject _piece = Instantiate(piece, transform.position, transform.rotation);
            _piece.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
            _piece.GetComponent<Rigidbody2D>().AddForce(335 * new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)));
        }

        GameObject _destroySound = Instantiate(destroySound, transform.position, transform.rotation) as GameObject;
        var _destroySoundSrc = _destroySound.GetComponent<AudioSource>();
        _destroySoundSrc.Play();
        Destroy(_destroySound, _destroySoundSrc.clip.length);

        Destroy(gameObject);
    }
}
