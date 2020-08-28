using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;

namespace TetrisGame
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int speed = 700;
        Random losujKsztalt;
        private int rowCount = 0;
        private int columnCount = 0;
        private int leftPos = 0;
        private int downPos = 0;
        private int   currentWidth;
        private int currentHeight;
        private int currentNumber;
        private int nextNumber;
        private int tetrisGridColumn;
        private int tetrisGridRow;
        private int rotation = 0;
        private bool gameActive = false;
        private bool nextShapeDrawed = false;
        private int[,] currentBlock = null;
        private bool isRotated = false;
        private bool bottomCollidied = false;
        private bool leftCollided = false;
        private bool rightCollided = false;
        private bool isGameOver = false;
        private int gameSpeed;
        private int levelScale = 60;
        private double gameSpeedCounter = 0;
        private int gameLevel = 1;
        private int gameScore = 0;

        List<System.Media.SoundPlayer> soundlist = new List<System.Media.SoundPlayer>();

        DispatcherTimer timer;
        //block declare
        private static Color T_Block = Colors.YellowGreen;
        private static Color L_Block = Colors.Green;
        private static Color K_Block = Colors.Blue;
        private static Color I_Block = Colors.Red;
        private static Color Z_Block = Colors.Purple;
        private static Color J_Block = Colors.DarkOrange;
        private static Color C_Block = Colors.AliceBlue;
        private static Color S_Block = Colors.DarkOrchid;
        List<int> currentBlockRow = null;
        List<int> currentBlockColumn = null;


        Color[] tabColor = {T_Block,
                            L_Block,
                            K_Block,
                            I_Block,
                            Z_Block,
                            J_Block,
                            S_Block};
        string[] array = { "","T_Model_0","L_Model_0","K_Model","I_Model_0","Z_Model_0","J_Model_0","S_Model_0" };
        public int[,] T_Model_0 = new int[2, 3] { { 1, 1, 1 }, { 0, 1, 0 } };
        public int[,] L_Model_0 = new int[3, 2] { { 1, 0 }, { 1, 0 }, { 1, 1 } };
        public int[,] K_Model = new int[2, 2] { { 1, 1 }, { 1, 1 } };
        public int[,] I_Model_0 = new int[3, 1] { { 1 }, { 1 }, { 1 } };
        public int[,] Z_Model_0 = new int[2, 3] { { 1, 1, 0 }, { 0, 1, 1 } };
        public int[,] C_Model_0 = new int[3, 2] { { 1, 1 }, { 1, 0 }, { 1, 1 } };
        public int[,] J_Model_0 = new int[2, 3] { { 1, 0, 0 }, { 1, 1, 1 } };
        public int[,] S_Model_0 = new int[2, 3] { { 0, 1, 1 }, { 1, 1, 0 } };
        public int[,] T_Model_90 = new int[3, 2] { { 1, 0 }, { 1, 1 }, { 1, 0 } };
        public int[,] L_Model_90 = new int[2, 3] { { 1, 1, 1 }, { 1, 0, 0 } };
        public int[,] I_Model_90 = new int[1, 3] { { 1, 1, 1 } };
        public int[,] Z_Model_90 = new int[3, 2] { { 0, 1 }, { 1, 1 }, { 1, 0 } };
        public int[,] C_Model_90 = new int[2, 3] { { 1, 1, 1 }, { 1, 0, 1 } };
        public int[,] J_Model_90 = new int[3, 2] { { 1, 1 }, { 1, 0 }, { 1, 0 } };
        public int[,] S_Model_90 = new int[3, 2] { { 1, 0 }, { 1, 1 }, { 0, 1 } };
        public int[,] T_Model_180 = new int[2, 3] { { 0, 1, 0 }, { 1, 1, 1 } };
        public int[,] L_Model_180 = new int[3, 2] { { 1, 1 }, { 0, 1 }, { 0, 1 } };
        public int[,] I_Model_180 = new int[3, 1] { { 1 }, { 1 }, { 1 } };
        public int[,] Z_Model_180 = new int[2, 3] { { 1, 1, 0 }, { 0, 1, 1 } };
        public int[,] C_Model_180 = new int[3, 2] { { 1, 1 }, { 0, 1 }, { 1, 1 } };
        public int[,] J_Model_180 = new int[2, 3] { { 1, 1, 1 }, { 0, 0, 1 } };
        public int[,] T_Model_270 = new int[3, 2] { { 1, 0 }, { 1, 1 }, { 1, 0 } };
        public int[,] L_Model_270 = new int[2, 3] { { 0, 0, 1 }, { 1, 1, 1 } };
        public int[,] I_Model_270 = new int[1, 3] { { 1, 1, 1 } };
        public int[,] Z_Model_270 = new int[3, 2] { { 0, 1 }, { 1, 1, }, { 1, 0 } };
        public int[,] C_Model_270 = new int[2, 3] { { 1, 0, 1 }, { 1, 1, 1 } };
        public int[,] J_Model_270 = new int[3, 2] { { 0, 1 }, { 0, 1 }, { 1, 1 } };

        public object Task  { get; private set; }
        
       
        public MainWindow()
        {
            InitializeComponent();

            gameSpeed = speed;
            KeyDown += MainWindow_KeyDown;
            timer = new DispatcherTimer();
            timer.Interval =new TimeSpan(0, 0, 0, 0, gameSpeed);
            timer.Tick += Timer_Tick;
            tetrisGridColumn = grid.ColumnDefinitions.Count;
            tetrisGridRow = grid.RowDefinitions.Count;
            losujKsztalt = new Random();
            currentNumber = losujKsztalt.Next(1, 8);
            nextNumber = losujKsztalt.Next(1, 8);
            AddMusic music = new AddMusic(soundlist);


        }


        private void MainWindow_KeyDown(object sender,KeyEventArgs e)
        {
            if (!timer.IsEnabled) { return; }
            switch (e.Key.ToString())
            {
                case "Up" :
                    rotation += 90;
                    if (rotation > 270) { rotation = 0; }
                    zmienKsztalt(rotation);
                    break;
                case "W" :
                    rotation += 90;
                    if (rotation > 270) { rotation = 0; }
                    zmienKsztalt(rotation);
                    break;
                case "Down":
                    downPos++;
                    break;
                case "S":
                    downPos++;
                    break;
                case "Right":
                    Collided();
                    if (!rightCollided)
                    {
                        leftPos++;
                    }
                    rightCollided = false;
                    break;
                case "D":
                    Collided();
                    if (!rightCollided)
                    {
                        leftPos++;
                    }
                    rightCollided = false;
                    break;
                case "Left":
                    Collided();
                    if (!leftCollided)
                    {
                        leftPos--;
                    }
                    leftCollided = false;
                    break;
                case "A":
                    Collided();
                    if (!leftCollided)
                    {
                        leftPos--;
                    }
                    leftCollided = false;
                    break;



            }
            poruszanieBloczkow();
        }

        private void zmienKsztalt(int rotations)
        {
            if (rotationCollided(rotation))
            {
                rotation -= 90;
                return;
            }
            if (array[currentNumber].IndexOf("T_") == 0)
            {

                currentBlock = getVariableByString("T_Model_" + rotations);
            }
            else if (array[currentNumber].IndexOf("L_") == 0)
            {
                currentBlock = getVariableByString("L_Model_" + rotations);
            }
            else if (array[currentNumber].IndexOf("K_") == 0)
            {
                return;
            }
            else if (array[currentNumber].IndexOf("I_") == 0)
            {

                currentBlock = getVariableByString("I_Model_" + rotations);
            }
            else if (array[currentNumber].IndexOf("Z_") == 0)
            {

                currentBlock = getVariableByString("Z_Model_" + rotations);
            }
            else if (array[currentNumber].IndexOf("J_") == 0)
            {

                currentBlock = getVariableByString("J_Model_" + rotations);
            }
            else if (array[currentNumber].IndexOf("S_") == 0)
            {
                if (rotation > 90) { rotation = rotation = 0; }
                currentBlock = getVariableByString("S_Model_" + rotation);
            }
            isRotated = true;
            dodajBloczek(currentNumber, leftPos, downPos);
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            downPos++;
            poruszanieBloczkow();
            if (gameSpeedCounter >= levelScale)
            {
                if(gameSpeed>=50)
                {
                    gameSpeed -= 50;
                    gameLevel++;
                    level.Text = "Level: " + gameLevel.ToString();

                }
                else
                {
                    gameSpeed = 50;
                }
                timer.Stop();
                timer.Interval = new TimeSpan(0, 0, 0, gameSpeed);
                timer.Start();
                gameSpeedCounter = 0;
            }
            gameSpeedCounter += (gameSpeed / 1000f);
        }

        
        

        private void poruszanieBloczkow()
        {
            leftCollided = false;
            rightCollided = false;
            Collided();
            if(leftPos>(tetrisGridColumn-currentWidth))
            {
                leftPos = (tetrisGridColumn - currentWidth);
            }
            else if (leftPos < 0)
            {
                leftPos = 0;
            }

            if (bottomCollidied)
            {
                stoped();
                return;
            }
            dodajBloczek(currentNumber, leftPos, downPos);
        }



        private void stoped()
        {
            timer.Stop();
            Music styk = new Music(2, soundlist);
            if (downPos <= 2)
            {
                gameOver();
                return;
            }
            int index = 0;
            while (index < grid.Children.Count)
            {
                UIElement elemnt = grid.Children[index];
                if(elemnt is Rectangle)
                {
                    Rectangle square = (Rectangle)elemnt;
                    if (square.Name.IndexOf("moving_") == 0)
                    {
                        string newName = square.Name.Replace("moving_", "arrived_");
                        square.Name = newName;
                    }
                   
                }
                    index++;
            }
            checkComplete();
            reset();
            timer.Start();
        }

      private void checkComplete()
        {

            int gridRow = grid.RowDefinitions.Count;
            int gridColumn = grid.ColumnDefinitions.Count;
            int squareCount = 0;
            for (int row = gridRow; row >= 0; row--)
            {
                squareCount = 0;
                for (int column = gridColumn; column >= 0; column--)
                {
                    Rectangle square;
                    square = (Rectangle)grid.Children
                   .Cast<UIElement>()
                   .FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
                    if (square != null)
                    {
                        if (square.Name.IndexOf("arrived") == 0)
                        {
                            squareCount++;
                        }
                    }
                }
                if (squareCount == gridColumn)
                {
                    Music combo = new Music(0, soundlist);
                    deleteLine(row);
                    score.Text = getScore().ToString();
                    checkComplete();
                }
                //TODO zrobic combo

            }
        }

        private void gameOver()
        {
            isGameOver = true;
            Music gameov = new Music(1, soundlist);
            reset();
            start.Content = "Rozpocznij Grę";
            gameoverText.Visibility = Visibility.Visible;
            rowCount = 0;
            columnCount = 0;
            leftPos = 0;
            gameSpeedCounter = 0;
            gameSpeed = speed;
            gameLevel = 1;
            gameActive = false;
            gameScore = 0;
            nextShapeDrawed = false;
            currentBlock = null;
            currentNumber = losujKsztalt.Next(1, 8);
            nextNumber = losujKsztalt.Next(1, 8);
            timer.Interval = new TimeSpan(0, 0, 0, 0, gameSpeed);


        }
        private void Collided()
        {
            bottomCollidied = sprawdzKolidacje(0, 1);
            leftCollided = sprawdzKolidacje(-1, 0);
            rightCollided = sprawdzKolidacje(1, 0);
        }


        private void reset()
        {
            downPos = 0;
            leftPos = 3;
            isRotated = false;
            rotation = 0;
            currentNumber = nextNumber;
            if (!isGameOver)
            {
                dodajBloczek(currentNumber, leftPos);
            }
            nextShapeDrawed = false;
            losujKsztalt = new Random();
            nextNumber = losujKsztalt.Next(1, 8);
            bottomCollidied = false;
            leftCollided = false;
            rightCollided = false;

            
        }

        private void deleteLine(int row)
        {

            for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
            {
                Rectangle square;
                try
                {
                    square = (Rectangle)grid.Children
                   .Cast<UIElement>()
                   .FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == i);
                    grid.Children.Remove(square);
                }
                catch { }

            }

            foreach (UIElement element in grid.Children)
            {
                Rectangle square = (Rectangle)element;
                if (square.Name.IndexOf("arrived") == 0 && Grid.GetRow(square) <= row)
                {
                    Grid.SetRow(square, Grid.GetRow(square) + 1);
                }
            }
        }
        private int getScore()
        {
            gameScore += 50 * gameLevel;
            return gameScore;
        }

        private bool sprawdzKolidacje(int _leftRightOffset, int _bottomOffset)
        {
            Rectangle movingSquare;
            int squareRow = 0;
            int squareColumn = 0;
            for(int i = 0; i <=2; i++)
            {
                squareRow = currentBlockRow[i];
                squareColumn = currentBlockColumn[i];
                try
                {
                    movingSquare = (Rectangle)grid.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetRow(e) == squareRow +
                      _bottomOffset && Grid.GetColumn(e) == squareColumn + _leftRightOffset);
                    if (movingSquare != null)
                    {
                        if (movingSquare.Name.IndexOf("arrived") == 0)
                        {
                            return true;
                        }
                    }
                }
                catch(Exception e) {  }
            }
            if (downPos > (tetrisGridRow - currentHeight))
            {
                return true;
            }
            return false;
        }

        private void dodajBloczek(int bloczekNumber, int left = 0, int down = 0)
        {
            usunBloczek();
            currentBlockRow = new List<int>();
            currentBlockColumn = new List<int>();
            Rectangle square = null;
            if (!isRotated)
            {
                currentBlock = null;
                currentBlock = getVariableByString(array[bloczekNumber].ToString());

            }
            int firstDim = currentBlock.GetLength(0);
            int secondDim = currentBlock.GetLength(1);
            currentWidth = secondDim;
            currentHeight = firstDim;

            for (int row = 0; row < firstDim; row++)
            {
                

                for (int col = 0; col < secondDim; col++)
                {
                    int bit = currentBlock[rowCount, col];
                    if (bit == 1)
                    {
                        square = getBasicSquare(tabColor[bloczekNumber - 1]);
                        grid.Children.Add(square);
                        square.Name = "moving_" + Grid.GetRow(square) + "_" + Grid.GetColumn(square);
                        if (down >= grid.RowDefinitions.Count - currentHeight)
                        {
                            down = grid.RowDefinitions.Count - currentHeight;
                        }
                        Grid.SetRow(square, rowCount + down);
                        Grid.SetColumn(square, columnCount + left);
                        currentBlockRow.Add(rowCount + down);
                        currentBlockColumn.Add(columnCount + left);
                    }
                    columnCount++;
                }
                columnCount = 0;
                rowCount++;
            }
            columnCount = 0;
            rowCount = 0;
            if (!nextShapeDrawed)
            {
                drawNextShape(nextNumber);
            }
        }


        private void drawNextShape(int shapeNumber)
        {
            canvasNext.Children.Clear();
            int[,] nextShapeBlock = null;
            nextShapeBlock = getVariableByString(array[shapeNumber]);
            int firstDim = nextShapeBlock.GetLength(0);
            int secondDim = nextShapeBlock.GetLength(1);
            int x = 0;
            int y = 0;
            Rectangle square;
            for (int row = 0; row < firstDim; row++)
            {
                for (int column = 0; column < secondDim; column++)
                {
                    int bit = nextShapeBlock[row, column];
                    if (bit == 1)
                    {
                        square = getBasicSquare(tabColor[shapeNumber - 1]);                        
                        canvasNext.Children.Add(square);
                        Canvas.SetLeft(square, x);
                        Canvas.SetTop(square, y);
                    }
                    x += 25;
                }
                x = 0;
                y += 25;
            }
            nextShapeDrawed = true;

        }
        private void usunBloczek()
        {
            int iteruj = 0;
            while (iteruj < grid.Children.Count)
            {
                UIElement element = grid.Children[iteruj];
                if(element is Rectangle)
                {
                    Rectangle square = (Rectangle)element;
                    if (square.Name.IndexOf("moving_") == 0)
                    {
                        grid.Children.Remove(element);
                        iteruj = -1;
                    }
                }
                iteruj++;
            }
        }
        private bool rotationCollided(int rotations)
        {
            if (sprawdzKolidacje(0, currentWidth - 1))
            {
                return true;
            }
            else if (sprawdzKolidacje(0, -(currentWidth - 1)))
            {
                return true;

            }
            else if (sprawdzKolidacje(0, -1))
            {
                return true;
            }
            else if (sprawdzKolidacje(-1, currentWidth - 1))
            {
                return true;
            }
            else if (sprawdzKolidacje(1, currentWidth - 1))
            {
                return true;
            }
            return false;
        }



        private Rectangle getBasicSquare(Color rect)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Width = 25;
            rectangle.Height = 25;
            rectangle.StrokeThickness = 1;
            
            rectangle.Stroke = Brushes.White;
            rectangle.Fill = getGradientColor(rect);
            return rectangle;
        }
        private LinearGradientBrush getGradientColor(Color clr)
        {
            LinearGradientBrush gradientColor = new LinearGradientBrush();
            gradientColor.StartPoint = new Point(0, 0);
            gradientColor.EndPoint = new Point(1, 1.5);
            GradientStop black = new GradientStop();
            black.Color = Colors.Black;
            black.Offset = -1.5;
            gradientColor.GradientStops.Add(black);
            GradientStop other = new GradientStop();
            other.Color = clr;
            other.Offset = 0.70;
            gradientColor.GradientStops.Add(other);
            return gradientColor;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {

            if (isGameOver)
            {
                grid.Children.Clear();
                canvasNext.Children.Clear();
                gameoverText.Visibility = Visibility.Collapsed;
                isGameOver = false;
            }
            if (!timer.IsEnabled)
            {
                if (!gameActive) { score.Text = "0"; leftPos = 3; dodajBloczek(currentNumber, leftPos); }
                next.Visibility = level.Visibility = Visibility.Visible;
                level.Text = "Level: " + gameLevel.ToString();
                timer.Start();
                start.Content = "Stop Game";
                gameActive = true;
            }
            else
            {
                timer.Stop();
                start.Content = "Start Game";
            }

        }
        private int[,] getVariableByString(string variable)
        {
            return (int[,])this.GetType().GetField(variable).GetValue(this);
        }

        private void Start_Click_1(object sender, RoutedEventArgs e)
        {

        }

    }
}
