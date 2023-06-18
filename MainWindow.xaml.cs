using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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

namespace MoonCalc
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

        double Normalize(double value)
        {
            value = value - Math.Floor(value);
            if (value < 0) value = value + 1;
            return value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker.SelectedDate != null)
            {


                pic1.Visibility = Visibility.Hidden;
                pic2.Visibility = Visibility.Hidden;
                pic3.Visibility = Visibility.Hidden;
                pic4.Visibility = Visibility.Hidden;
                pic5.Visibility = Visibility.Hidden;
                pic6.Visibility = Visibility.Hidden;
                pic7.Visibility = Visibility.Hidden;
                pic8.Visibility = Visibility.Hidden;

                DateTime choosedDate = datePicker.SelectedDate.Value;

                int YY, MM;
                double K1, K2, K3, JD;

                YY = choosedDate.Year - ((12 - choosedDate.Month) / 10);
                MM = choosedDate.Month + 9;
                if (MM >= 12) MM = MM - 12;

                K1 = Math.Floor(365.25 * (YY + 4712));
                K2 = Math.Floor(30.6 * MM + 0.5);

                double rez = (YY / 100) + 49;

                K3 = Math.Floor(Math.Floor(rez) * 75) - 38;

                JD = K1 + K2 + choosedDate.Day + 59;
                if (JD > 2299160) JD = JD - K3;

                double IP = Normalize((JD - 2451550.1) / 29.530588853);
                double AG = IP * 29.53;

                if (AG < 1.84566) { moonPhaseTB.Text = "Moon phase: NEW"; pic1.Visibility = Visibility.Visible; }
                else if (AG < 5.53699) { moonPhaseTB.Text = "Moon phase: Waxing crescent"; pic2.Visibility = Visibility.Visible; }
                else if (AG < 9.22831) { moonPhaseTB.Text = "Moon phase: First quarter"; pic3.Visibility = Visibility.Visible; }
                else if (AG < 12.91963) { moonPhaseTB.Text = "Moon phase: Waxing gibbous"; pic4.Visibility = Visibility.Visible; }
                else if (AG < 16.61096) { moonPhaseTB.Text = "Moon phase: FULL"; pic5.Visibility = Visibility.Visible; }
                else if (AG < 20.30228) { moonPhaseTB.Text = "Moon phase: Waning gibbous"; pic6.Visibility = Visibility.Visible; }
                else if (AG < 23.99361) { moonPhaseTB.Text = "Moon phase: Last quarter"; pic7.Visibility = Visibility.Visible; }
                else if (AG < 27.68493) { moonPhaseTB.Text = "Moon phase: Waning crescent"; pic8.Visibility = Visibility.Visible; }
                else { moonPhaseTB.Text = "Moon phase: NEW"; pic1.Visibility = Visibility.Visible; }

                moonAgeDaysTB.Text = $"Moon age (days): {Math.Round(AG, 2)}";

                double perc = 16.61096 / AG * 100;
                if (perc >= 100) perc = 100 - (perc - 100);

                moonAgePercTB.Text = $"Moon age (percentage of full): {Math.Round(perc, 2)}";


                IP = IP * 2 * Math.PI;

                double DP = 2 * Math.PI * Normalize((JD - 2451562.2) / 27.55454988);
                double RP = Normalize((JD - 2451555.8) / 27.321582241);
                double LO = 360 * RP + 6.3 * Math.Sin(DP) + 1.3 * Math.Sin(2 * IP - DP) + 0.7 * Math.Sin(2 * IP);

                if (LO < 33.18) zodiacTB.Text = "Zodiac: Pisces";
                else if (LO < 51.16) zodiacTB.Text = "Zodiac: Aries";
                else if (LO < 93.44) zodiacTB.Text = "Zodiac: Taurus";
                else if (LO < 119.48) zodiacTB.Text = "Zodiac: Gemini";
                else if (LO < 135.30) zodiacTB.Text = "Zodiac: Cancer";
                else if (LO < 173.34) zodiacTB.Text = "Zodiac: Leo";
                else if (LO < 224.17) zodiacTB.Text = "Zodiac: Virgo";
                else if (LO < 242.57) zodiacTB.Text = "Zodiac: Libra";
                else if (LO < 271.26) zodiacTB.Text = "Zodiac: Scorpio";
                else if (LO < 302.49) zodiacTB.Text = "Zodiac: Sagittarius";
                else if (LO < 311.72) zodiacTB.Text = "Zodiac: Capricorn";
                else if (LO < 348.58) zodiacTB.Text = "Zodiac: Aquarius";
                else zodiacTB.Text = "Zodiac: Pisces";
            }            
        }
    }
}
