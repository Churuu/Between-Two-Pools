using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConversationCreator))]
public class ConversationCreatorEditor : Editor
{
    SerializedProperty textBox;
    public GameObject conversationPrefab;
    public GameObject conversationTrigger;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ConversationCreator convo = (ConversationCreator)target;

        convo.selectedConversation = EditorGUILayout.Popup("Selected conversation", convo.selectedConversation, convo.options);
        GUILayout.Label("Conversation");
        convo.cName = EditorGUILayout.TextField(convo.cName);

        if (GUILayout.Button("Create new conversation"))
        {
            convo.CreateNewConversation(convo.cName);
        }
        GUILayout.Space(10);

        if (GUILayout.Button("Add Text to selected conversation"))
        {
            convo.CreateNewConversation(convo.cName);
        }
        convo.cText = EditorGUILayout.TextArea(convo.cText, GUILayout.Height(100f));
    }
}
