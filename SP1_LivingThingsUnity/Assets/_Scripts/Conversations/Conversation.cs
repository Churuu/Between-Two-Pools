using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation
{

    public string name;
    public List<string> Dialog = new List<string> {
        "Default"
    };

    public string[] options
    {
        get {

            return Dialog.ToArray();
        }
    }

    public Conversation(string name)
    {
        this.name = name;
    }

}
