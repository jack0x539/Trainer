using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Trainer
{
    public delegate void ExerciseStartedDel(object sender, Exercise exercise);
    public delegate void ExerciseUpdatedDel(object sender, Exercise exercise);
    public delegate void WorkoutCompletedDel(object sender);

    public class PersonalTrainer
    {
        TimeSpan _workoutDuration;

        int _iExercise;
        Exercise _curExercise;
        Exercise[] _exercises;

        Timer _timer;

        public event ExerciseStartedDel ExerciseStarted;
        public event ExerciseUpdatedDel ExerciseUpdated;
        public event WorkoutCompletedDel WorkoutCompleted;

        static readonly TimeSpan Second = new TimeSpan(0, 0, 1);
        static readonly TimeSpan NegativeSecond = Second.Negate();
        static readonly TimeSpan ZeroDuration = new TimeSpan();

        public TimeSpan WorkoutDuration { get { return _workoutDuration; } }

        public PersonalTrainer(IEnumerable<Exercise> exercises)
        {
            _exercises = exercises.ToArray();
            Reset();
        }

        private void Reset()
        {
            _iExercise = 0;
            _workoutDuration = new TimeSpan();
            _timer = new Timer(1000);
            _timer.AutoReset = true;
            _timer.Elapsed += _timer_Elapsed;
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _curExercise.Duration = _curExercise.Duration.Add(NegativeSecond);
            _workoutDuration = _workoutDuration.Add(Second);

            OnExerciseUpdated(_curExercise);

            if (_curExercise.Duration <= ZeroDuration)
            {
                _iExercise++;

                if (_iExercise >= _exercises.Length)
                {
                    _timer.AutoReset = false;
                    _timer.Stop();

                    OnWorkoutCompleted();
                }
                else
                {
                    _curExercise = _exercises[_iExercise];
                    OnExerciseStarted(_curExercise);
                }
            }
        }

        public void Begin()
        {
            Reset();
            
            _curExercise = _exercises[_iExercise];
            _timer.Start();

            OnExerciseStarted(_curExercise);
        }

        private void OnExerciseStarted(Exercise exercise)
        {
            if (ExerciseStarted != null)
                ExerciseStarted(this, exercise);
        }

        private void OnExerciseUpdated(Exercise exercise)
        {
            if (ExerciseUpdated != null)
                ExerciseUpdated(this, exercise);
        }

        private void OnWorkoutCompleted()
        {
            if (WorkoutCompleted != null)
                WorkoutCompleted(this);
        }
    }
}
