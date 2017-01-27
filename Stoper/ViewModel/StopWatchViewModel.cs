using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Stoper.ViewModel
{
    using Model;
    class StopWatchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private StopwatchModel _stopWatchModel = new StopwatchModel();
        private DispatcherTimer _timer = new DispatcherTimer(); 

        public bool Running { get { return _stopWatchModel.Running; } }
        bool _lastRunning;

        public StopWatchViewModel()
        {
            _timer.Interval = TimeSpan.FromMilliseconds(50);
            _timer.Tick += TimerTick;
            _timer.Start();
            Start();

            _stopWatchModel.LapTimeUpdated += LapTimeUpdatedEventHandler;
        }
        public void Start()
        {
            _stopWatchModel.Start();
        }

        public void Stop()
        {
            _stopWatchModel.Stop();
        }

        public void Reset()
        {
            bool running = Running;
            _stopWatchModel.Reset();
            if (running)
            {
                _stopWatchModel.Start();
            }
        }

        public void Lap()
        {
            _stopWatchModel.Lap();
        }

        int _lastHour, _lastMinutes;
        decimal _lastSeconds;

        void TimerTick(object sender, object e)
        {
            if (_lastRunning != Running)
            {
                _lastRunning = Running;
                OnPropertyChanged("Running");
            }
            if (_lastHour != Hours)
            {
                _lastHour = Hours;
                OnPropertyChanged("Hours");
            }
            if (_lastMinutes != Minutes)
            {
                _lastMinutes = Minutes;
                OnPropertyChanged("Minutes");
            }
            if (_lastSeconds != Seconds)
            {
                _lastSeconds = Seconds;
                OnPropertyChanged("Seconds");
            }
        }

        public int Hours
        {
            get { return _stopWatchModel.Elapsed.HasValue ? _stopWatchModel.Elapsed.Value.Hours : 0; }
        }

        public int Minutes
        {
            get { return _stopWatchModel.Elapsed.HasValue ? _stopWatchModel.Elapsed.Value.Minutes : 0; }
        }

        public decimal Seconds
        {
            get
            {
                if (_stopWatchModel.Elapsed.HasValue)
                {
                    return (decimal)_stopWatchModel.Elapsed.Value.Seconds + (_stopWatchModel.Elapsed.Value.Milliseconds * .001M);
                }
                else
                {
                    return 0.0M;
                }
            }
        }

        public int LapHours
        {
            get { return _stopWatchModel.LapTime.HasValue ? _stopWatchModel.LapTime.Value.Hours : 0; }
        }

        public int LapMinutes
        {
            get { return _stopWatchModel.LapTime.HasValue ? _stopWatchModel.LapTime.Value.Minutes : 0; }
        }

        public decimal LapSeconds
        {
            get
            {
                if (_stopWatchModel.LapTime.HasValue)
                {
                    return (decimal)_stopWatchModel.LapTime.Value.Seconds + (_stopWatchModel.LapTime.Value.Milliseconds * .001M);
                }
                else
                {
                    return 0.0M;
                }
            }
        }

        int _lastLapHours, _lastLapMinutes;
        decimal _lastLapSeconds;

        private void LapTimeUpdatedEventHandler(object sender, LapEventArgs e)
        {
            if (_lastLapHours != LapHours)
            {
                _lastLapHours = LapHours;
                OnPropertyChanged("LapHours");
            }
            if (_lastLapMinutes != LapMinutes)
            {
                _lastLapMinutes = LapMinutes;
                OnPropertyChanged("LapMinutes");
            }
            if (_lastLapSeconds != LapSeconds)
            {
                _lastLapSeconds = LapSeconds;
                OnPropertyChanged("LapSeconds");
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
