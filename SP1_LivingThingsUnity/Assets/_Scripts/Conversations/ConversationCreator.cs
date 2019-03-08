using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationCreator : MonoBehaviour
{


    [HideInInspector] public string cName;
    [HideInInspector] public int selectedConversation = 0;
    [HideInInspector] public int selectedDialog = 0;


    [HideInInspector]
    public List<Conversation> conversations = new List<Conversation> {
        new Conversation("Default")
    };

    [HideInInspector]
    public List<string> optionsList = new List<string> {
        "Default"
    };

    [HideInInspector]
    public string[] CharacterOptions =
    {
        "Otter",
        "Seal",
        "Frog",
        "Plankton",
        "Shrimp"
    };

    public int character
    {
        get
        {
            return conversations[selectedConversation].characterDialog[selectedDialog];
        }
        set
        {
            conversations[selectedConversation].characterDialog[selectedDialog] = value;
        }
    }


    public string cText
    {
        get
        {
            return conversations[selectedConversation].dialog[selectedDialog];
        }
        set
        {
            conversations[selectedConversation].dialog[selectedDialog] = value;
        }
    }

    public string[] options
    {
        get
        {
            return optionsList.ToArray();
        }
    }

    public string[] dialogOptions
    {
        get
        {
            return conversations[selectedConversation].dialog.ToArray();
        }
    }

    public void CreateNewConversation(string name)
    {
        conversations.Add(new Conversation(name));
        optionsList.Add(name);
    }

    public void AddDialog()
    {

        conversations[selectedConversation].dialog.Add(conversations[selectedConversation].dialog.Count.ToString());
        conversations[selectedConversation].characterDialog.Add(0);
    }

    public void DeleteDialog()
    {
        conversations[selectedConversation].dialog.RemoveAt(selectedDialog);
        conversations[selectedConversation].characterDialog.RemoveAt(selectedDialog);
        selectedDialog = 0;
    }

    public void DeleteConversation(int index)
    {
        try
        {
            conversations.RemoveAt(index);
            optionsList.RemoveAt(index);
            selectedConversation = 0;
        }
        catch (System.Exception)
        {
            Debug.LogError("Något gick fel... Vad ni än gör fråga inte Robin för han vet inte... :)");
            throw;
        }

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
