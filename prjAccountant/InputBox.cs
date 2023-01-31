using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Accountant
{
    public class InputBox
    {

        Window Box = new Window();//window for the inputbox
        FontFamily font = new FontFamily("Tahoma");//font for the whole inputbox
        int FontSize = 30;//fontsize for the input
        StackPanel sp1 = new StackPanel();// items container
        string title = "InputBox";//title as heading
        string boxcontent;//title
        string defaulttext = "Enter the text...";//default textbox content
        string errormessage = "Text is not correct";//error messagebox content
        string errortitle = "Error";//error messagebox heading title
        string okbuttontext = "OK";//Ok button content
        string cancelbuttontext = "Cancel";//Cancel button content
        Brush BoxBackgroundColor = Brushes.GreenYellow;// Window Background
        Brush InputBackgroundColor = Brushes.Ivory;// Textbox Background
        bool clicked = false;
        TextBox input = new TextBox();
        Button ok = new Button();
        Button cancel = new Button();
        bool inputreset = false;
        public InputBox(string content)
        {
            try
            {
                boxcontent = content;
            }
            catch { boxcontent = "Error!"; }
            windowdef();
        }
        public InputBox(string content, string Htitle, string DefaultText)
        {
            try
            {
                boxcontent = content;
            }
            catch { boxcontent = "Error!"; }
            try
            {
                title = Htitle;
            }
            catch
            {
                title = "Error!";
            }
            try
            {
                defaulttext = DefaultText;
            }
            catch
            {
                DefaultText = "Error!";
            }
            windowdef();
        }
        public InputBox(string content, string Htitle, string Font, int Fontsize)
        {
            try
            {
                boxcontent = content;
            }
            catch { boxcontent = "Error!"; }
            try
            {
                font = new FontFamily(Font);
            }
            catch { font = new FontFamily("Tahoma"); }
            try
            {
                title = Htitle;
            }
            catch
            {
                title = "Error!";
            }
            if (Fontsize >= 1)
                FontSize = Fontsize;
            windowdef();
        }
        private void windowdef()// window building - check only for window size
        {
            Box.WindowStyle = WindowStyle.None;
            Box.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Box.Height = 150;// Box Height
            Box.Width = 300;// Box Width
            //Box.Background = BoxBackgroundColor;
            Box.Title = title;
            Box.Content = sp1;
            Box.Closing += Box_Closing;
            Box.MouseLeftButtonDown += Box_MouseLeftButtonDown;
            TextBlock content = new TextBlock();
            content.Margin = new Thickness(10, 10, 10, 10);
            content.TextWrapping = TextWrapping.Wrap;
            content.Background = null;
            content.HorizontalAlignment = HorizontalAlignment.Left;
            content.Text = boxcontent;
            //content.FontFamily = font;
            //content.FontSize = FontSize;
            sp1.Children.Add(content);

            //input.Background = InputBackgroundColor;
            //input.FontFamily = font;
            //input.FontSize = FontSize;
            input.HorizontalAlignment = HorizontalAlignment.Left;
            input.Margin = new Thickness(10, 10, 10, 10);
            input.Text = defaulttext;
            input.MinWidth = 265;
            input.MouseEnter += input_MouseDown;
            sp1.Children.Add(input);
            ok.Width = 70;
            ok.Height = 20;
            ok.Click += ok_Click;
            ok.Content = okbuttontext;
            ok.HorizontalAlignment = HorizontalAlignment.Left;
            ok.Margin = new Thickness(10, 0, 10, 10);

            cancel.Width = 70;
            cancel.Height = 20;
            cancel.Click += cancel_Click;
            cancel.Content = cancelbuttontext;
            cancel.HorizontalAlignment = HorizontalAlignment.Left;
            cancel.Margin = new Thickness(10, 0, 10, 10);

            sp1.Children.Add(ok);
            sp1.Children.Add(cancel);

        }
        void Box_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!clicked)
                e.Cancel = true;
        }
        private void input_MouseDown(object sender, MouseEventArgs e)
        {
            if ((sender as TextBox).Text == defaulttext && inputreset == false)
            {
                (sender as TextBox).Text = null;
                inputreset = true;
            }
        }
        void ok_Click(object sender, RoutedEventArgs e)
        {
            clicked = true;
            if (input.Text == defaulttext || input.Text == "")
                MessageBox.Show(errormessage, errortitle);
            else
            {
                Box.Close();
            }
            clicked = false;
        }

        void cancel_Click(object sender, RoutedEventArgs e)
        {
            clicked = true;
            Box.Close();            
        }

        void Box_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Box.DragMove();
        }

        public string ShowDialog()
        {
            Box.ShowDialog();
            return input.Text;
        }
    }
}
