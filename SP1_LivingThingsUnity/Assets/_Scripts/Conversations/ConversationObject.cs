using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Conversation")]
public class ConversationObject : ScriptableObject {

    public string[] dialog;
    public Sprite[] characters;
    public AudioClip[] sounds;

}
