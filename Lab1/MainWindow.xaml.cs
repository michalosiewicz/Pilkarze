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
using System.IO;
using System.Globalization;

namespace Lab1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string plik = "repo.txt";
        public MainWindow()
        {
            InitializeComponent();
            ComboBoxLang.Text = CultureInfo.CurrentCulture.Name;
        }

        private void Czysc_formularz()
        {
            Text_imie.Text = "";
            Text_nazwisko.Text = "";
            wiek.Value = 15;
            Slider_waga.Value = 50;
            Text_wzrost.Text = "";
            Kluby.Text = "";
        }
        private void Slider_waga_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Text_waga.Text = Slider_waga.Value.ToString()+" kg";
        }

        private void wiek_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            string lat;
            if (CultureInfo.CurrentCulture.Name == "pl-PL")
            {
                lat = "lat";
                var x = wiek.Value % 10;
                if (x == 2 || x == 3 || x == 4)
                    lat = "lata";
            }
            else if (CultureInfo.CurrentCulture.Name == "de-DE")
            {
                lat = "Jahre";
            }
            else
            {
                lat = "years";
            }
            Text_wiek.Text = wiek.Value.ToString() + " " + lat;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            int wzrost;
            if(int.TryParse(Text_wzrost.Text,out wzrost))
            {
                if (Text_imie.Text != "" && Text_nazwisko.Text != ""&& Kluby.Text!="")
                {
                    Pilkarz p = new Pilkarz(Text_imie.Text, Text_nazwisko.Text,(int)wiek.Value, 
                        (int)Slider_waga.Value, wzrost, Kluby.Text);

                    bool nalezy = false;

                    foreach(var x in Lista_pilkarzy.Items)
                    {
                        if (p.Equals(x))
                        {
                            nalezy = true;
                            break;
                        }
                    }
                    if (nalezy == false)
                    {
                        Lista_pilkarzy.Items.Add(p);
                        Czysc_formularz();
                    }
                    else
                        MessageBox.Show("Podany piłkarz już istnieje na liście");
                }
                else
                    MessageBox.Show("Niepoprawne dane");
            }
            else
            {
                MessageBox.Show("Niepoprawne dane");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(plik))
            {
                var linie = File.ReadAllLines(plik);
                foreach (var linia in linie)
                {
                    string[] atrybuty = linia.Split(',');
                    string[] name = atrybuty[0].Split(' ');
                    string[] wiek = atrybuty[2].Split(' ');
                    string[] waga = atrybuty[3].Split(' ');
                    string[] wzrost = atrybuty[4].Split(' ');

                    Pilkarz p = new Pilkarz(name[0].Trim(), name[1].Trim(), int.Parse(wiek[1]),
                        int.Parse(waga[1]), int.Parse(wzrost[1]),atrybuty[1].Trim());
                    Lista_pilkarzy.Items.Add(p);

                }
            }
           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (File.Exists(plik))
                File.Delete(plik);
            foreach(var x in Lista_pilkarzy.Items)
            {
                File.AppendAllText(plik, x.ToString()+Environment.NewLine);
            }
        }

        private void Lista_pilkarzy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Lista_pilkarzy.SelectedIndex > -1)
            {
                var p = (Pilkarz)Lista_pilkarzy.SelectedItem;
                Text_imie.Text = p.Imie;
                Text_nazwisko.Text = p.Nazwisko;
                wiek.Value = p.Wiek;
                Slider_waga.Value = p.Waga;
                Text_wzrost.Text = p.Wzrost.ToString();
                Kluby.Text = p.Klub;
            }

        }

        private void button_edytuj_Click(object sender, RoutedEventArgs e)
        {
            if (Lista_pilkarzy.SelectedIndex > -1)
            {
                int wzrost;
                if (int.TryParse(Text_wzrost.Text, out wzrost))
                {
                    if (Text_imie.Text != "" && Text_nazwisko.Text != "" && Kluby.Text != "")
                    {
                        Pilkarz p = new Pilkarz(Text_imie.Text,Text_nazwisko.Text, (int)wiek.Value, 
                            (int)Slider_waga.Value, wzrost, Kluby.Text);
                        bool nalezy = false;

                        foreach (var x in Lista_pilkarzy.Items)
                        {
                            if (p.Equals(x))
                            {
                                nalezy = true;
                                break;
                            }
                        }
                        if (nalezy == false)
                        {
                            Lista_pilkarzy.Items[Lista_pilkarzy.SelectedIndex] = p;
                            Czysc_formularz();
                        }
                        else
                            MessageBox.Show("Podany piłkarz już istnieje na liście");
                    }
                    else
                        MessageBox.Show("Niepoprawne dane");

                }
                else
                    MessageBox.Show("Niepoprawne dane");
            }
        }

        private void button_usun_Click(object sender, RoutedEventArgs e)
        {
            if (Lista_pilkarzy.SelectedIndex > -1)
            {
                var wynik = MessageBox.Show($"Czy na pewno chcesz usunąć {Lista_pilkarzy.SelectedItem}?", "Uwaga!!!",
                MessageBoxButton.YesNoCancel);
                if (wynik == MessageBoxResult.Yes)
                {
                    Lista_pilkarzy.Items.Remove(Lista_pilkarzy.Items[Lista_pilkarzy.SelectedIndex]);
                    Czysc_formularz();
                }
                if (wynik == MessageBoxResult.No)
                {
                    Czysc_formularz();
                }
            }
        }

        private void ButtonLanguage_Click(object sender, RoutedEventArgs e)
        {
            App.ChangeLanguage(ComboBoxLang.Text);
        }
    }
}
