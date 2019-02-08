using UnityEngine;
using UnityEngine.UI;

public class TextColliderOnOff : MonoBehaviour
{

    [SerializeField] Text text;
    [SerializeField] string tagPlayer = "Player";

    private void Start()
    {
        if (text != null)
        {
            text.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (text != null && collision.tag == tagPlayer )
        {
            text.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (text != null && collision.tag == tagPlayer)
        {
            text.enabled = false;
        }
    }
}
