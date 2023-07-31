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
        private int timerInt = 600; // Initialisierung einer Variablen für einen Timer-Zähler
        private int huhnCounter = 0; // Initialisierung einer Variablen zur Zählung der Hühner
        private List<PictureBox> pictureBoxList = new List<PictureBox>(); // Initialisierung einer Liste für PictureBox-Objekte
        private static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer(); // Initialisierung eines Timer-Objekts für das Spiel
        private static System.Windows.Forms.Timer timerZeit = new System.Windows.Forms.Timer(); // Initialisierung eines Timer-Objekts zur Zeitmessung
        private int Zeit = 60; // Initialisierung einer Variablen für die verbleibende Zeit im Spiel
        private Label timerLabel; // Initialisierung eines Labels für die Anzeige der verbleibenden Zeit
        private int gesamtscore; // Initialisierung einer Variablen für den Gesamtscore des Spielers

        public Form1()
        {
            InitializeComponent(); // Initialisierung der Form-Komponenten
            SetBackgroundImage(); // Festlegen des Hintergrundbilds der Form
            this.DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Tick += new EventHandler(GameLoop); // Hinzufügen einer Methode (GameLoop) zum Tick-Event des Timers
            timer.Interval = 10; // Setzen des Timer-Intervalls auf 10 Millisekunden (für das GameLoop-Update)
            timer.Start(); // Starten des Timers

            timerZeit.Tick += new EventHandler(ZeitMessung); // Hinzufügen einer Methode (ZeitMessung) zum Tick-Event des Zeitmessungs-Timers
            timerZeit.Interval = 1000; // Setzen des Timer-Intervalls auf 1 Sekunde (für die Zeitmessung)
            timerZeit.Start(); // Starten des Zeitmessungs-Timers
        }

        private void GameLoop(object myObject, EventArgs myEventArgs)
        {
            timerInt++; // Erhöhen des Timer-Zählers

            if (timerInt % 100 == 0) // Alle 100 Einheiten des Timer-Zählers ein Huhn spawnen
            {
                SpawnHuhn(); // Methode zum Erzeugen eines neuen Huhns aufrufen
            }

            for (int i = pictureBoxList.Count - 1; i >= 0; i--) // Schleife zum Bewegen der vorhandenen Hühner auf dem Bildschirm
            {
                PictureBox pictureBox = pictureBoxList[i]; // Das aktuelle PictureBox-Objekt aus der Liste
                pictureBox.Location = new Point(pictureBox.Location.X + 7, pictureBox.Location.Y + new Random().Next(-3, 4)); // Verschieben des Huhns nach rechts

                if (pictureBox.Location.X > 2500) // Wenn das Huhn den rechten Rand des Bildschirms erreicht
                {
                    // Huhn aus der Liste und den Controls der Form entfernen und das PictureBox-Objekt freigeben
                    pictureBoxList.RemoveAt(i);
                    this.Controls.Remove(pictureBox);
                    pictureBox.Dispose();
                }
            }

            //Debug.WriteLine(Cursor.Position.ToString()); // Ausgabe der aktuellen Mausposition (Debugging-Zwecke)
        }

        private void ZeitMessung(object myObject, EventArgs myEventArgs)
        {
            Zeit--; // Verringern der verbleibenden Zeit
            lblZeit.Text = Zeit.ToString(); // Aktualisieren der Anzeige für die verbleibende Zeit

            if (Zeit <= 0) // Wenn die Zeit abgelaufen ist
            {
                timerZeit.Stop(); // Stopp des Zeitmessungs-Timers
                timer.Stop(); // Stopp des Haupt-Spiel-Timers
                ShowResultMessage(); // Anzeige einer Ergebnismeldung
            }
        }

        private void ShowResultMessage()
        {
            // Anzeigen einer MessageBox mit dem Spielergebnis
            MessageBox.Show("Die Zeit ist vorbei! Dein Score: " + gesamtscore.ToString(), "Ergebnisse", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SpawnHuhn()
        {
            // Methode zum Erzeugen eines neuen Huhns (PictureBox)

            PictureBox pictureBox = new PictureBox();
            huhnCounter++;
            pictureBox.Name = "Butt" + huhnCounter;

            int buttonHeight = Restart.Height; // Höhe des Buttons

            
            int spawnYPosition = new Random().Next(buttonHeight, this.ClientSize.Height - pictureBox.Height - this.ClientSize.Height / 100);

            pictureBox.Location = new Point(-100, spawnYPosition);

            int score = new Random().Next(20, 50); // Zufälliger Punktwert für das Huhn
            pictureBox.Size = new Size(score, score); // Größe der PictureBox entsprechend des Punktwerts
            pictureBox.Tag = score; // Punktwert des Huhns in das Tag-Attribut der PictureBox schreiben
            pictureBox.Image = Image.FromFile(@"..\\..\\..\\Bilder\\huhn.gif"); // Laden des Huhn-Bildes aus einer Datei
            pictureBox.BackColor = Color.Transparent; // Transparente Hintergrundfarbe für die PictureBox
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Skalieren des Bildes auf die PictureBox-Größe
            pictureBox.BorderStyle = BorderStyle.None; // Keine Rahmenlinie um die PictureBox anzeigen

            this.Controls.Add(pictureBox); // Hinzufügen der PictureBox zum Formular
            pictureBoxList.Add(pictureBox); // Hinzufügen der PictureBox zum Huhn-Liste
            pictureBox.Click += new EventHandler(NewPictureBox_Click); // Hinzufügen eines Click-Events für die PictureBox
            //Debug.WriteLine(Cursor.Position.ToString()); // Ausgabe der aktuellen Mausposition (Debugging-Zwecke)
        }

        // In event method.
        private async void NewPictureBox_Click(object sender, EventArgs e)
        {
            // Methode zum Behandeln des Klick-Events einer PictureBox (wenn der Spieler ein Huhn trifft)

            //Debug.WriteLine("Click!"); // Ausgabe einer Debug-Nachricht (Debugging-Zwecke)
            PictureBox pictureBox = (PictureBox)sender; // Das auslösende PictureBox-Objekt des Klick-Events
            pictureBox.Image = Image.FromFile(@"..\\..\\..\\Bilder\\huhn1.png"); // Ändern des Huhn-Bildes auf ein getroffenes Huhn
            await Task.Delay(200); // Kurze Pause (Animation für das getroffene Huhn)

            gesamtscore += Convert.ToInt16(pictureBox.Tag); // Erhöhen des Gesamtscores entsprechend dem Punktwert des getroffenen Huhns
            this.Controls.Remove(pictureBox); // Entfernen der PictureBox vom Formular
            pictureBox.Dispose(); // Freigeben des PictureBox-Objekts
            lblPunktzahl.Text = gesamtscore.ToString(); // Aktualisieren der Anzeige für die Punktzahl des Spielers
        }

        private void SetBackgroundImage()
        {
            // Methode zum Setzen des Hintergrundbilds der Form

            string imagePath = @"..\\..\\..\\Bilder\\hintergrund.jpg"; // Pfad zum Hintergrundbild

            if (System.IO.File.Exists(imagePath)) // Überprüfen, ob die Bilddatei existiert
            {
                try
                {
                    using (FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        BackgroundImage = Image.FromStream(stream); // Laden des Bildes und Setzen als Hintergrundbild
                        BackgroundImageLayout = ImageLayout.Stretch; // Stretch-Layout für das Hintergrundbild (es wird an die Größe der Form angepasst)
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Laden des Hintergrundbildes: " + ex.Message); // Ausgabe einer Fehlermeldung (Debugging-Zwecke)
                }
            }
            else
            {
                Debug.WriteLine("Hintergrundbild nicht gefunden: " + imagePath); // Ausgabe einer Fehlermeldung, wenn das Bild nicht gefunden wurde (Debugging-Zwecke)
            }
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            // Methode zum Neustart des Spiels

            // Zurücksetzen der spielrelevanten Variablen
            timerInt = 600;
            huhnCounter = 0;
            gesamtscore = 0;
            Zeit = 60;
            lblPunktzahl.Text = gesamtscore.ToString(); // Zurücksetzen der Punktzahl-Anzeige
            lblZeit.Text = Zeit.ToString(); // Zurücksetzen der Zeit-Anzeige

            // Entfernen der vorhandenen PictureBoxes
            foreach (var pictureBox in pictureBoxList)
            {
                this.Controls.Remove(pictureBox);
                pictureBox.Dispose();
            }
            pictureBoxList.Clear(); // Leeren der PictureBox-Liste

            // Wiederstarten der Timer
            timerZeit.Start();
            timer.Start();
        }
    }
}
