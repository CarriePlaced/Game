using System;

namespace Game
{
    public class Tile : ViewModelBase
    {
        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged("State");
                    OnPropertyChanged("FilePath");
                }
            }
        }

        public bool IsPlayerRed
        {
            get { return _isPlayerRed; }
            set
            {
                _isPlayerRed = value;
                OnPropertyChanged("IsPlayerRed");
                OnPropertyChanged("FilePath");
            }
        }

        public string FilePath
        {
            get
            {
                if (!State)
                {
                    return "Icons/notDiscovered.png";
                }

                if (IsMine && IsPlayerRed)
                {
                    return String.Format("Icons/redMine.png");
                }
                if (IsMine && !IsPlayerRed)
                {
                    return String.Format("Icons/blueMine.png");
                }
                return String.Format("Icons/{0}.png", Mines);
            }
        }

        public int Mines { get; set; }
        public bool IsMine { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        private bool _isPlayerRed;
        private bool _state;
    }
}
