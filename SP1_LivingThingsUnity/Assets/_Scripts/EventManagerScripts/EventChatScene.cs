using UnityEngine;

public class EventChatScene : MonoBehaviour
{//Jonas 2019-03-11

  [SerializeField]  GameObject[] gameObjects;
    // Use this for initialization
    void Start()
    {
        EventManager.instance.OnChatActiv += NoMomentOnChat;
        EventManager.instance.OnChatEnd += ChatEndMomentOn;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void NoMomentOnChat()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObject.GetComponent<PlayerController>().SetPlayerState(false);
        }
    }
    private void ChatEndMomentOn()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObject.GetComponent<PlayerController>().SetPlayerState(true);
        }
    }
}
