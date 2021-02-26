namespace LifeGame
{
    class Life
    {
        #region Constants

        public const int MATRIX_WIDTH = 48;
        public const int MATRIX_HEIGHT = 37;

        #endregion

        #region Properties

        /// <summary>
        /// Digital matrix, but instead matrix <see cref="Cell"/> object.
        /// </summary>
        private static Cell[,] lifeField { get; set; } = new Cell[MATRIX_WIDTH, MATRIX_HEIGHT];

        #endregion


        public static void StartLife(int[,] field)
        {
            InitializeField(field);
            Start();
        }


        private static void InitializeField(int[,] field)
        {
            for(int i = 0; i < MATRIX_WIDTH; i++)
            {
                for(int k = 0; k < MATRIX_HEIGHT; k++)
                {
                    Cell c = new Cell(i, k, field);
                    lifeField[i, k] = c;
                }
            }
        }

        private static void Start()
        {
            SetCellStates();
            MainWindow.FieldUpdate(GetNumbersMatrix());
        }

        private static void SetCellStates()
        {
            for (int i = 0; i < MATRIX_WIDTH; i++)
            {
                for (int k = 0; k < MATRIX_HEIGHT; k++)
                {
                    SetCellNewState(i, k);
                    SetCellToRemoveState(i, k);
                    UpdateCellState(i, k);
                }
            }
        }

        private static void SetCellToRemoveState(int i, int k)
        {
            lifeField[i, k].IsDead();
        }

        private static void SetCellNewState(int i, int k)
        {
            CreateNewCell(i, k);
        }



        private static void SetNewState()
        {
            for(int i = 0; i < MATRIX_WIDTH; i++)
            {
                for(int k = 0; k < MATRIX_HEIGHT; k++)
                {
                  
                }
            }
        }

        private static void CreateNewCell(int i, int k)
        {
            if(lifeField[i, k].GetAliveNeighboursCount() == 2)
            {
                for(int ni = i - 1, nn1 = 0; ni < MATRIX_WIDTH && nn1 < 3; ni++)
                {
                    if (ni >= 0)
                    {
                        for (int nk = k - 1, nn2 = 0; nk < MATRIX_HEIGHT && nn2 < 3; nk++)
                        {
                            if (nk >= 0)
                            {
                                if ((ni != i || nk != k) && lifeField[ni, nk].GetAliveNeighboursCount() == 3)
                                {
                                    lifeField[ni, nk].Value = Cell.NEW_CELL;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private static int[,] GetNumbersMatrix()
        {
            int[,] matrix = new int[MATRIX_WIDTH, MATRIX_HEIGHT];
            for(int i = 0; i < MATRIX_WIDTH; i++)
            {
                for(int k = 0; k < MATRIX_HEIGHT; k++)
                {
                    matrix[i, k] = lifeField[i, k].Value;
                }
            }
            return matrix;
        }

        private static void UpdateCellState(int i, int k)
        {
            if (lifeField[i, k].Value == Cell.NEW_CELL)
                lifeField[i, k].Value = Cell.ALIVE_CELL;
            else if (lifeField[i, k].Value == Cell.TOREMOVE_CELL)
                lifeField[i, k].Value = Cell.DEAD_CELL;
        }

    }
}
