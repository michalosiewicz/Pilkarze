using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Pilkarz
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Wiek { get; set; }
        public int Waga { get; set; }
        public int Wzrost { get; set; }
        public string Klub { get; set; }

        public Pilkarz(string imie,string nazwisko,int wiek,int waga,int wzrost,string klub)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Wiek = wiek;
            Waga = waga;
            Wzrost = wzrost;
            Klub = klub;
        }
        public override string ToString()
        {
            string lat;
            if (CultureInfo.CurrentCulture.Name == "pl-PL")
            {
                lat = "lat";
                var x = Wiek % 10;
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
            return $"{Imie} {Nazwisko}, {Klub}, {Wiek} {lat}, {Waga} kg, {Wzrost} cm";
        }
        public override bool Equals(object obj)
        {
            var p = obj as Pilkarz;
            if (p == null || p.Imie != this.Imie || p.Nazwisko != this.Nazwisko||p.Wzrost!=this.Wzrost||
                p.Wiek!=this.Wiek||p.Waga!=this.Waga||p.Klub!=this.Klub)
                return false;
            else
                return true;
        }
    }
}
