using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation
{

    public string name;
    public List<string> thingsToSay = new List<string>();

    public Conversation(string name)
    {
        this.name = name;
    }

}
