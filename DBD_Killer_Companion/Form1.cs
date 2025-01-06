using System.Diagnostics;

namespace DBD_Killer_Companion
{
    public partial class Form1 : Form
    {
        Boolean onGame = false;
        PictureBox[] pb_HookCounters;
        PictureBox[] pb_Players;
        int[] HookTracker = new int[4];
        int minute;
        int second;

        public Form1()
        {
            InitializeComponent();

            pb_HookCounters = new PictureBox[8];
            pb_HookCounters[0] = pb_s1Mark1;
            pb_HookCounters[1] = pb_s1Mark2;
            pb_HookCounters[2] = pb_s2Mark1;
            pb_HookCounters[3] = pb_s2Mark2;
            pb_HookCounters[4] = pb_s3Mark1;
            pb_HookCounters[5] = pb_s3Mark2;
            pb_HookCounters[6] = pb_s4Mark1;
            pb_HookCounters[7] = pb_s4Mark2;

            pb_Players = new PictureBox[4];
            pb_Players[0] = pb_Survivor1;
            pb_Survivor1.Image = Properties.Resources.chromakey;
            pb_Players[1] = pb_Survivor2;
            pb_Survivor2.Image = Properties.Resources.chromakey;
            pb_Players[2] = pb_Survivor3;
            pb_Survivor3.Image = Properties.Resources.chromakey;
            pb_Players[3] = pb_Survivor4;
            pb_Survivor4.Image = Properties.Resources.chromakey;

            initializeHookCounters();
        }

        private void initializeHookCounters()
        {
            for (int x = 0; x < 8; x++)
            {
                pb_HookCounters[x].Image = Properties.Resources.image_5;
            }
        }

        private void initializeMain()
        {
            for (int x = 0; x < 4; x++)
            {
                pb_Players[x].Enabled = true;
                HookTracker[x] = 0;
            }

            initializeHookCounters();
        }

        private void btn_Survivor_Click(object sender, EventArgs e)
        {
            PictureBox clicked_PictureBox = sender as PictureBox;

            if (clicked_PictureBox != null)
            {
                int index = Array.IndexOf(pb_Players, clicked_PictureBox);
                if (index != -1 && HookTracker[index] < 2)
                {
                    updateTracker(index);
                }
            }
        }

        private void KeyController(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("test");
            if (e.KeyCode == Keys.Q && onGame == true)
            {
                updateTracker(0);
            }
            else if (e.KeyCode == Keys.W && onGame == true)
            {
                updateTracker(1);
            }
            else if (e.KeyCode == Keys.E && onGame == true)
            {
                updateTracker(2);
            }
            else if (e.KeyCode == Keys.R && onGame == true)
            {
                updateTracker(3);
            }
            else if (e.KeyCode == Keys.A && onGame == true)
            {
                revertTracker(0);
            }
            else if (e.KeyCode == Keys.S && onGame == true)
            {
                revertTracker(1);
            }
            else if (e.KeyCode == Keys.D && onGame == true)
            {
                revertTracker(2);
            }
            else if (e.KeyCode == Keys.F && onGame == true)
            {
                revertTracker(3);
            }
            else if(e.KeyCode == Keys.Space)
            {
                btn_Timer_Click(sender, e);
            }
            else if(e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void updateTracker(int index)
        {
            pb_HookCounters[index * 2 + HookTracker[index]].Image = Properties.Resources.image_6;
            HookTracker[index]++;
        }

        private void revertTracker(int index)
        {
            if (HookTracker[index] > 0)
            {
                pb_HookCounters[index * 2 + HookTracker[index] - 1].Image = Properties.Resources.image_5;
                HookTracker[index]--;
            }
        }

        private void btn_Timer_Click(object sender, EventArgs e)
        {
            if (onGame)
            {
                //turn off
                //timer stop
                //disable button
                btn_Timer.Text = "START";



                timer1.Stop();
                txtbox_timer.BackColor = SystemColors.Control;
                onGame = false;
            }
            else
            {
                //turn on
                //timer start
                //enable button
                //reset markers
                minute = 0;
                second = 0;
                txtbox_timer.Text = translate_timer(minute, second);

                btn_Timer.Text = "STOP";
                timer1.Start();
                initializeMain();
                txtbox_timer.BackColor = SystemColors.HighlightText;
                onGame = true;
            }
        }

        private String translate_timer(int minute, int second)
        {
            Debug.WriteLine(minute + ":" + second);
            String S_minute = "";
            String S_second = "";


            S_minute = minute.ToString();
            if (minute <= 9)
            {
                S_minute = "0" + S_minute;
            }

            S_second = second.ToString();
            if (second <= 9)
            {
                S_second = "0" + S_second;
            }

            Debug.WriteLine("Actual -> " + S_minute + ":" + S_second);
            return S_minute + " : " + S_second;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            second++;

            if (second >= 60)
            {
                minute++;
                second = 0;
            }

            txtbox_timer.Text = translate_timer(minute, second);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("test");
        }
    }
}
