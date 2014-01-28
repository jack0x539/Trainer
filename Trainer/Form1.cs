using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Trainer
{
    using FormsTimer = System.Windows.Forms.Timer;

    public partial class Form1 : Form
    {
        PersonalTrainer trainer;

        public Form1()
        {
            InitializeComponent();

            trainer = new PersonalTrainer(
                new List<Exercise>() 
                { 
                    new Exercise("Sit Ups", 5),
                    new Exercise("Squats", 10),
                    new Exercise("Push Ups", 15) 
                });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            trainer.ExerciseUpdated += trainer_ExerciseUpdated;
            trainer.WorkoutCompleted += trainer_WorkoutCompleted;
            trainer.Begin();
        }

        void trainer_WorkoutCompleted(object sender)
        {
            trainer.ExerciseUpdated -= trainer_ExerciseUpdated;
            trainer.WorkoutCompleted -= trainer_WorkoutCompleted;

            WriteControlText(lblTime, "WORKOUT COMPLETED!");
        }

        void trainer_ExerciseUpdated(object sender, Exercise exercise)
        {
            WriteControlText(lblExercise, "{0}", exercise.Name);
            WriteControlText(lblTime, "{0:MM:ss}", exercise.Duration);

            WriteControlText(label2, "Workout Duration: {0:MM:ss}", ((PersonalTrainer)sender).WorkoutDuration);

            
        }

        void WriteControlText(Control control, string fmt, params object[] args)
        {
            if (InvokeRequired)
            {
                Invoke((ThreadStart)delegate { WriteControlText(control, fmt, args); });
                return;
            }

            control.Text = String.Format(fmt, args);
        }

        void 
    }
}
