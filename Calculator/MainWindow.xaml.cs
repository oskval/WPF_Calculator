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
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double lastNumber;
        private double result;
        private SelectedOperator selectedOperator;
        public MainWindow()
        {
            InitializeComponent();


            AcButton.Click += AcButton_Click;
            NegativeButton.Click += NegativeButton_Click;
            PercentButton.Click += PercentButton_Click;
            EqualsButton.Click += EqualsButton_Click;
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;

            if (!double.TryParse(ResultLabel.Content.ToString(), out newNumber)) return;

            switch (selectedOperator)
            {
                case SelectedOperator.Addition:
                    result = SimpleMath.Add(lastNumber, newNumber);
                    break;
                case SelectedOperator.Subtraction:
                    result = SimpleMath.Subtract(lastNumber, newNumber);
                    break;
                case SelectedOperator.Division:
                    result = SimpleMath.Divide(lastNumber, newNumber);
                    break;
                case SelectedOperator.Multiplication:
                    result = SimpleMath.Multiply(lastNumber, newNumber);
                    break;
            }

            ResultLabel.Content = result.ToString();
        }

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;

            if (!double.TryParse(ResultLabel.Content.ToString(), out tempNumber)) return;
            tempNumber /= 100;

            if (lastNumber != 0)
            {
                tempNumber *= lastNumber;
            }

            ResultLabel.Content = tempNumber.ToString();
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(ResultLabel.Content.ToString(), out lastNumber)) return;
            lastNumber *= -1;
            ResultLabel.Content = lastNumber.ToString();
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            ResultLabel.Content = "0";
            result = 0;
            lastNumber = 0;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
             if (!double.TryParse(ResultLabel.Content.ToString(), out lastNumber)) return;
            ResultLabel.Content = "0";

            if (Equals(sender, AsterixButton))
                selectedOperator = SelectedOperator.Multiplication;
            if (Equals(sender, SlashButton))
                selectedOperator = SelectedOperator.Division;
            if (Equals(sender, MinusButton))
                selectedOperator = SelectedOperator.Subtraction;
            if (Equals(sender, PlusButton))
                selectedOperator = SelectedOperator.Addition;


        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = int.Parse((sender as Button).Content.ToString());


            if (ResultLabel.Content.ToString() == "0")
            {
                ResultLabel.Content = $"{selectedValue}";
            }
            else
            {
                ResultLabel.Content += $"{selectedValue}";
            }
        }

        private void CommaButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!ResultLabel.Content.ToString().Contains("."))
            {
                ResultLabel.Content = $"{ResultLabel.Content}.";
            }

        }
    }

    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public class SimpleMath
    {
        public static double Add(double a, double b)
        {
            return a + b;
        }

        public static double Subtract(double a, double b)
        {
            return a - b;
        }

        public static double Multiply(double a, double b)
        {
            return a * b;
        }

        public static double Divide(double a, double b)
        {
            if (b != 0) return a / b;


            MessageBox.Show("Division by zero is not supported", "Wrong operation", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return 0;

        }

    }
}
