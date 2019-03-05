using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationTrigger : MonoBehaviour
{
    public Text convoText;

    public string conversationName;

    public enum characterTypes { Otter = 1, Seal = 2, Frog = 3 };
    public characterTypes characters;

    float letterTimer = 0.1f;
    bool entered = false;
    public int currentIndex = 0;

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject obj = col.gameObject;
        if (obj.CompareTag(Enum.GetName(typeof(characterTypes), characters)) && !entered)
        {
            currentIndex = 0;
            PlayConversation();
            entered = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Skips current dialog
        {
            currentIndex++;
            PlayConversation();
        }

    }

    void PlayConversation()
    {
        var conversationCreator = FindObjectOfType<ConversationCreator>();
        var conversation = conversationCreator.FindConversationByName(conversationName);
        if (currentIndex < conversation.Dialog.Count)
            StartCoroutine(PlayDialog(conversation));


        if (currentIndex == conversation.Dialog.Count)
        {
            convoText.text = "";
            Destroy(gameObject);
        }
    }

    IEnumerator PlayDialog(Conversation conversation)
    {

        convoText.text = "";
        foreach (var letter in conversation.Dialog[currentIndex].ToCharArray())
        {
            convoText.text += letter;
            yield return new WaitForSeconds(letterTimer);
        }
    }
}
