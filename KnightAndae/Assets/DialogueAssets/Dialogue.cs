using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name; //Holds the name of the NPC

    [TextArea(3, 10)] //Available size
    public string[] sentences; //Hold sentences

}
