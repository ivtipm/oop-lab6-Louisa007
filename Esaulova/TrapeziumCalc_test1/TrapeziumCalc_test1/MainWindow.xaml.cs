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

namespace TrapeziumCalc_test1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double A, B, C, D, maxSide, S, P, m, h, d1, d2, cornA, cornB, cornC, cornD;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
            textBoxSide_A.Text = textBoxSide_B.Text = textBoxSide_C.Text = textBoxSide_D.Text = " ";

            label_TrapeziumType.Content = "Тип трапеции: ";
            labelProperty_P.Content = "Периметр трапеции P: ";
            labelProperty_m.Content = "Средняя линия трапеции m: ";
            labelProperty_S.Content = "Площадь трапеции S: ";
            labelProperty_h.Content = "Высота трапеции h: ";
            labelProperty_d1.Content = "Диагональ трапеции d1: ";
            labelProperty_d2.Content = "Диагональ трапеции d2: ";
            labelProperty_CornA.Content = "Угол α: ";
            labelProperty_CornB.Content = "Угол β: ";
            labelProperty_CornD.Content = "Угол δ: ";
            labelProperty_CornC.Content = "Угол γ: ";
        }

        private void ButtonResult_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
            try //Если при выполнении "try" возникнет ошибка то случится "вылет" на catch
            {
                A = Convert.ToDouble(textBoxSide_A.Text); //забираем числа из текстбоксов в переменные
                B = Convert.ToDouble(textBoxSide_B.Text);
                C = Convert.ToDouble(textBoxSide_C.Text);
                D = Convert.ToDouble(textBoxSide_D.Text);

                if (IsSidesOK() == false) //если четырёхугольника с такими сторонами нет, то ничего не считаем
                {
                    label_TrapeziumType.Content = "Трапеции не существет";
                }
                else  //если есть, то считаем
                {
                    label_TrapeziumType.Content = TypeOfTrapezium();
                    labelProperty_P.Content = "Периметр трапеции P: " + CalcP().ToString("0.000");
                    labelProperty_m.Content = "Средняя линия трапеции m: " + CalcM().ToString("0.000");
                    labelProperty_S.Content = "Площадь трапеции S: " + CalcS().ToString("0.000");
                    labelProperty_h.Content = "Высота трапеции h: " + CalcH().ToString("0.000");
                    labelProperty_d1.Content = "Диагональ трапеции d1: " + CalcD1().ToString("0.000");
                    labelProperty_d2.Content = "Диагональ трапеции d2: " + CalcD2().ToString("0.000");
                    labelProperty_CornA.Content = "Угол α: " + CalcCornA().ToString("0.000");
                    labelProperty_CornB.Content = "Угол β: " + CalcCornB().ToString("0.000");
                    labelProperty_CornD.Content = "Угол δ: " + CalcCornD().ToString("0.000");
                    labelProperty_CornC.Content = "Угол γ: " + CalcCornC().ToString("0.000");
                }
            }
            catch(Exception ex)
            {
                label_TrapeziumType.Content = ex.Message;
            }
        }

        public double CalcH() //посчитать высоту
        {
            /*
            double part1 = Math.Pow((D - B), 2) + Math.Pow(A, 2) - Math.Pow(C, 2);
            double part2 = (2 * (D - B));
            double part3 = Math.Pow((part1 / part2), 2);
            double part4 = Math.Pow(C, 2) - part3;
            h = Math.Sqrt(part4);*/

            h = S / m;

            return h;
        }

        public double CalcS() //посчитать площадь
        {
            //S = ((B + D) * h) / 2;

            //Чтобы удобнее проверять правильность вычислиний, формула разбита на части
            double part1 = Math.Pow((B - D), 2) + Math.Pow(A, 2) - Math.Pow(C, 2);
            double part2 = (2 * (B - D));
            double part3 = Math.Pow((part1 / part2), 2);
            double part4 = Math.Pow(A, 2) - part3;
            S = m * Math.Sqrt(part4);

            return S;
        }

        public double CalcM() //посчитать среднюю линию
        {
            m = (B + D) / 2;
            return m;
        }

        public double CalcP() //посчитать периметр
        {
            P = A + B + C + D;
            return P;
        }

        public double CalcD1() //посчитать диагональ1
        {
            double part1 = D * (Math.Pow(C, 2) - Math.Pow(A, 2));
            double part2 = (D - B);
            double part3 = Math.Pow(C, 2) + (D * B) - (part1 / part2);
            d1 = Math.Sqrt(part3);
            return d1;
        }

        public double CalcD2() //посчитать диагональ2
        {
            double part1 = D * (Math.Pow(A, 2) - Math.Pow(C, 2));
            double part2 = (D - B);
            double part3 = Math.Pow(A, 2) + (D * B) - (part1 / part2);
            d2 = Math.Sqrt(part3);
            return d2;
        }

        public double CalcCornA() //посчитать угол1
        {
            cornA = 90 - Math.Acos(h / A) * (180 / Math.PI);            
            return cornA;
        }

        public double CalcCornB() //посчитать угол2
        {
            cornB = 180 - cornA;
            return cornB;
        }

        public double CalcCornD() //посчитать угол3
        {
            cornD = 90 - Math.Acos(h / C) * (180 / Math.PI);
            return cornD;
        }

        public double CalcCornC() //посчитать угол4
        {
            cornC = 180 - cornD;
            return cornC;
        }


        public void ResetAll() //Сбросить все переменные
        {
            A = B = C = D = maxSide = S = P = m = h = d1 = d2 = cornA = cornB = cornC = cornD = 0;
        }

        public string TypeOfTrapezium() //определение типа трапеции (Alpha Version)
        {
            if(A==C)
            {
                return "Равнобедренная трапеция";
            }
            else
                return "Разносторонняя трапеция";

            //if (A != B || A != D)
            //{
            //    return "Разносторонняя трапеция";
            //}
        }

        public bool IsSidesOK() //Проверка на существование четырёхугольника (Alpha Version)
        {
            maxSide = A;
            if (maxSide < B) maxSide = B;
            if (maxSide < C) maxSide = C;
            if (maxSide < D) maxSide = D;

            if (maxSide == A)
            {
                if (maxSide < (B + C + D))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            if (maxSide == B)
            {
                if (maxSide < (A + C + D))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            if (maxSide == C)
            {
                if (maxSide < (A + B + D))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            if (maxSide == D)
            {
                if (maxSide < (A + B + C))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }
    }
}
