using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphicalWatch
{
    public partial class Form1 : Form
    {
        //Для "електронных часов"
        //Обьявляем переменные пока что присвоим им 0
        private Label labelElectricalWatch = null;
        private Label labelWithDate = null;
        private Label labelforWeather = null;
        private Timer timerForElectricalWatch = null;

        int HEIGHT_Group_Box = 200;
        

        //Для обычных(аналоговых часов )
        private Timer timerForAnalogWatch = null;
        private GroupBox grBox = null;
        //                         секундная стрелка минутная       часовая стрелка
        const int WIDTH = 310, HEIGHT = 310, secHAND = 140, minHAND = 110, hrHAND = 80;
        //                                        длинна стрелок
        //center
        int cx, cy;
        Bitmap bitMap= null;
        Graphics graphics;

        //Для погоды
        Weather weather;


        public Form1()
        {
            InitializeComponent();
            configForForm();
            initComponentsForElectricalWatch();
            initComponentsForAnalogWatch();
            initWeather();
            initStyleForWidgets();
            addWidgetsInForm();
        }

        private void configForForm() //Настройки для формы
        {   
            this.Text = "Graphical Watch";
            this.Height = 325;
            this.Width = 450;
            //Задний цвет
            this.BackColor = Color.White;
        }

        private void initWeather()
        {
            weather = new Weather();
        }

        private void initComponentsForElectricalWatch()
        {
            //Инициализируем бокс 
            grBox = new GroupBox();
            grBox.Location = new Point(0, 305);
            grBox.Height = 200;
            grBox.Width = 450;
            

            //Инициализируем таймер
            timerForElectricalWatch = new Timer();
            timerForElectricalWatch.Interval = 1000;
            timerForElectricalWatch.Start();
            
            //Инициализируем лейбл для время
            labelElectricalWatch = new Label(); 
            labelElectricalWatch.Location = new Point(HEIGHT_Group_Box / 2 - 25, 50); //устанавливаем позицию елемента
            labelElectricalWatch.BackColor = Color.Orange;
            labelElectricalWatch.Width = 160;
            labelElectricalWatch.Height = 40;

            //Лейбл для даты
            labelWithDate = new Label();
            labelWithDate.Location = new Point(HEIGHT_Group_Box / 2 - 25, 20);
            labelWithDate.Height = 30;
            labelWithDate.Width = 170;

            //Лейбл для погодЫ
            labelforWeather = new Label();
            labelforWeather.Location = new Point(0, 50);
            labelforWeather.Height = 40;
            labelforWeather.Width = 70;

            grBox.Controls.Add(labelElectricalWatch);
            grBox.Controls.Add(labelWithDate);
            grBox.Controls.Add(labelforWeather);
           
        }

        private void initComponentsForAnalogWatch()
        {
            //Создаем битмап
            bitMap = new Bitmap(WIDTH + 1, HEIGHT + 1);

            //центр
            cx = WIDTH / 2;
            cy = HEIGHT / 2;

            //инициализируем таймер
            timerForAnalogWatch = new Timer(); 
            timerForAnalogWatch.Interval = 1000; //в милисекундах
            timerForAnalogWatch.Start();                                    
        }

        private void initStyleForWidgets()
        {
            labelElectricalWatch.Font = new Font("Arial", 25, FontStyle.Bold); // устанавливаем шрифт елемента 
            labelWithDate.Font = new Font("Arial", 15, FontStyle.Bold);
            labelforWeather.Font = new Font("Arial", 15, FontStyle.Bold);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Обработчик кликов
            timerForElectricalWatch.Start();
            timerForElectricalWatch.Tick += new EventHandler(timerForElectricalWatch_Tick); // создаем событие 
            timerForAnalogWatch.Tick += new EventHandler(timerForAnalogWatch_Tick);
        }

        private void addWidgetsInForm()
        {
            this.Controls.Add(grBox);
        }

        public void timerForElectricalWatch_Tick(object sender,EventArgs e)
        {
            labelElectricalWatch.Text = DateTime.Now.ToString("T"); //T - это длинный формат времени
            labelWithDate.Text = DateTime.Now.ToString("D"); //D - дата 
            labelforWeather.Text = weather.getWeather().ToString() + "°";
        }

        public void timerForAnalogWatch_Tick(object sender, EventArgs e)
        {
            drawingAnalogWatch();
        }

        private void drawingAnalogWatch()
        {
            //Создаем графику
            graphics = Graphics.FromImage(bitMap);

            //Получаем время
            int seconds = DateTime.Now.Second; //Получаем секунды
            int minutes = DateTime.Now.Minute; //Минуты
            int hours = DateTime.Now.Hour;     //Часы

            //    координаты стрелок
            int[] handCoord = new int[2]; //Массив в который мы будем записывать результат функции которая считает положение стрелок
            //Очищаем поле для рисовки
            graphics.Clear(Color.White);

            //Рисуем круг
            graphics.DrawEllipse(new Pen(Color.Orange, 3f), 0, 0, WIDTH, HEIGHT);

            //Рисуем цифры
            graphics.DrawString("12", new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new PointF(138, 2));
            graphics.DrawString("1", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(218, 27));
            graphics.DrawString("2", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(270, 80));
            graphics.DrawString("3", new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new PointF(286, 140));
            graphics.DrawString("4", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(270, 214));
            graphics.DrawString("5", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(216, 264));
            graphics.DrawString("6", new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new PointF(146, 282));
            graphics.DrawString("7", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(78, 264));
            graphics.DrawString("8", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(28, 214));
            graphics.DrawString("9", new Font("Arial", 18, FontStyle.Bold), Brushes.Black, new PointF(0, 140));
            graphics.DrawString("10", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(27, 78));
            graphics.DrawString("11", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(75, 23));


            //Рисуем секундную стрелку
            handCoord = msCoords(seconds, secHAND); //Вызываем метод который вернет нам массив в котором находиться инфа
                                               //об расположении стрелок 
            graphics.DrawLine(new Pen(Color.Red, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //Рисум минутную 
            handCoord = msCoords(minutes, minHAND);
            graphics.DrawLine(new Pen(Color.Black, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //Рисум часовую
            handCoord = hrCoords(hours % 12, minutes, hrHAND);
            graphics.DrawLine(new Pen(Color.Gray, 5f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //Загружаем бит мап в picture Box
            pictureBox1.Image = bitMap;
        }

        //Координаты для часовой стрелки(минуты и секунды)
        //Функция для вычисления положения минутной и секндной стрелки
        private int[] msCoords(int value, int hlen)
        {
            int[] coord = new int[2];//массив который будет хранить данные об положении стрелок
            value *= 6; //Узнаем в какой мы половине часов 
            //value -----> Если 6 * на пол минуты у нас будет 180 , а это значит то - что мы будем находиться во 2 половине 


            if(value >= 0 &&  value <= 180) //Если мы находимся в этих пределах, то значит что мы в 1 половине 
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * value / 180));//Формула для положении стрелок в первой половине
                                                                              //по x и y
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * value / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * value / 180));//Формула для положении стрелок во второй половине часов
                                                                               //по x и y
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * value / 180));
            }

            return coord;
        }

        //Координаты для часовой стрелки
        //Функция для вычисления положения часовой стрелки
        //                         hour        minutes      длинна стрелки
        private int[] hrCoords(int hvalue,int mvalue ,  int hlen)
        {
            int[] coord = new int[2]; //массив который будет хранить данные об положении стрелок

            //Каждый час делает 30 градусов 
            //Каждая минута занимает 0.5 градусов
            int value = (int)((hvalue * 30) + mvalue * 0.5);

            if (value >= 0 && value <= 180) // если мы в 1 половине 
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * value / 180)); //Формула для положении стрелок по x и y
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * value / 180));
            }
            else //если во второй половине 
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * value / 180));//Формула для положении стрелок по x и y
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * value / 180));
            }

            return coord;
        }
    }
}
