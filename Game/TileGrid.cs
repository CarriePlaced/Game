using System;

namespace Game
{
    public class TileGrid
    {
        public TileGrid(int row, int col, int mineCount)
        {
            _row = row;
            _col = col;
            _mineCount = mineCount;
        }

        public Tile[] Grid
        {
            get
            {
                if (_tileArray == null)
                {
                    // initialize
                    _tileArray = new Tile[_row * _col];
                    for (int i = 0; i < _row; i++)
                    {
                        for (int j = 0; j < _col; j++)
                        {
                            _tileArray[i + j * _col] = new Tile(i, j);
                        }
                    }

                    // generate mines
                    Random random = new Random();
                    int counter = _mineCount;
                    while (counter > 0)
                    {
                        int index = random.Next(100);
                        if (!_tileArray[index].IsMine)
                        {
                            _tileArray[index].Mines = -1;
                            _tileArray[index].IsMine = true;
                            counter--;
                        }
                    }

                    // count mines
                    for (int i = 0; i < _row; i++)
                    {
                        for (int j = 0; j < _col; j++)
                        {
                            if (!_tileArray[i + j * _col].IsMine)
                            {
                                for (int x = i - 1; x <= i + 1; x++)
                                {
                                    for (int y = j - 1; y <= j + 1; y++)
                                    {
                                        if (x >= 0 && y >= 0 && x < _row && y < _col && _tileArray[x + y * _col].IsMine)
                                        {
                                            _tileArray[i + j * _col].Mines++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return _tileArray;
            }
        }

        public void UpdateSurrounding(Tile tile)
        {
            if (tile.Mines == 0)
            {
                for (int x = tile.X - 1; x <= tile.X + 1 &&  x <_row ; x++)
                {
                    for (int y = tile.Y - 1; y <= tile.Y + 1 && y < _col; y++)
                    {
                        if (x >= 0 && y >= 0 && _tileArray[x + y * _col] != tile)
                        {
                            if (_tileArray[x + y * _col].Mines == 0 && !_tileArray[x + y * _col].State)
                            {
                                _tileArray[x + y * _col].State = true;
                                UpdateSurrounding(_tileArray[x + y * _col]);
                            }
                            _tileArray[x + y * _col].State = true;
                        }
                    }
                }
            }
        }

        private readonly int _row;
        private readonly int _col;
        private readonly int _mineCount;
        private Tile[] _tileArray;
    }
}
