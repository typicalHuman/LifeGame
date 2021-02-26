using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LifeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties
        /// <summary>
        /// Equivalent * in grid size.
        /// </summary>
        private static readonly GridLength STAR = new GridLength(1, GridUnitType.Star);

        /// <summary>
        /// Graphical matrix of rectnagles.
        /// </summary>
        private static Rectangle[,] rectMatrix { get; set; } = new Rectangle[Life.MATRIX_WIDTH, Life.MATRIX_HEIGHT];

        /// <summary>
        /// Temporary canvas to use in static methods.
        /// </summary>
        private static Canvas _canvas { get; set; }
        #endregion

        #region Constants

        private const int RECT_SIZE = 10;
        private const double RECT_THICKNESS = 0.1;

        private static readonly Brush WHITE = Brushes.White;
        private static readonly Brush BLACK = Brushes.Black;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            _canvas = canvas;
        }
       
        #region Methods

        /// <summary>
        /// Update digital matrix.
        /// </summary>
        public static void MatrixUpdate()
        {
            int[,] array = App.MainVM.lifeField;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int k = 0; k < array.GetLength(1); k++)
                {
                    int rectIndex = _canvas.Children.IndexOf(rectMatrix[i, k]);
                    if (rectMatrix[i, k].Fill == BLACK)
                        array[i, k] = Cell.ALIVE_CELL;
                    else
                        array[i, k] = Cell.DEAD_CELL;
                }
            }
        }

        /// <summary>
        /// Update graphical matrix.
        /// </summary>
        /// <param name="array">Digital matrix.</param>
        public static void FieldUpdate(int[,] array)
        {
            for(int i = 0; i < array.GetLength(0); i++)
            {
                for(int k = 0; k < array.GetLength(1); k++)
                {
                    int rectIndex = _canvas.Children.IndexOf(rectMatrix[i, k]);
                    if (array[i,k] == Cell.DEAD_CELL)
                        (_canvas.Children[rectIndex] as Rectangle).Fill = WHITE;
                    else
                        (_canvas.Children[rectIndex] as Rectangle).Fill = BLACK;
                    rectMatrix[i, k] = _canvas.Children[rectIndex] as Rectangle;
                }
            }
        }

        #region CreateRectangle

        /// <summary>
        /// Get rectangle with setted position on canvas.
        /// </summary>
        /// <param name="i">X coordinate.</param>
        /// <param name="k">Y coordinate.</param>
        /// <returns>New rectangle.</returns>
        private Rectangle CreateRectangle(int i, int k)
        {
            Rectangle rect = new Rectangle();
            rect.Fill = WHITE;
            SetRectSize(rect);
            SetRectStroke(rect);
            SetRectCanvasPosition(rect, i, k);
            SetRectEvents(rect);
            return rect;
        }

        private void SetRectSize(Rectangle rect)
        {
            rect.Height = RECT_SIZE;
            rect.Width = RECT_SIZE;
        }

        private void SetRectStroke(Rectangle rect)
        {
            rect.Stroke = Brushes.Gray;
            rect.StrokeThickness = RECT_THICKNESS;
        }

        private void SetRectCanvasPosition(Rectangle rect, int i, int k)
        {
            Canvas.SetTop(rect, k * RECT_SIZE);
            Canvas.SetLeft(rect, i * RECT_SIZE);
        }

        private void SetRectEvents(Rectangle rect)
        {
            rect.MouseEnter += Rect_MouseMove;
            rect.MouseLeftButtonDown += MouseLeftButtonDown;
        }

        #endregion

        private void SetRectColor(Rectangle rect)
        {
            rect.Fill = rect.Fill == BLACK ? WHITE : BLACK;
        }

        #endregion

        #region Events

        /// <summary>
        /// Handled when lbtn is pressed and cursor is in the rectangle hitbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rect_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
                (sender as Rectangle).Fill = BLACK;
        }
        private void MouseLeftButtonDown(object sender, EventArgs e)
        {
            SetRectColor((Rectangle)sender);
        }
        /// <summary>
        /// Initialize graphical matrix of rectangles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Life.MATRIX_WIDTH; i++)
            {
                for (int k = 0; k < Life.MATRIX_HEIGHT; k++)
                {
                    Rectangle rect = CreateRectangle(i, k);
                    canvas.Children.Add(rect);
                    rectMatrix[i, k] = rect;
                }
            }
        }

        #endregion

    }
}
