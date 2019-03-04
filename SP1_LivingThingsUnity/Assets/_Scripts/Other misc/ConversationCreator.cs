using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationCreator : MonoBehaviour
{
    [HideInInspector] public string cText;
    [HideInInspector] public string cName;
    [HideInInspector] public int selectedConversation = 0;
    [HideInInspector] public List<Conversation> conversations = new List<Conversation>();
    [HideInInspector] public List<string> optionsList = new List<string>();
    [HideInInspector] public string[] options;

    void UpdateOptions()
    {
        options = optionsList.ToArray();
    }

    public void CreateNewConversation(string name)
    {
        conversations.Add(new Conversation(name));
        optionsList.Add(name);
        UpdateOptions();
    }

    public void AddTextToConversation(string text)
    {
        conversations[selectedConversation].thingsToSay.Add(text);
    }

    public Conversation FindConversationByName(string name)
    {

        if (conversations.Count == 0)
            Debug.LogWarning("There are no conversations");

        foreach (var conversation in conversations)
        {
            if (conversation.name == name)
                return conversation;
        }
        return null;
    }
}
