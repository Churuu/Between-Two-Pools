using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Conversation
{

    public string name;
    public List<string> dialog = new List<string> {
        "Default"
    };

    public List<Sprite> characterDialog = new List<Sprite>();

    public string[] dialogOptions
    {
        get
        {

            return dialog.ToArray();
        }
    }

    public Conversation(string name)
    {
        this.name = name;
    }
}
