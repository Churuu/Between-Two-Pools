using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextColliderOnOff : MonoBehaviour
{// Jonas Thunberg 2019-02-09

    [SerializeField] Text text;
    List<GameObject> gameObjects = new List<GameObject>();

    private void Start()
    {
        var playerControllers = FindObjectsOfType<PlayerController>();
        foreach (PlayerController controller in playerControllers)
        {
            gameObjects.Add(controller.gameObject);
        }
        if (text != null)
        {
            text.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (text != null && collision.gameObject == gameObjects[i])
            {
                text.enabled = true;
            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (text != null && collision.gameObject == gameObjects[i])
            {
                text.enabled = false;
            }
        }
    }
}
