using System.Collections.ObjectModel;
using System.Media;
using System.Windows.Input;
using Game.Properties;

namespace Game
{
    public class GameViewModel : ViewModelBase
    {
        private readonly TileGrid _grid;
        private bool _currentPlayerRed;
        private ObservableCollection<Tile> _gameGrid;
        private int _mineCount = 30;

        public GameViewModel()
        {
            ClickCommand = new RelayCommand<Tile>(tile => Click(tile));
            _grid = new TileGrid(10, 10, _mineCount);
            _gameGrid = new ObservableCollection<Tile>(_grid.Grid);
        }

        public static ICommand ClickCommand { get; private set; }

        public int PlayerRed { get; set; }

        public int PlayerBlue { get; set; }

        public int MineCount
        {
            get { return _mineCount; }
            set
            {
                if (_mineCount != value)
                {
                    _mineCount = value;
                    OnPropertyChanged("MineCount");
                }
            }
        }

        public bool IsPlayerRed
        {
            get { return _currentPlayerRed; }
            set
            {
                if (_currentPlayerRed != value)
                {
                    _currentPlayerRed = value;
                    OnPropertyChanged("IsPlayerRed");
                }
            }
        }

        public ObservableCollection<Tile> GameGrid
        {
            get { return _gameGrid; }
            set
            {
                if (_gameGrid != value)
                {
                    _gameGrid = value;
                    OnPropertyChanged("GameGrid");
                }
            }
        }

        private void Click(Tile tile)
        {
            if (tile.State != true)
            {
                tile.State = true;
                tile.IsPlayerRed = IsPlayerRed;
                _grid.UpdateSurrounding(tile);

                if (tile.IsMine)
                {
                    if (IsPlayerRed)
                    {
                        PlayerRed++;
                        OnPropertyChanged("PlayerRed");
                    }
                    else
                    {
                        PlayerBlue++;
                        OnPropertyChanged("PlayerBlue");
                    }
                    var simpleSound = new SoundPlayer(Resources.Mine);
                    simpleSound.Play();
                    MineCount--;
                }
                else
                {
                    var simpleSound = new SoundPlayer(Resources.Number);
                    simpleSound.Play();
                    IsPlayerRed = !IsPlayerRed;
                }
            }
        }
    }
}