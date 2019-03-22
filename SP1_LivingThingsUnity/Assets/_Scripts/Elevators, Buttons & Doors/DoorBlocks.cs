using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBlocks : MonoBehaviour
{
    Animator anim;
    public GameObject[] blocks;
    public int blockMargin = 1;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void SwitchBlockPosition(Vector2 dir)
    {
        foreach (var block in blocks)
        {
            var _block = block.GetComponent<Block>();
            Vector2 blockPos = _block.startPosition;
            _block.SetTarget(blockPos + dir);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var obj = col.gameObject;
        if (obj.CompareTag("Seal"))
        {
            anim.SetBool("Pressed", true);
            if (gameObject.CompareTag("ButtonBlockUp"))
                SwitchBlockPosition(new Vector2(0, blockMargin));
            else
                SwitchBlockPosition(new Vector2(0, -blockMargin));
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        var obj = col.gameObject;
        if (obj.CompareTag("Seal"))
        {
            anim.SetBool("Pressed", false);
            if (gameObject.CompareTag("ButtonBlockUp"))
                SwitchBlockPosition(Vector2.zero);
            else
                SwitchBlockPosition(Vector2.zero);
        }
    }

}
