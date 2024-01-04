using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Words_With_Friends_Project
{
    public partial class Form1 : Form
    {
        static Button[] UserLetters = new Button[7]; // Allows me to make the userletter as an array so it can be looped over.
        static Label[] PlrPts = new Label[4]; // Four player mode.
        static int[] plrpoints = new int[4] {0,0,0,0};
        static int gameround = 0;
        static int plrturn = 1;
        static int plrturnmax = 0;
        static bool gameStarted = false;
        static string errmsg = "...";
        static int letterSkips = 0;
        static Random RPick = new Random();
        static char[] PlayerChrs = new char[7] { '!', '!', '!', '!', '!', '!', '!' };
        static bool gameStick = false;
        static string[] eventmessage = new string[2];


        // Multipliers
        static int[] multi_placement = new int[3] { 0, 0, 0 }; // Parameters: Word choice, How many things are multiplied and multiplier
        static string LMultiplier = "0";



        public Form1()
        {
            InitializeComponent();
        }

        private void TundeError()
        {
            string errmessage = "AYOTUND";

            MessageBox.Show("Did you hack? Something is not right here...");
            gameStarted = false;
            UserLetter1.Text = char.ToString(errmessage[0]);
            UserLetter2.Text = char.ToString(errmessage[1]);
            UserLetter3.Text = char.ToString(errmessage[2]);
            UserLetter4.Text = char.ToString(errmessage[3]);
            UserLetter5.Text = char.ToString(errmessage[4]);
            UserLetter6.Text = char.ToString(errmessage[5]);
            UserLetter7.Text = char.ToString(errmessage[6]);

        }

        //LOAD WORD LOAD WORD LOAD WORD LOAD WORD LOAD WORD LOAD WORD LOAD WORD LOAD WORD LOAD WORD LOAD WORD LOAD WORD

        async private void WordLoad()
        {
            bool UnderScoreStorm = false;
            if (gameround % 4 == 0 && gameround != 0)
            {
                UnderScoreStorm = true;
                MasterButton.Enabled = false;
                ClearButton.Enabled = false;
            }
            for (int i = 0; i < PlayerChrs.Length; i++)
            {
                PlayerChrs[i] = '!';
            }
            for (int i = 0; i < UserLetters.Length; i++)
            {
                UserLetters[i].Enabled = true;
                UserLetters[i].ForeColor = Color.White;
            }
            ResultLabel.Text = "-";
            LMultiplier = "0";
            multi_placement[0] = 0;
            multi_placement[1] = 0;
            multi_placement[2] = 0;
            MultiplierMain.Visible = false;
            MultiplierBack.Visible = false;
            MultiplierTopBAR.Visible = false;
            MultiplierBottomBAR.Visible = false;

            char[] vowelRNG = new char[] { 'A', 'A', 'E', 'E', 'E', 'I', 'O', 'U' };
            char[] consonantMainsRNG = new char[] { 'B', 'B', 'C', 'D', 'F', 'G', 'H', 'K', 'L', 'M', 'M', 'N', 'P', 'R', 'S', 'T', 'W' };
            char[] consonantsRareRNG = new char[] { 'J', 'J', 'J', 'J', 'Q', 'Q', 'V', 'V', 'X', 'X', 'Y', 'Y', 'Z', 'Z', '_'};
            int Placement = 0;
            int successfulplace = RPick.Next(2, 3); // Always at least two vowels (2 or 3 vowels)
            if (UnderScoreStorm == false)
            {
                while (successfulplace > 0)
                {
                    Placement = RPick.Next(0, PlayerChrs.Length);
                    if (PlayerChrs[Placement] == '!')
                    {
                        PlayerChrs[Placement] = vowelRNG[RPick.Next(0, vowelRNG.Length)];
                        successfulplace = successfulplace - 1;
                    }

                }
                successfulplace = RPick.Next(0, 2); // 1/3 chance of an extra vowel.
                if (successfulplace == 1)
                {
                    while (successfulplace > 0)
                    {
                        Placement = RPick.Next(0, PlayerChrs.Length);
                        if (PlayerChrs[Placement] == '!')
                        {
                            PlayerChrs[Placement] = vowelRNG[RPick.Next(0, vowelRNG.Length)];
                            successfulplace = successfulplace - 1;
                        }

                    }
                }

                successfulplace = RPick.Next(0, 1); // Rare letters will appear in  0-1 boxes
                while (successfulplace > 0)
                {
                    Placement = RPick.Next(0, PlayerChrs.Length);
                    if (PlayerChrs[Placement] == '!')
                    {
                        PlayerChrs[Placement] = consonantsRareRNG[RPick.Next(0, consonantsRareRNG.Length)];
                        successfulplace = successfulplace - 1;
                    }

                }

                successfulplace = RPick.Next(0, 2); // 1/3 chance of an extra rare consonant.
                if (successfulplace == 1)
                {
                    while (successfulplace > 0)
                    {
                        Placement = RPick.Next(0, PlayerChrs.Length);
                        if (PlayerChrs[Placement] == '!')
                        {
                            PlayerChrs[Placement] = consonantsRareRNG[RPick.Next(0, consonantsRareRNG.Length)];
                            successfulplace = successfulplace - 1;
                        }

                    }
                }



                for (int i = 0; i < PlayerChrs.Length; i++) // Fills remaining userletter boxs with common consonants array.
                {
                    if (PlayerChrs[i] == '!')
                    {
                        PlayerChrs[i] = consonantMainsRNG[RPick.Next(0, consonantMainsRNG.Length)];
                    }
                }

                for (int i = 0; i < UserLetters.Length; i++)
                {
                    UserLetters[i].Text = char.ToString(PlayerChrs[i]);
                }
            }
            else
            {
                gameStick = true;
                await Task.Delay(1500);
                string UsSTxt = "UNDERSCORESTORM";
                for (int i = 0; i <= 4; i++)
                {
                    UserLetters[i + 1].Text = UsSTxt[i].ToString();
                }
                await Task.Delay(600);
                for (int i = 0; i < UserLetters.Length; i++)
                {
                    UserLetters[i].Text = "";
                }
                await Task.Delay(900);
                for (int i = 5; i <= 9; i++)
                {
                    UserLetters[i - 4].Text = UsSTxt[i].ToString();
                }
                await Task.Delay(600);
                for (int i = 0; i < UserLetters.Length; i++)
                {
                    UserLetters[i].Text = "";
                }
                await Task.Delay(900);
                for (int i = 10; i <= 14; i++)
                {
                    UserLetters[i - 9].Text = UsSTxt[i].ToString();
                }
                await Task.Delay(600);
                for (int i = 0; i < UserLetters.Length; i++)
                {
                    UserLetters[i].Text = "";
                }
                await Task.Delay(2000);
                Placement = RPick.Next(1,9);
                for (int i = 0; i < UserLetters.Length; i++)
                {
                    UserLetters[i].Text = UsSTxt[i].ToString();
                }

                for (int j = 1; j < Placement; j++)
                { 
                    for (int i = 0; i < UserLetters.Length; i++)
                    {
                        
                        
                        UserLetters[i].Text = UsSTxt[i].ToString();
                    }
                    UsSTxt = UsSTxt.Substring(1, UsSTxt.Length - 1);
                    await Task.Delay(1500);
                }

                gameStick = false;
                successfulplace = RPick.Next(5,7);
                while (successfulplace > 0)
                {
                    Placement = RPick.Next(0, PlayerChrs.Length);
                    if (PlayerChrs[Placement] != '_')
                    {
                        PlayerChrs[Placement] = '_';
                        successfulplace = successfulplace - 1;
                        MultiplierMain.Visible = true;
                        MultiplierMain.Location = new Point(197 + (144 * (Placement)), 65);
                        MultiplierMain.ForeColor = Color.FromArgb(139, 69, 19);
                        await Task.Delay(500);
                        MultiplierMain.Visible = false;
                        UserLetters[Placement].Text = "_";
                    }

                }

                for (int i = 0; i < UserLetters.Length; i++)
                {
                    PlayerChrs[i] = UserLetters[i].Text[0];
                }

            }


            // Chance of multiplied letter value and word. (Do this after word identification works)

            for (int i = 1; i <= PlayerChrs.Length; i++)
            {
                if (PlayerChrs[i-1] != '_')
                {
                    multi_placement[0] = RPick.Next(0, 14); 
                }        
                else
                {
                    multi_placement[0] = RPick.Next(0, 2);        
                }

                if (multi_placement[0] == 1)
                {
                    multi_placement[1] = multi_placement[1] + 1;
                    if (multi_placement[1] == 1)
                    {
                        LMultiplier = i.ToString();
                    }      
                }
            }
            int[] multiplier_chance = new int[10];
            if (multi_placement[1] >= 3)
            {
                LMultiplier = "A";
                multiplier_chance[0] = 2;
                multiplier_chance[1] = 2;
                multiplier_chance[2] = 3;
                multiplier_chance[3] = 3;
                multiplier_chance[4] = 2;
                multiplier_chance[5] = 2;
                multiplier_chance[6] = 2;
                multiplier_chance[7] = 2;
                multiplier_chance[8] = 3;
                multiplier_chance[9] = 2;
            }
            else
            {
                if (LMultiplier != "0")
                {
                    if (PlayerChrs[int.Parse(LMultiplier)-1] == '_')
                    {
                        multiplier_chance[0] = 5;
                        multiplier_chance[1] = 5;
                        multiplier_chance[2] = 2;
                        multiplier_chance[3] = 3;
                        multiplier_chance[4] = 5;
                        multiplier_chance[5] = 5;
                        multiplier_chance[6] = 2;
                        multiplier_chance[7] = 5;
                        multiplier_chance[8] = 5;
                        multiplier_chance[9] = 5;
                    }
                    else
                    {
                        multiplier_chance[0] = 2;
                        multiplier_chance[1] = 2;
                        multiplier_chance[2] = 3;
                        multiplier_chance[3] = 3;
                        multiplier_chance[4] = 2;
                        multiplier_chance[5] = 3;
                        multiplier_chance[6] = 2;
                        multiplier_chance[7] = 3;
                        multiplier_chance[8] = 3;
                        multiplier_chance[9] = 2;
                    }
                }
            }
            multi_placement[2] = multiplier_chance[RPick.Next(0, multiplier_chance.Length-1)];
            if (multi_placement[2] == 2)
            {
                MultiplierMain.ForeColor = Color.FromArgb(65, 105, 225);
                MultiplierBack.ForeColor = Color.FromArgb(65, 105, 225);
                MultiplierBottomBAR.BackColor = Color.FromArgb(65, 105, 225);
                eventmessage[1] = "Wowie, you have recieved a x2 multiplier on";

            }
            else if (multi_placement[2] == 3)
            {
                MultiplierMain.ForeColor = Color.FromArgb(255, 165, 0);
                MultiplierBack.ForeColor = Color.FromArgb(255, 165, 0);
                MultiplierBottomBAR.BackColor = Color.FromArgb(255, 165, 0);
                eventmessage[1] = "Wowie, you have recieved a x3 multiplier on";
            }
            else if (multi_placement[2] == 5)
            {
                MultiplierMain.ForeColor = Color.FromArgb(255, 0, 0);
                MultiplierBack.ForeColor = Color.FromArgb(255, 0, 0);
                MultiplierBottomBAR.BackColor = Color.FromArgb(255, 0, 0);
                eventmessage[1] = "Wowie, you have recieved a x5 multiplier on";
            }


            if (LMultiplier != "0")
            {
                if (LMultiplier != "A")
                {
                    MultiplierMain.Location = new Point(197 + (144 * (int.Parse(LMultiplier) - 1)), 65);
                    MultiplierMain.Visible = true;
                    eventmessage[1] += " a letter!";
                    EventDisplayer(eventmessage, 850);
                    if (PlayerChrs[int.Parse(LMultiplier) - 1] == '_')
                    {
                    }
                    else
                    {
                        //MultiplierMain.Location = new Point(197 + (144 * (int.Parse(LMultiplier) - 1)), 65);
                    }
                }
                else
                {
                    eventmessage[1] += " the WHOLE WORD!";
                    EventDisplayer(eventmessage, 850);
                    MultiplierMain.Location = new Point(197, 65);
                    MultiplierMain.Visible = true;
                    MultiplierBack.Visible = true;
                    MultiplierTopBAR.Visible = true;
                    MultiplierBottomBAR.Visible = true;
                }
            }





        }

        // PLAYER TURN SYSTEM PLAYER TURN SYSTEM PLAYER TURN SYSTEM PLAYER TURN SYSTEM PLAYER TURN SYSTEM PLAYER TURN SYSTEM PLAYER TURN SYSTEM

        private void PlayerSystem(string requesttype,int playerselection)
        {
            if (requesttype == "PlayerSelected") // Player Turn Pick
            {
                ChangeButton.Enabled = true;
                ResultLabel.Text = "-";
                letterSkips = 0;

                for (int i = 0; i < plrturnmax ; i++)
                {
                    if (i == playerselection - 1)
                    {
                        PlrPts[i].ForeColor = Color.RoyalBlue;
                    }
                    else
                    {
                        PlrPts[i].ForeColor = Color.DarkRed;
                    }
                }

            }
            else if (requesttype == "LetterReset") // When the player Chooses to click the new letters button.
            {
           
                if (letterSkips < 3)
                {
                    letterSkips = letterSkips + 1;
                    if (letterSkips == 2)
                    {
                        plrpoints[playerselection-1] -= 2;
                        PlayerSystem("PointsUpdate", 0);
                    }
                    else if (letterSkips == 3)
                    {
                        plrpoints[playerselection - 1] += RPick.Next(-4,-3);
                        PlayerSystem("PointsUpdate", 0);
                    }
                    eventmessage[0] = "Skips used = " + letterSkips + "/3";
                    EventDisplayer(eventmessage, 800);
                    WordLoad();
                }

                if (letterSkips >= 3)
                {
                    ChangeButton.Enabled = false;
                }
          


            }
            else if (requesttype == "PointsUpdate") // Update player points so that they show up in player points bar.
            {
                for (int i = 0;i < plrturnmax; i++)
                {
                    PlrPts[i].Text = "Player " + (i + 1) + ": " + plrpoints[i];
                }
            }
        }

        // EVENT DISPLAYER EVENT DISPLAYER EVENT DISPLAYER EVENT DISPLAYER EVENT DISPLAYER EVENT DISPLAYER EVENT DISPLAYER EVENT DISPLAYER
        async private void EventDisplayer(string[] EventTxt,int timefor)
        {
            if (timefor > 0)
            {
                gameStick = true;
                EventLabel.Text = EventTxt[0];
                await Task.Delay(timefor);
                EventLabel.Text = EventTxt[1];
                gameStick = false;
            }
            else
            {
                EventLabel.Text = EventTxt[0];
            }

        }

        async private void Form1_Load(object sender, EventArgs e)
        {
            // Assign the seven user letter buttons to an array.
            UserLetters[0] = UserLetter1;
            UserLetters[1] = UserLetter2;
            UserLetters[2] = UserLetter3;
            UserLetters[3] = UserLetter4;
            UserLetters[4] = UserLetter5;
            UserLetters[5] = UserLetter6;
            UserLetters[6] = UserLetter7;
            // Assign the player points display bars into the array.
            PlrPts[0] = P1Pts;
            PlrPts[1] = P2Pts;
            PlrPts[2] = P3Pts;
            PlrPts[3] = P4Pts;
            for (int i = 0; i < UserLetters.Length; i++)
            {
                UserLetters[i].Text = "";
                UserLetters[i].BackColor = Color.FromArgb(162, 163, 165);
            }
            P1Pts.Text = " ";
            P1Pts.BackColor = Color.FromArgb(162, 163, 165);
            P2Pts.Text = "Two Players";
            P3Pts.Text = "Three Players";
            P4Pts.Text = "Four Players";
            SpecialEventTitle.Visible = false;
            EventLabel.Text = "Select how many players you want in the top right corner.";
            while (gameStarted == false) // Wait until the amount of players is selected
            {
                await Task.Delay(500);
            }
            //Startup
            gameStarted = false;
            //Decoration and Flashy
            UserLetter1.BackColor = Color.FromArgb(255, 128, 0);
            UserLetter1.Text = "A";
            await Task.Delay(500);
            UserLetter2.BackColor = Color.FromArgb(255, 128, 0);
            UserLetter2.Text = "Y";
            await Task.Delay(500);
            UserLetter3.BackColor = Color.FromArgb(255, 128, 0);
            UserLetter3.Text = "O";
            await Task.Delay(500);
            UserLetter4.BackColor = Color.FromArgb(255, 128, 0);
            UserLetter4.Text = "M";
            await Task.Delay(500);
            UserLetter5.BackColor = Color.FromArgb(255, 128, 0);
            UserLetter5.Text = "I";
            await Task.Delay(500);
            UserLetter6.BackColor = Color.FromArgb(255, 128, 0);
            UserLetter6.Text = "D";
            await Task.Delay(500);
            UserLetter7.BackColor = Color.FromArgb(255, 128, 0);
            UserLetter7.Text = "E";
            await Task.Delay(500);
            ChangeButton.Text = "New Letters";
            await Task.Delay(500);
            ClearButton.Text = "Clear";
            await Task.Delay(500);
            MasterButton.Text = "Enter";
            await Task.Delay(600);
            for (int i = 0; i < UserLetters.Length; i++)
            {
                UserLetters[i].Text = "";
            }
            await Task.Delay(600);
            UserLetter1.Text = "A";
            UserLetter2.Text = "Y";
            UserLetter3.Text = "O";
            UserLetter4.Text = "M";
            UserLetter5.Text = "I";
            UserLetter6.Text = "D";
            UserLetter7.Text = "E";
            await Task.Delay(600);
            for (int i = 0; i < UserLetters.Length; i++)
            {
                UserLetters[i].Text = "";
            }
            await Task.Delay(600);
            UserLetter1.Text = "A";
            UserLetter2.Text = "Y";
            UserLetter3.Text = "O";
            UserLetter4.Text = "M";
            UserLetter5.Text = "I";
            UserLetter6.Text = "D";
            UserLetter7.Text = "E";
            await Task.Delay(600);
            for (int i = 0; i < UserLetters.Length; i++)
            {
                UserLetters[i].Text = "";
            }
            await Task.Delay(600);
            if (plrturnmax < 1 || plrturnmax > 4)
            {
                TundeError();

            }
            //End Of startup
            gameStarted = true;
            while (gameStarted == true) // Game is now running
            {

                

                PlayerSystem("PlayerSelected", plrturn);

                WordLoad();

                await Task.Delay(10000);
                break;


            }



        }

        private void P2Pts_Click(object sender, EventArgs e) // When you choose 2 Players at the begining.
        {
            if (gameStarted == false)
            {
                SpecialEventTitle.Visible = true;
                P1Pts.BackColor = Color.White;
                P1Pts.Text = "Player 1: 0";
                P2Pts.Text = "Player 2: 0";
                P3Pts.BackColor = Color.FromArgb(162, 163, 165);
                P3Pts.Text = " ";
                P4Pts.BackColor = Color.FromArgb(162, 163, 165);
                P4Pts.Text = " ";
                plrturnmax = 2;
                gameStarted = true;
            }

        }

        private void P3Pts_Click(object sender, EventArgs e) // When you choose 3 Players at the begining.
        {
            if (gameStarted == false)
            {
                SpecialEventTitle.Visible = true;
                P1Pts.BackColor = Color.White;
                P1Pts.Text = "Player 1: 0";
                P2Pts.Text = "Player 2: 0";
                P3Pts.Text = "Player 3: 0";
                P4Pts.BackColor = Color.FromArgb(162, 163, 165);
                P4Pts.Text = " ";
                plrturnmax = 3;
                gameStarted = true;
            }

        }

        private void P4Pts_Click(object sender, EventArgs e) // When you choose 4 Players at the begining.
        {
            if (gameStarted == false)
            {
                SpecialEventTitle.Visible = true;
                P1Pts.BackColor = Color.White;
                P1Pts.Text = "Player 1: 0";
                P2Pts.Text = "Player 2: 0";
                P3Pts.Text = "Player 3: 0";
                P4Pts.Text = "Player 4: 0";
                plrturnmax = 4;
                gameStarted = true;
            }

        }

        private void UselessButton_Click(object sender, EventArgs e)
        {
            // Make a table for useless button random message
            MessageBox.Show("Are you stupid child?\nwhy did you click here");
            // Double colour 65, 105, 225
            // Triple colour 255,165,0
            //⎵ 206, 226 / position

            // button colour: 255, 128, 0
            // plr colour: DarkRed

            // Multiplier StarterLocation X = 197

            // Spaces = 144X

            // Things to do: Player Turn (this means things like penalties), Word Identification
            // Zero Color 139, 69, 19
        }

        // USER LETTER STATE USER LETTER STATE USER LETTER STATE USER LETTER STATE USER LETTER STATE USER LETTER STATE USER LETTER STATE USER LETTER STATE

        private void UserLetterState(bool LState,char SelectChar,char letter) //Controls when letters are added or removed to the main box.
        {
            if (ResultLabel.Text == "-")
            {
                ResultLabel.Text = "";
            }

            if (SelectChar != 'A')
            {
                if (LState == false)
                {
                    if (UserLetters[SelectChar - '1'].Text == "_")
                    {
                        MasterButton.Enabled = true;
                    }

                    ResultLabel.Text += UserLetters[SelectChar - '1'].Text;
                    UserLetters[SelectChar - '1'].Text = "-";
                    UserLetters[SelectChar - '1'].Enabled = false;
                    
                }
            }
            else if (SelectChar == 'A')
            {
                if (LState == false)
                {
                    for (int i = 0; i < UserLetters.Length; i++)
                    {
                        UserLetters[i].Enabled = false;
                        UserLetters[i].Text = PlayerChrs[i].ToString();
                    }
                }
                else
                {
                    for (int i = 0; i < UserLetters.Length; i++)
                    {
                        UserLetters[i].Text = char.ToString(PlayerChrs[i]);
                        UserLetters[i].Enabled = true;
                    }
                    ResultLabel.Text = "-";
                }
            }
        }

        private void UserLetter1_Click(object sender, EventArgs e)
        {
            if (gameStarted == true && gameStick == false && UserLetter1.Enabled == true)
            {
                UserLetterState(false, '1', '-');
            }
        }

        private void UserLetter2_Click(object sender, EventArgs e)
        {
            if (gameStarted == true && gameStick == false && UserLetter2.Enabled == true)
            {
                UserLetterState(false, '2', '-');
            }
        }

        private void UserLetter3_Click(object sender, EventArgs e)
        {
            if (gameStarted == true && gameStick == false && UserLetter3.Enabled == true)
            {
                UserLetterState(false, '3', '-');
            }
        }

        private void UserLetter4_Click(object sender, EventArgs e)
        {
            if (gameStarted == true && gameStick == false && UserLetter4.Enabled == true)
            {
                UserLetterState(false, '4', '-');
            }
        }

        private void UserLetter5_Click(object sender, EventArgs e)
        {
            if (gameStarted == true && gameStick == false && UserLetter5.Enabled == true)
            {
                UserLetterState(false, '5', '-');
            }
        }

        private void UserLetter6_Click(object sender, EventArgs e)
        {
            if (gameStarted == true && gameStick == false && UserLetter6.Enabled == true)
            {
                UserLetterState(false, '6', '-');
            }
        }

        private void UserLetter7_Click(object sender, EventArgs e)
        {
            if (gameStarted == true && gameStick == false && UserLetter7.Enabled == true)
            {
                UserLetterState(false, '7', '-');
            }
        }

        private void ChangeButton_Click(object sender, EventArgs e) // Change the letters you get (perhaps if you don't like them)
        {
            if (gameStarted == true)
            {
                PlayerSystem("LetterReset", plrturn);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e) // Reset letters
        {
            if (gameStarted == true && gameStick == false)
            {
                UserLetterState(true, 'A', '-');  
            }
            // Re-enable all user letters and clear out word.
        }

        async private void MasterButton_Click(object sender, EventArgs e) // The master button, it verifies gameplay. (Enter Button)
        {
            
            if (gameStarted == true && gameStick == false)
            {
                MasterButton.Enabled = false;
                // Check if word exists.
                string[] CheckForWord = System.IO.File.ReadAllLines(@"Words7Chars.txt");
                bool wordfound = false;
                bool wordvalid = false;              
                for (int i = 0; i < ResultLabel.Text.Length; i++) // Removes any underscores in front of or behind word for scanning
                {
                    if (ResultLabel.Text[0].ToString() == "_")
                    {
                        ResultLabel.Text = ResultLabel.Text.Substring(1, ResultLabel.Text.Length - 1);
                    
                    }
                    else if (ResultLabel.Text[ResultLabel.Text.Length - 1].ToString() == "_")
                    {
                        ResultLabel.Text = ResultLabel.Text.Substring(0, ResultLabel.Text.Length - 1);

                    }
                    if (ResultLabel.Text.ToUpper() != ResultLabel.Text.ToLower())
                    {
                        wordvalid = true;
                    }
                }

                if (wordvalid == true)
                {
                    string[] LOrdering = System.IO.File.ReadAllLines(@"LetterValues.txt");
                    int WordInc = 0;

                    

                    for (int i = 0; i <= 25; i++)
                    {
                        if (LOrdering[i][0].ToString() == ResultLabel.Text[0].ToString().ToLower())
                        {
                            if (i < 25/2)
                            {
                                while (CheckForWord[WordInc][0] != LOrdering[(25 / 2) + 1][0]) //Earlier letters start search on a, later letters start search on z.
                                {
                                    if (ResultLabel.Text.ToLower() == CheckForWord[WordInc]) //print individual lines
                                    {
                                        wordfound = true;
                                    }
                                    WordInc = WordInc + 1;
                                }
                            }
                            else if( i>= 25/2 )
                            {
                                WordInc = CheckForWord.Length - 1;
                                while (CheckForWord[WordInc][0] != LOrdering[(25 / 2) - 1][0]) //Earlier letters start search on a, later letters start search on z.
                                {
                                    if (ResultLabel.Text.ToLower() == CheckForWord[WordInc]) //print individual lines
                                    {
                                        wordfound = true;
                                    }
                                    WordInc = WordInc - 1;
                                }
                            }
                            break;
                        }
                    }
                    
                   




                    //for (int i = 0; i < CheckForWord.Length; i++)
                    //{

                    //}
                }

                int UnderScoreCount = 0;
                MultiplierMain.Visible = false;
                MultiplierBack.Visible = false;
                MultiplierTopBAR.Visible = false;
                MultiplierBottomBAR.Visible = false;
                for (int i = 0; i < PlayerChrs.Length; i++)
                {
                    if (PlayerChrs[i] == '_' && UserLetters[i].Enabled == false)
                    {
                        UserLetters[i].Enabled = true;
                        int randompoints = RPick.Next(-5, 6);
                        UnderScoreCount = UnderScoreCount + 1;

                        if (randompoints > 0)
                        {
                            MultiplierMain.Visible = true;
                            MultiplierMain.Location = new Point(197 + (144 * (i)), 65);
                            MultiplierMain.ForeColor = Color.FromArgb(0, 153, 0);
                            UserLetters[i].ForeColor = Color.FromArgb(255, 128, 0);
                            await Task.Delay(900);
                            MultiplierMain.Visible = false;
                        }


                        if (LMultiplier != "0")
                        {
                            if (LMultiplier != "A")
                            {
                                if (int.Parse(LMultiplier) - 1 == i)
                                {
                                    UserLetters[i].Text = "+ " + (randompoints * multi_placement[2]).ToString();
                                    UserLetters[i].ForeColor = Color.FromArgb(0, 0, 255);
                                    plrpoints[plrturn - 1] = plrpoints[plrturn - 1] + (randompoints * multi_placement[2]);
                                }
                                else
                                {
                                    UserLetters[i].Text = "+ " + (randompoints).ToString();
                                    UserLetters[i].ForeColor = Color.FromArgb(0, 153, 0);
                                    plrpoints[plrturn - 1] = plrpoints[plrturn - 1] + (randompoints);

                                }
                            }
                            else
                            {
                                UserLetters[i].Text = "+ " + (randompoints * multi_placement[2]).ToString();
                                UserLetters[i].ForeColor = Color.FromArgb(0, 0, 255);
                                plrpoints[plrturn - 1] = plrpoints[plrturn - 1] + (randompoints * multi_placement[2]);

                            }


                        }
                        else
                        {
    
                            UserLetters[i].Text = "+ " + (randompoints).ToString();
                            UserLetters[i].ForeColor = Color.FromArgb(0, 153, 0);
                            plrpoints[plrturn - 1] = plrpoints[plrturn - 1] + (randompoints);

                        } 

                        if (randompoints < 0)
                        {
                            MultiplierMain.Visible = true;
                            MultiplierMain.Location = new Point(197 + (144 * (i)), 65);
                            MultiplierMain.ForeColor = Color.FromArgb(255, 0, 0);
                            UserLetters[i].ForeColor = Color.FromArgb(255, 128, 0);
                            UserLetters[i].Text = UserLetters[i].Text.Substring(1, UserLetters[i].Text.Length - 1);
                            await Task.Delay(900);
                            MultiplierMain.Visible = false;
                            UserLetters[i].ForeColor = Color.FromArgb(255, 0, 0); 
                        }
                        else if (randompoints == 0)
                        {
                            MultiplierMain.Visible = true;
                            MultiplierMain.Location = new Point(197 + (144 * (i)), 65);
                            MultiplierMain.ForeColor = Color.FromArgb(139, 69, 19);
                            UserLetters[i].ForeColor = Color.FromArgb(255, 128, 0);
                            await Task.Delay(900);
                            MultiplierMain.Visible = false;
                            UserLetters[i].ForeColor = Color.FromArgb(139, 69, 19);
                        }      
                    }
                }
                PlayerSystem("PointsUpdate", 0);

                if (wordfound == true)
                {
                    //LetterValues



                    string[] LValues = System.IO.File.ReadAllLines(@"LetterValues.txt");
                    gameStick = true;        
                    for (int i = 0; i < PlayerChrs.Length; i++)
                    {

                        for (int j = 0; j <= 25; j++)
                        {
                            if (PlayerChrs[i].ToString().ToLower() == LValues[j][0].ToString() && wordfound == true)
                            {
                                // Reward points
                                if (UserLetters[i].Enabled == false)
                                {

                                    if (LMultiplier != "0")
                                    {
                                        if (LMultiplier != "A")
                                        {
                                            if (int.Parse(LMultiplier) - 1 == i)
                                            {
                                                UserLetters[i].Text = "+ " + (int.Parse(LValues[j].Substring(4, LValues[j].Length - 4)) * multi_placement[2]).ToString();
                                                UserLetters[i].ForeColor = Color.FromArgb(0, 0, 255);
                                                UserLetters[i].Text = "+ " + (int.Parse(LValues[j].Substring(4, LValues[j].Length - 4)) * multi_placement[2]).ToString();
                                                plrpoints[plrturn - 1] = plrpoints[plrturn - 1] + (int.Parse(LValues[j].Substring(4, LValues[j].Length - 4)) * multi_placement[2]);
                                                UserLetters[i].Enabled = true;
                                            }
                                            else
                                            {
                                                UserLetters[i].Text = "+ " + LValues[j].Substring(4, LValues[j].Length - 4);
                                                UserLetters[i].ForeColor = Color.FromArgb(0, 153, 0);
                                                plrpoints[plrturn - 1] = plrpoints[plrturn - 1] + int.Parse(LValues[j].Substring(4, LValues[j].Length - 4));
                                                UserLetters[i].Enabled = true;
                                            }
                                        }
                                        else
                                        {
                                            UserLetters[i].Text = "+ " + (int.Parse(LValues[j].Substring(4, LValues[j].Length - 4)) * multi_placement[2]).ToString();
                                            UserLetters[i].ForeColor = Color.FromArgb(0, 0, 255);
                                            plrpoints[plrturn - 1] = plrpoints[plrturn - 1] + (int.Parse(LValues[j].Substring(4, LValues[j].Length - 4)) * multi_placement[2]);
                                            UserLetters[i].Enabled = true;
                                        }

                                    }
                                    else
                                    {
                                        UserLetters[i].Text = "+ " + LValues[j].Substring(4, LValues[j].Length - 4);
                                        UserLetters[i].ForeColor = Color.FromArgb(0, 153, 0);
                                        plrpoints[plrturn - 1] = plrpoints[plrturn - 1] + int.Parse(LValues[j].Substring(4, LValues[j].Length - 4));
                                        UserLetters[i].Enabled = true; 
                                    }



                                }

                            }

                        }
                    }

                    PlayerSystem("PointsUpdate", 0);
                    MessageBox.Show("Nice word you got there!");


                    gameStick = false;                 
                }
                else
                {
                  MessageBox.Show("Couldn't make a word, huh?");
                }
                eventmessage[0] = " ";
                eventmessage[1] = " ";
                MasterButton.Enabled = true;
                ClearButton.Enabled = true;
                if (plrturn == plrturnmax)
                {
                    plrturn = 1;
                    gameround += 1;
                }
                else
                {
                    plrturn += 1;
                }
                PlayerSystem("PlayerSelected", plrturn);
                eventmessage[0] = "It is now Player " + plrturn + "'s turn";
                for (int i = 0; i < UserLetters.Length; i++)
                {
                    UserLetters[i].Text = "";
                }
                await Task.Delay(600);
                WordLoad();
                EventDisplayer(eventmessage, 1500);


            }

        }
    }
}
