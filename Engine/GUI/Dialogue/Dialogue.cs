//Dialogue.cs by Evan Reeves.
//Can be easily adapted to work with another framework, just modify Draw()/LoadFont() and GetInput().
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using SFML.Graphics;
using Newtonsoft.Json;
using System.IO;

namespace Client.GUI
{
    class Dialogue
    {
        private List<DialogueSet> dialogueSet;
        private DialogueState state = DialogueState.Inactive;
        private DialogueSet currentSet;

        private string currentText = "";
        private int charaterCount = 0;
        private int multipleChoiceSelection = 0;

        //Renderer Specific Vars
        private Font dialogueFont;
        private Text dialogueText = new Text();

        enum DialogueState
        {
            Single,
            Multiple,
            Inactive
        }

        public Dialogue()
        {
            //Update automatically;
            Client.OnUpdate += Update;
            Client.OnLateDraw += Draw;
        }

        public void LoadSet(string filename)
        {
            Reset();
            LoadFont();
            dialogueSet = JsonConvert.DeserializeObject<List<DialogueSet>>(File.ReadAllText(@"Content\Dialogue\" + filename));
            DialogueStart();
        }

        private void LoadFont()
        {
            //Renderer Specific
            while (dialogueFont == null)
            {
                dialogueFont = new Font(@"Content\Fonts\SourceSansPro.otf");
            }
            dialogueText.Font = dialogueFont;
            dialogueText.CharacterSize = 18;
            dialogueText.Color = Color.White;
            dialogueText.Style = Text.Styles.Bold;
            dialogueText.Position = new Vector2f(30, 380);
        }

        private void DialogueStart()
        {
            foreach (DialogueSet ng in dialogueSet)
            {
                if ((string)ng.ParentNode == "Node0")
                {
                    //We have found the first DialogueSet. Sort.
                    SortSet(ng);
                }
            }
        }

        public void Reset()
        {
            multipleChoiceSelection = 0;
            currentText = "";
            charaterCount = 0;
        }

        private void SortSet(DialogueSet dS)
        {
            currentSet = dS;

            if (dS.NodeCount == 0)
            {
                //No dialogue, abort.
                state = DialogueState.Inactive;
                Console.WriteLine("ERROR: Current dialogue set has no nodes.");
            }
            else if (dS.NodeCount == 1)
            {
                //Single line of dialogue.
                SetSingle(dS);
                state = DialogueState.Single;
            }
            else
            {
                //Multiple choice dialogue.
                SetMultiple(dS);
                state = DialogueState.Multiple;
            }
            charaterCount = 0; //reset character scroll count;
            multipleChoiceSelection = 0; // reset choice selection.
        }

        private void SetSingle(DialogueSet dS)
        {
            currentText = dS.Dialogue[0];
        }

        private void SetMultiple(DialogueSet dS)
        {
            string text = "";
            foreach (string str in dS.Dialogue){
                text += str + Environment.NewLine;
            }
            currentText = text;
        }

        private void GetInput()
        {
            if (Input.OnKeyDown(Keyboard.Key.Space))
                Iterate();

            if (Input.OnKeyDown(Keyboard.Key.Up) && multipleChoiceSelection > 0)
                multipleChoiceSelection--;
            else if (Input.OnKeyDown(Keyboard.Key.Down) && multipleChoiceSelection < currentSet.NodeCount - 1)
                multipleChoiceSelection++;
        }

        public void Update()
        {
            if (charaterCount < currentText.Length)
                charaterCount++;

            GetInput();
        }

        public void Draw()
        {
            dialogueText.DisplayedString = currentText.Substring(0, charaterCount);  

            switch (state)
            {  
                case DialogueState.Single:
                    Client.Window.Draw(dialogueText);
                    break;

                case DialogueState.Multiple:
                    Client.Window.Draw(dialogueText);
                    break;

                case DialogueState.Inactive:
                    //Do not draw.
                    break;
            }
        }

        public void Iterate()
        {
            switch (state)
            {
                case DialogueState.Single:

                    foreach (DialogueSet dS in dialogueSet)
                    {
                        if (dS.ParentNode == currentSet.DialogueNodes[0])
                        {
                            SortSet(dS);
                            return;
                        }
                    }
                    break;

                case DialogueState.Multiple:

                    foreach (DialogueSet dS in dialogueSet)
                    {
                        if (dS.ParentNode == currentSet.DialogueNodes[multipleChoiceSelection])
                        {
                            //Set what the selection does here if anything.
                            SortSet(dS);
                            return;
                        }
                    }
                    break;

                case DialogueState.Inactive:
                    break;
            }
            //Must be the end of dialogue. Set Inactive.
            state = DialogueState.Inactive;
        }
    }
}
