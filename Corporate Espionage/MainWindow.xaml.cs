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
using Microsoft.Win32;

namespace Corporate_Espionage {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        Ranking ranking = new Ranking();
        string path = "D:\\temp\\ranked_words.txt";


        public MainWindow() {
            InitializeComponent();
            if(File.Exists(path)) {
                ranking.ImportFile(path);
                BtnCompute.IsEnabled = true;
                LblErrorBar.Background = Brushes.Transparent;
                LblErrorBar.Content = "";
            } else {
                LblErrorBar.Background = Brushes.Red;
                LblErrorBar.Foreground = Brushes.White;
                LblErrorBar.Content = "Error: Check file not found. Open file location and attach file.";
                BtnCompute.IsEnabled = false;
            }//end if
            TxtToRank.Focus();
        }//end main event

        private void BtnCompute_Click(object sender, RoutedEventArgs e) {
            ranking.NumRanked = 0;
            ranking.PercentRank = 0;
            LstUnique.Items.Clear();
            ranking.ConvertArray(TxtToRank.Text);
            CheckRank();
            TxtToRank.Focus();
        }//end button_click event

        private void BtnClearAll(object sender, RoutedEventArgs e) {
            LstRanked.Items.Clear();
            TxtToRank.Clear();
            LstUnique.Items.Clear();
            TxtToRank.Focus();
        }//end Button_ClearAll event

        private void Open_Click(object sender, RoutedEventArgs e) {
            path = LaunchSingleFileDialog();
            if(File.Exists(path)) {
                ranking.ImportFile(path);
                BtnCompute.IsEnabled = true;
                LblErrorBar.Background = Brushes.Transparent;
                LblErrorBar.Content = "";
            } else {
                LblErrorBar.Background = Brushes.Red;
                LblErrorBar.Foreground = Brushes.White;
                LblErrorBar.Content = "Error: Check file not found. Open file location and attach file.";
                BtnCompute.IsEnabled = false;
            }//end if
        }//end OpenClick event

        //METHODS
        public void CheckRank() {
            foreach(string word in ranking.StoredAry) {
                if(HasWord(word)) {
                    GetRank(word);
                }//end if
            }//end foreach
            Updater();
        }//end method

        private void Updater() {
            LstRanked.Items.Clear();
            LstRanked.Items.Add($"Number Ranked = {ranking.NumRanked}");
            if(TxtToRank.Text != "") {
                LstRanked.Items.Add($"Percent Ranked = {(Convert.ToDouble(ranking.NumRanked) / ranking.AllArray.Length) * 100}%");;
            } else {
                LstRanked.Items.Add($"Percent Ranked = 0%");
            }//end if
        }//end method

        private void GetRank(string word) {
            //LstUnique.Items.Add($"{word} {ranking.RankCount}");
            LstUnique.Items.Add($"{word}");
            LstUnique.Items.Add($"{ranking.RankCount}");
        }//end method

        private bool HasWord(string word) {
            ranking.RankCount = 1;
            foreach(string rank in ranking.RankedAry) {
                if(rank == word) {
                    ranking.NumRanked++;
                    return true;
                }//end if
                ranking.RankCount++;
            }//end foreach

            return false;
        }//end method

        private string LaunchSingleFileDialog() {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog ofd_temp = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            ofd_temp.DefaultExt = ".txt";
            ofd_temp.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = ofd_temp.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if(result == true) {
                return ofd_temp.FileName;
            }//end if

            return "C:\\temp\\ranked_words.txt";
        }//end method;
    }//end class
}//end namespace
