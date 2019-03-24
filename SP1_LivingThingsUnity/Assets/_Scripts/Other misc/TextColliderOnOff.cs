using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextColliderOnOff : MonoBehaviour
{// Jonas Thunberg 2019-02-09

    [SerializeField] GameObject imageText;
    List<GameObject> gameObjects = new List<GameObject>();

    private void Start()
    {
        var playerControllers = FindObjectsOfType<PlayerController>();
        foreach (PlayerController controller in playerControllers)
        {
            gameObjects.Add(controller.gameObject);
        }
        if (imageText != null)
        {
            imageText.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (imageText != null && collision.gameObject == gameObjects[i])
            {
                imageText.SetActive(true);
            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (imageText != null && collision.gameObject == gameObjects[i])
            {
                imageText.SetActive(false);
            }
        }
    }
}
