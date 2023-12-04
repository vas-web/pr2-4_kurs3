using LibMas1;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Пример_таблицы_WPF;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private int[,] MasArray;
        private int DefM = 10, DefN = 10, DefOT = 0, DefDO = 0;
        private int M, N, OT, DO, temp;

        public void CreateTable_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxM.Text))
            {
                M = DefM;
                MessageBox.Show("кол-во строк не задано," +
                        " \nавтоматически присвоено x10",
                        "Практическая работа №2",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
            else
            {
                M = Convert.ToInt32((TextBoxM.Text));
                if (M < 0)
                {
                    MessageBox.Show("кол-во строк не может быть," +
                        " \nотрицательным " +
                        "\nзнак числа изменен на противоположенный",
                        "Практическая работа №2",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    M = M * -1;
                }
                else if (M == 0)
                {
                    M = DefM;
                    MessageBox.Show("кол-во строк не задано," +
                        " \nавтоматически присвоено x10",
                        "Практическая работа №2",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }

            if (string.IsNullOrEmpty(TextBoxN.Text))
            {
                N = DefN;
                MessageBox.Show("кол-во столбцов не задано," +
                        " \nавтоматически присвоено x10",
                        "Практическая работа №2",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
            else
            {
                N = Convert.ToInt32((TextBoxN.Text));
                if (N < 0)
                {
                    MessageBox.Show("кол-во столбцов не может быть," +
                        " \nотрицательным " +
                        "\nзнак числа изменен на противоположенный",
                        "Практическая работа №2",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    N = N * -1;
                }
                else if (N == 0)
                {
                    N = DefN;
                    MessageBox.Show("кол-во столбцов не задано," +
                        " \nавтоматически присвоено x10",
                        "Практическая работа №2",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }

            if (string.IsNullOrEmpty(TextBoxOt.Text) && string.IsNullOrEmpty(TextBoxDo.Text))
            {
                OT = DefOT;
                DO = DefDO;
                MessageBox.Show("Вы не указали значение диапазона" +
                        " \nгенерации автоматически присвоено 0",
                        "Практическая работа №2",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
            else
            {
                OT = Convert.ToInt32((TextBoxOt.Text));
                DO = Convert.ToInt32((TextBoxDo.Text));
                if (OT > DO)
                {
                    MessageBox.Show("ошибка указания диапазона",
                        "Практическая работа №2",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    temp = OT;
                    OT = DO;
                    DO = temp;
                }
            }

            MasArray = new int[M, N];
            ArrayHelper.AddArrayItems(MasArray, OT, DO);
            DataGrid1.ItemsSource = VisualArray.ToDataTable(MasArray).DefaultView;
        }

        public void DataGrid1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int indexColumn = e.Column.DisplayIndex;
            int indexRow = e.Row.GetIndex();
            if (String.IsNullOrEmpty(((TextBox)e.EditingElement).Text))
            {
                ((TextBox)e.EditingElement).Text = null;
            }
            else
            {
                MasArray[indexRow, indexColumn] = Convert.ToInt32(((TextBox)e.EditingElement).Text);
            }
        }

        public void SaveFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы (*.*) | *.* | Текстовые файлы | *.txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение таблицы";
            if (save.ShowDialog() == true)
            {
                StreamWriter file = new StreamWriter(save.FileName);
                file.WriteLine(MasArray.GetLength(0));
                file.WriteLine(MasArray.GetLength(1));
                for (int i = 0; i < MasArray.GetLength(0); i++)
                {
                    for (int j = 0; j < MasArray.GetLength(1); j++)
                    {
                        file.WriteLine(MasArray[i, j]);
                    }
                }
                file.Close();
            }
        }

        public void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".txt";
            open.Filter = "Все файлы (*.*) | *.* | Текстовые файлы | *.txt";
            open.FilterIndex = 2;
            open.Title = "Сохранение таблицы";
            if (open.ShowDialog() == true)
            {
                StreamReader file = new StreamReader(open.FileName);
                int Len = Convert.ToInt32(file.ReadLine());
                int Row = Convert.ToInt32(file.ReadLine());
                MasArray = new int[Len, Row];
                for (int i = 0; i < MasArray.GetLength(0); i++)
                {
                    for (int j = 0; j < MasArray.GetLength(1); j++)
                    {
                        MasArray[i, j] = Convert.ToInt32(file.ReadLine());
                    }
                }
                DataGrid1.ItemsSource = VisualArray.ToDataTable(MasArray).DefaultView;
                file.Close();
            }
        }

        public void GiveSizeMasArray_Click(object sender, RoutedEventArgs e)
        {
            if (MasArray != null)
            {
                int biba;
                int SizeM = MasArray.GetLength(0);
                int SizeN = MasArray.GetLength(1);
                SizeMasArray.Text = Convert.ToString(SizeM + "x" + SizeN);
                biba = SizeM * SizeN;
                CountItems.Text = Convert.ToString(biba + " - Элементов");
                SredAr.Text = Convert.ToString(ArrayHelper.CalculateSrAr(MasArray) + " - Среднее арифметическое");
            }
            else
            {
                MessageBox.Show("нечего измерять",
                        "Практическая работа №2",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
        }

        public void ClearMasArray_Click(object sender, RoutedEventArgs e)
        {
            DataGrid1.ItemsSource = null;
            MasArray = null;
        }

        public void Plus_Click(object sender, RoutedEventArgs e)
        {
            if (MasArray != null)
            {
                ArrayHelper.CalculatePlus(MasArray, out string[] StrMas, out int summa);
                TextBoxSum.Text = Convert.ToString(summa);
                String StrMasPlus = String.Empty;
                for (int i = 0; i < StrMas.Length; i++)
                {
                    StrMasPlus += StrMas[i] + " ";
                }
                TextBoxSumPlus.Text = StrMasPlus;
            }
            else
            {
                MessageBox.Show("нечего складывать",
                        "Практическая работа №2",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
        }

        public void Minus_Click(object sender, RoutedEventArgs e)
        {
            if (MasArray != null)
            {
                ArrayHelper.CalculateMinus(MasArray, out string[] StrMasMin, out int raznost);
                TextBoxMin.Text = Convert.ToString(raznost);
                String StrMasMinMin = String.Empty;
                for (int i = 0; i < StrMasMin.Length; i++)
                {
                    StrMasMinMin += StrMasMin[i] + " ";
                }
                TextBoxMinPlus.Text = StrMasMinMin;
            }
            else
            {
                MessageBox.Show("нечего  вычитать",
                        "Практическая работа №2",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
        }

        public void Umnozit_Click(object sender, RoutedEventArgs e)
        {
            if (MasArray != null)
            {
                ArrayHelper.CalculateUmn(MasArray, out string[] StrMasUmn, out int proizved);
                TextBoxUmn.Text = Convert.ToString(proizved);
                String StrMasUmnUmn = String.Empty;
                for (int i = 0; i < StrMasUmn.Length; i++)
                {
                    StrMasUmnUmn += StrMasUmn[i] + " ";
                }
                TextBoxUmnPlus.Text = StrMasUmnUmn;
            }
            else
            {
                MessageBox.Show("нечего  умножать",
                        "Практическая работа №2",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
        }

        private void FindMaxOfMinButton_Click(object sender, RoutedEventArgs e)
        {
            if (MasArray != null)
            {
                int maxOfMin = ArrayHelper.FindMaxOfMinInRows(MasArray);
                TextBoxFindMinOfMax.Text = Convert.ToString(maxOfMin + " -  максимальный среди минимальных элементов строк");
            }
            else
            {
                MessageBox.Show("нечего  искать",
                        "Практическая работа №2",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }
        }

        private void CalculateAll_Click(object sender, RoutedEventArgs e)
        {
            if (MasArray != null)
            {
                GiveSizeMasArray_Click(this, new RoutedEventArgs(Button.ClickEvent));
                Plus_Click(this, new RoutedEventArgs(Button.ClickEvent));
                Minus_Click(this, new RoutedEventArgs(Button.ClickEvent));
                Umnozit_Click(this, new RoutedEventArgs(Button.ClickEvent));
                FindMaxOfMinButton_Click(this, new RoutedEventArgs(Button.ClickEvent));
            }
            else
            {
                CreateTable_Click(this, new RoutedEventArgs(Button.ClickEvent));
                GiveSizeMasArray_Click(this, new RoutedEventArgs(Button.ClickEvent));
                Plus_Click(this, new RoutedEventArgs(Button.ClickEvent));
                Minus_Click(this, new RoutedEventArgs(Button.ClickEvent));
                Umnozit_Click(this, new RoutedEventArgs(Button.ClickEvent));
                FindMaxOfMinButton_Click(this, new RoutedEventArgs(Button.ClickEvent));
            }
        }
    }
}