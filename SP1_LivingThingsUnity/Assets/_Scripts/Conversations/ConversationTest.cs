using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConversationTest
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

    public ConversationTest(string name)
    {
        this.name = name;
    }

}
