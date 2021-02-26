using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace LifeGame
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        /// Field with digital values of cell state.
        /// </summary>
        public int[,] lifeField { get; set; } = new int[Life.MATRIX_WIDTH, Life.MATRIX_HEIGHT];

        /// <summary>
        /// Timer. It affords changing duration between updating cell states.
        /// </summary>
        private DispatcherTimer dispatcherTimer { get; set; } = new DispatcherTimer();

        /// <summary>
        /// Duration between updating cell states.
        /// </summary>
        public TimeSpan Duration { get; set; } = new TimeSpan(0, 0, 0, 0, 1);

        #region IsStarted
        private bool isStarted;
        public bool IsStarted
        {
            get => isStarted;
            set
            {
                isStarted = value;
                OnPropertyChanged("IsStarted");
            }
        }
        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Initializing timer.
        /// </summary>
        public MainViewModel()
        {
            dispatcherTimer.Tick += LifeTick;
            dispatcherTimer.Interval = Duration;
        }

        #endregion

        #region Events

        /// <summary>
        /// Update cell state with every tick of <see cref="dispatcherTimer"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LifeTick(object sender, EventArgs e)
        {
            MainWindow.MatrixUpdate();
            Life.StartLife(lifeField);
        }

        #endregion

        #region Commands

        #region ClearCommand

        private RelayCommand clearCommand;
        public RelayCommand ClearCommand
        {
            get => clearCommand ?? (clearCommand = new RelayCommand(obj =>
            {
                ClearField();
                MainWindow.FieldUpdate(lifeField);
            }));
        }

        #endregion

        #region StartCommand

        private RelayCommand startCommand;
        public RelayCommand StartCommand
        {
            get => startCommand ?? (startCommand = new RelayCommand(obj =>
            {
                if (!isStarted)
                    dispatcherTimer.Start();
                else
                    dispatcherTimer.Stop();
                IsStarted = !IsStarted;
            }));
        }

        #endregion

        #endregion

        #region Methods

        private void ClearField()
        {
            for(int i = 0; i < lifeField.GetLength(0); i++)
            {
                for(int k = 0; k < lifeField.GetLength(1); k++)
                {
                    lifeField[i, k] = Cell.DEAD_CELL;
                }
            }
        }

        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
