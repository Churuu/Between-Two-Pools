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

    public List<int> characterDialog = new List<int>
    {
        0
    };

    public string[] options
    {
        get
        {

            return dialog.ToArray();
        }
    }

    public int[] characters
    {
        get
        {
            return characterDialog.ToArray();
        }
    }

    public Conversation(string name)
    {
        this.name = name;
    }

}
