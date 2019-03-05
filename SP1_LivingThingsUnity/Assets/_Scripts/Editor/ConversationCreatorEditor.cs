using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConversationCreator))]
public class ConversationCreatorEditor : Editor
{
    int oldSelection = 0;
    string oldText = "";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ConversationCreator conversationCreator = (ConversationCreator)target;

        conversationCreator.selectedConversation = EditorGUILayout.Popup("Conversations", conversationCreator.selectedConversation, conversationCreator.options);


        GUILayout.Label("Name");
        conversationCreator.cName = EditorGUILayout.TextField(conversationCreator.cName);


        if (GUILayout.Button("Create new conversation") && !conversationCreator.cName.Contains(" "))
        {
            conversationCreator.CreateNewConversation(conversationCreator.cName);
        }
        else
        {
            Debug.LogWarning("The conversation must have a name and cannot have spaces in the name!");
        }

        if (GUILayout.Button("Delete selected conversation") && conversationCreator.selectedConversation < conversationCreator.conversations.Count && conversationCreator.selectedConversation >= 0 && conversationCreator.conversations.Count != 0)
        {
            conversationCreator.DeleteConversation(conversationCreator.selectedConversation);
        }
        GUILayout.Space(10);


        conversationCreator.selectedDialog = EditorGUILayout.Popup("Dialog", conversationCreator.selectedDialog, conversationCreator.dialogOptions);
        conversationCreator.character = EditorGUILayout.Popup("Character", conversationCreator.character, conversationCreator.CharacterOptions);

        if (conversationCreator.selectedDialog != oldSelection)
            oldSelection = conversationCreator.selectedDialog;

        if (GUILayout.Button("Add new dialog"))
        {
            conversationCreator.AddDialog();
        }
        if (GUILayout.Button("Delete dialog"))
        {
            conversationCreator.DeleteDialog();
        }

        conversationCreator.cText = EditorGUILayout.TextArea(conversationCreator.cText, GUILayout.Height(100f));

    }
}
