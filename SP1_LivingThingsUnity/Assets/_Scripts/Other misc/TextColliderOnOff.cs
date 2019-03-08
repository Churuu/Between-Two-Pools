using UnityEngine;
using UnityEngine.UI;

public class TextColliderOnOff : MonoBehaviour
{// Jonas Thunberg 2019-02-09

    [SerializeField] Text text;


    private void Start()
    {
        if (text != null)
        {
            text.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (text != null && collision.transform.childCount != 0)
        {
            if (collision.transform.GetChild(0).GetComponent<ActivePlayerStateMachine>() != null)
            {
                text.enabled = true;
            }
        }

		if (text != null && collision.transform.childCount != 0)
		{
			if (collision.transform.GetChild(1).GetComponent<ActivePlayerStateMachine>() != null)
			{
				text.enabled = true;
			}
		}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (text != null && collision.transform.childCount != 0)
        {
            if (collision.transform.GetChild(0).GetComponent<ActivePlayerStateMachine>() != null)
            {
                text.enabled = false;
            }

        }

		if (text != null && collision.transform.childCount != 0)
		{
			if (collision.transform.GetChild(1).GetComponent<ActivePlayerStateMachine>() != null)
			{
				text.enabled = false;
			}

		}
    }
}
