//DialogueSet.cs by Evan Reeves.
//Class used to store the dialogue format used in Dialogue.cs.
using System;
using System.Collections.Generic;

namespace Client.GUI
{
    public class DialogueSet
    {
        public string ParentNode;
        public int NodeCount;
        public List<string> Dialogue = new List<string>();
        public List<string> DialogueNodes = new List<string>();
    }
}
