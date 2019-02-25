using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public GameObject[] blocks;
    public int blockMargin = 1;
    public float delta = 0.1f;

    bool extended = true;
    int count = 0;
    Vector2 newPosition = Vector2.zero;

    void Start()
    {

    }

    void Update()
    {
        SwitchBlockPosition();
    }

    void SwitchBlockPosition()
    {
        if (count < blocks.Length && newPosition != Vector2.zero)
        {
            blocks[count].transform.position = Vector2.Lerp(blocks[count].transform.position, newPosition, Time.deltaTime * delta);
        }

        if (Vector2.Distance(blocks[count].transform.position, newPosition) < 0.1f ^ newPosition == Vector2.zero)
        {
            newPosition = new Vector2(blocks[count].transform.position.x, blocks[count].transform.position.y + (extended ? 1 : -1));
            count++;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var obj = col.gameObject;
        if (obj.CompareTag("Otter"))
        {
            count = 0;
            extended = !extended;
        }
    }

}
