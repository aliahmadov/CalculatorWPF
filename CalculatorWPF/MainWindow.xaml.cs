using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace CalculatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        public bool OperatorClicked { get; set; } = false;
        public double ResultValue { get; set; }

        public string OperationSign { get; set; }

        public bool isEqualCLicked { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (myLabel.Content == "0" || OperatorClicked || isEqualCLicked)
            {
                myLabel.Content = "";
            }

            var button = sender as Button;

            if (button.Content.ToString() == ".")
            {
                if (!myLabel.Content.ToString().Contains("."))
                {
                    myLabel.Content += ".";
                }
            }
            else
            {
                myLabel.Content += button.Content.ToString();
                OperatorClicked = false;
                isEqualCLicked = false;
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (!OperatorClicked)
            {
                if (ResultValue != 0 && !isEqualCLicked)
                {
                    equalbtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    if (button.Name == "mult")
                    {
                        OperationSign = "*";
                        upLabel.Content += myLabel.Content + "x";
                    }
                    else if (button.Name == "div")
                    {
                        OperationSign = "/";
                        upLabel.Content += myLabel.Content + "÷";
                    }
                    else
                    {
                        OperationSign = button.Content.ToString();
                        upLabel.Content += myLabel.Content + button.Content.ToString();
                    }
                    OperatorClicked = true;
                }
                else
                {
                    if (button.Name == "mult")
                    {
                        OperationSign = "*";
                        upLabel.Content += myLabel.Content + "x";
                        ResultValue = Convert.ToDouble(myLabel.Content, CultureInfo.InvariantCulture);
                    }
                    else if (button.Name == "div")
                    {
                        OperationSign = "/";
                        upLabel.Content += myLabel.Content + "÷";
                        ResultValue = Convert.ToDouble(myLabel.Content, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        OperationSign = button.Content.ToString();
                        upLabel.Content += myLabel.Content + button.Content.ToString();
                        ResultValue = Double.Parse(myLabel.Content.ToString(),CultureInfo.InvariantCulture);
                    }
                }
            }
            //  myLabel.Content = "";
            OperatorClicked = true;
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            myLabel.Content = "";
            upLabel.Content = "";
            ResultValue = 0;
        }

        private void Equal_Event(object sender, RoutedEventArgs e)
        {
            switch (OperationSign)
            {
                case "+":
                    myLabel.Content = (ResultValue + Convert.ToDouble(myLabel.Content)).ToString();
                    break;
                case "*":
                    myLabel.Content = (ResultValue * Convert.ToDouble(myLabel.Content)).ToString();
                    break;
                case "-":
                    myLabel.Content = (ResultValue - Convert.ToDouble(myLabel.Content)).ToString();
                    break;
                case "/":
                    myLabel.Content = (ResultValue / Convert.ToDouble(myLabel.Content)).ToString();
                    break;
                case "%":
                    myLabel.Content = ((ResultValue/100) * Convert.ToDouble(myLabel.Content)).ToString();
                    break;
                default:
                    break;
            }
            ResultValue = Convert.ToDouble(myLabel.Content);
            upLabel.Content = "";
            isEqualCLicked = true;
        }

        private void Exit_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
