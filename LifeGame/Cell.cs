using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
    class Cell
    {
        #region Constants

        /// <summary>
        /// Size of dimension.
        /// </summary>
        private const int NEIGHBOURS_COUNT = 3;

        /// <summary>
        /// More than 3 neighbours equals death.
        /// </summary>
        private const int OVERPOPULATION_VALUE = 4;
        /// <summary>
        /// Less than 2 neighbours equals death.
        /// </summary>
        private const int LONELINESS_VALUE = 1;

        /// <summary>
        /// The middle of 3x3 matrix.
        /// </summary>
        private const int MIDDLE_MATRIX_INDEX = 1;

        /// <summary>
        /// Dead cell state.
        /// </summary>
        public const int DEAD_CELL = 0;
        /// <summary>
        /// Alive cell state.
        /// </summary>
        public const int ALIVE_CELL = 1;
        /// <summary>
        /// To remove state cell.
        /// </summary>
        public const int TOREMOVE_CELL = -1;

        /// <summary>
        /// New cell state.
        /// </summary>
        public const int NEW_CELL = 3;

        #endregion

        /// <summary>
        /// Cell state.
        /// </summary>
        public int Value
        {
            get => Neighbours[MIDDLE_MATRIX_INDEX, MIDDLE_MATRIX_INDEX];
            set
            {
                Neighbours[MIDDLE_MATRIX_INDEX, MIDDLE_MATRIX_INDEX] = value; 
            }
        }

        /// <summary>
        /// Cell neighbours (max 9).
        /// </summary>
        public int[,] Neighbours { get; set; } = new int[NEIGHBOURS_COUNT, NEIGHBOURS_COUNT];

        /// <summary>
        /// Initialize <see cref="Value"/> by coordinates and game field.
        /// </summary>
        /// <param name="i">X coordinate.</param>
        /// <param name="k">Y coordinate.</param>
        /// <param name="field">Game field.</param>
        public Cell(int i, int k, int[,] field)
        {
            Value = field[i, k];
            InitNeighbours(i, k, field);
            var a = Neighbours;
        }

        /// <summary>
        /// Initialize <see cref="Value"/>'s neighbours.
        /// </summary>
        /// <param name="i">X coordinate.</param>
        /// <param name="k">Y coordinate.</param>
        /// <param name="field">Game field.</param>
        private void InitNeighbours(int i, int k, int[,] field)
        {
            i--; k--;
            for(int ii = i, ni = 0; ii < field.GetLength(0) && ni < NEIGHBOURS_COUNT; ii++, ni++)
            {
                if(ii >= 0)
                {
                    for(int kk = k, nk = 0; kk < field.GetLength(1) && nk < NEIGHBOURS_COUNT; kk++, nk++)
                    {
                        if(kk >= 0)
                        {
                            Neighbours[ni, nk] = field[ii, kk];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Is cell dead.
        /// </summary>
        public bool IsDead()
        {
            int nCounter = -1;
            for(int i = 0; i < NEIGHBOURS_COUNT; i++)
            {
                for(int k = 0; k < NEIGHBOURS_COUNT; k++)
                {
                    if (Neighbours[i, k] == ALIVE_CELL)
                    {
                        nCounter++;
                        if (nCounter >= OVERPOPULATION_VALUE)
                        {
                            Value = TOREMOVE_CELL;
                            return true;
                        }
                    }
                }
            }
            if (nCounter <= LONELINESS_VALUE)
            {
                Value = TOREMOVE_CELL;
                return true;
            }
            return false;
        }

        public int GetAliveNeighboursCount()
        {
            int nCounter = -1;
            if (Value == DEAD_CELL)
                nCounter = 0;
            for(int i = 0; i < NEIGHBOURS_COUNT; i++)
            {
                for(int k = 0; k < NEIGHBOURS_COUNT; k++)
                {
                    if (Neighbours[i, k] == ALIVE_CELL)
                        nCounter++;
                }
            }
            return nCounter;
        }

    }
}
