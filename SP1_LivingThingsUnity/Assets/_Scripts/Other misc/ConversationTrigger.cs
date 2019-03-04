using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationTrigger : MonoBehaviour
{
    [HideInInspector] public Text convoText;

    public string conversationName;

    public enum characterTypes { Otter = 1, Seal = 2, Frog = 3 };
    public characterTypes characters;

    float letterTimer = 0.2f;

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject obj = col.gameObject;
        if (obj.CompareTag(Enum.GetName(typeof(characterTypes), characters)))
        {
            var conversationCreator = FindObjectOfType<ConversationCreator>();
            var conversation = conversationCreator.FindConversationByName(conversationName);

            for (int i = 0; i < conversation.thingsToSay.Count; i++)
            {
                StartCoroutine(PlayConversation(conversation, i));

            }
        }
    }



    IEnumerator PlayConversation(Conversation conversation, int index)
    {
        foreach (var letter in conversation.thingsToSay[index].ToCharArray())
        {
            convoText.text += letter;
            yield return new WaitForSeconds(letterTimer);
        }
    }
}
