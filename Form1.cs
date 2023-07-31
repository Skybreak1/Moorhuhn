using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Moorhuhn
{
    public partial class Form1 : Form
    {
        private int timerInt = 600;
        private int huhnCounter = 0;
        private List<PictureBox> pictureBoxList = new List<PictureBox>();
        private static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private static System.Windows.Forms.Timer timerZeit = new System.Windows.Forms.Timer();
        private int Zeit = 0;
        private Label timerLabel;
        private int gesamtscore;

        public Form1()
        {
            InitializeComponent();
            SetBackgroundImage();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Tick += new EventHandler(GameLoop);
            timer.Interval = 10; // in ms
            timer.Start();

            timerZeit.Tick += new EventHandler(ZeitMessung);
            timerZeit.Interval = 1000;
            timerZeit.Start();
        }

        private void GameLoop(object myObject, EventArgs myEventArgs)
        {
            timerInt++;

            if (timerInt % 100 == 0) // Jede x Sekunden Spawn Huhn
            {
                SpawnHuhn();
            }

            for (int i = pictureBoxList.Count - 1; i >= 0; i--)
            {
                PictureBox pictureBox = pictureBoxList[i];
                pictureBox.Location = new Point(pictureBox.Location.X + 7, pictureBox.Location.Y);
                if (pictureBox.Location.X > 2500)
                {
                    // Remove the PictureBox from the list and the form's controls
                    pictureBoxList.RemoveAt(i);
                    this.Controls.Remove(pictureBox);
                    pictureBox.Dispose();
                }
            }

            //Debug.WriteLine(Cursor.Position.ToString());
        }

        private void ZeitMessung(object myObject, EventArgs myEventArgs)
        {
            Zeit++;
            lblZeit.Text = Zeit.ToString();
        }

        private void SpawnHuhn()
        {
            //Debug.WriteLine("Spawne Huhn als button");
            PictureBox pictureBox = new PictureBox();

            //Set name for a button to recognize it later.
            huhnCounter++;
            pictureBox.Name = "Butt" + huhnCounter;


            //button.Text = "New";

            pictureBox.Location = new Point(-100, new Random().Next(400));
            int score = new Random().Next(20, 50);
            pictureBox.Size = new Size(score, score);
            pictureBox.Tag = score;
            pictureBox.Image = Image.FromFile(@"..\\..\\..\\Bilder\\huhn.gif");
            pictureBox.BackColor = Color.Transparent;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.BorderStyle = BorderStyle.None;
            //pictureBox.Image.RotateFlip(RotateFlipType.Rotate180FlipY);

            this.Controls.Add(pictureBox);
            pictureBoxList.Add(pictureBox);

            // add click event to the button.
            pictureBox.Click += new EventHandler(NewPictureBox_Click);
            //Debug.WriteLine(Cursor.Position.ToString());

        }

        // In event method.
        private async void NewPictureBox_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Click!");
            PictureBox pictureBox = (PictureBox)sender;
            pictureBox.Image = Image.FromFile(@"..\\..\\..\\Bilder\\huhn1.png");
            await Task.Delay(200);
            gesamtscore += Convert.ToInt16(pictureBox.Tag);
            this.Controls.Remove(pictureBox);
            pictureBox.Dispose();
            lblPunktzahl.Text = gesamtscore.ToString();
        }

        private void SetBackgroundImage()
        {
            string imagePath = @"..\\..\\..\\Bilder\\hintergrund.jpg";
            if (System.IO.File.Exists(imagePath))
            {
                try
                {
                    using (FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        BackgroundImage = Image.FromStream(stream);
                        BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden des Hintergrundbildes: " + ex.Message);
                }
            }
            else
            {
                Debug.WriteLine("Hintergrundbild nicht gefunden: " + imagePath);
            }
        }
    }
}
