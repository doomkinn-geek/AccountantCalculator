using Accountant;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace prjAccountant
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            string[] strs = sourceText.Text.Split('\n');
            Dictionary<string, int> counts = new Dictionary<string, int>();
            string key;
            int num, spaceIndex;
            bool noNameValue = false;//значение, как на рынке: много сумм, а в конце название
            int noNameRes = 0;

            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i].Trim() != "")
                {                  
                    spaceIndex = strs[i].IndexOf(" ");
                    if (spaceIndex == -1)
                    {
                        noNameValue = true;
                        try
                        {
                            num = Convert.ToInt16(strs[i]);
                        }
                        catch (Exception)
                        {
                            num = 0;
                        }
                        noNameRes += num;
                        continue;
                    }
                    num = Convert.ToInt16(strs[i].Substring(0, spaceIndex));
                    key = strs[i].Substring(spaceIndex, (strs[i].Length - (spaceIndex + 1)));
                    key = key.Trim();
                    key = key.ToLower();

                    if (num < 400)
                    {
                    }
                    if (!counts.ContainsKey(key))
                    {
                        counts.Add(key, 0);
                        counts[key] = num;
                        if (noNameValue)
                        {
                            counts[key] += noNameRes;
                            noNameValue = false;
                        }
                    }
                    else
                    {
                        counts[key] += num;
                    }
                }            
            }


            //Проверяем, не портится ли список, после его урезания
            /*int checkSum = 0;
            foreach (KeyValuePair<string, int> kvp in counts)
            {
                checkSum += kvp.Value;
            }*/

            List<string> toRemove = new List<string>();
            int otherAmount = 0;

            counts.Add("ПРОЧИЕ", 0);
            foreach (KeyValuePair<string, int> kvp in counts)
            {
                if (kvp.Value < 400)
                {
                    otherAmount += kvp.Value;
                    toRemove.Add(kvp.Key);
                }
            }

            foreach(string i in toRemove)
            {
                counts.Remove(i);
            }
            counts["ПРОЧИЕ"] = otherAmount;

            string res = "";

            /*res += "Контрольная сумма: ";
            res += "\n";
            res += checkSum;
            res += "\n";*/

            int fullAmount = 0;
            foreach(KeyValuePair<string, int> kvp in counts.OrderByDescending(kvp => kvp.Value))
            {
                res += kvp.Key;
                res += ":    ";
                res += kvp.Value;
                res += "\n";

                fullAmount += kvp.Value;
            }
            res += "\n\nОбщая сумма:  ";
            res += fullAmount;

            resultText.Text = res;
        }

        private void Window_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void saveResultBotton_Click(object sender, RoutedEventArgs e)
        {
            if (resultText.Text.Trim() == "") return;
            InputBox ii = new InputBox("Как назовем файл?");
            string fileName = ii.ShowDialog();
            if (fileName.Trim() == "") return;
            if(!Directory.Exists(Environment.CurrentDirectory + "\\reports\\"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\reports\\");
            }
            string FileName = Environment.CurrentDirectory + "\\reports\\" + fileName;



            StreamWriter s = new StreamWriter(FileName);
            s.Write(resultText.Text);
            s.Close();
        }
    }
}
