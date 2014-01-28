using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trainer
{
    public struct Exercise
    {
        string _name;
        TimeSpan _duration;

        public Exercise(string name, TimeSpan duration)
        {
            _name = name;
            _duration = duration;
        }

        public Exercise(string name, int seconds)
        {
            _name = name;
            _duration = new TimeSpan(0, 0, seconds);
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public TimeSpan Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
    }
}
