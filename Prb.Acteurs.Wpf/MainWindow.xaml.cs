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
using Prb.Acteurs.Core;

namespace Prb.Acteurs.Wpf
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
        List<Actor> actors;
        bool isNew;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            actors = new List<Actor>();
            DoSeeding();
            PopulateListbox();
            ClearControls();
            grpActors.IsEnabled = true;
            grpData.IsEnabled = false;
        }
        private void DoSeeding()
        {
            actors.Add(new Actor("Jackson", "Samuel", Actor.Gender.Male, 1948, "Washington (USA)", "USA"));
            actors.Add(new Actor("Willis", "Bruce", Actor.Gender.Male, 1955, "Idar-Oberstein (DE)", "USA"));
            actors.Add(new Actor("Pitt", "Brad", Actor.Gender.Male, 1963, "Shwawnee (USA)", "USA"));
            actors.Add(new Actor("Neeson", "Liam", Actor.Gender.Male, 1952, "Ballymena (UK)", "UK"));
            actors.Add(new Actor("Lewis", "Damian", Actor.Gender.Male, 1971, "London (UK)", "UK"));
            actors.Add(new Actor("Cumberbatch", "Benedict", Actor.Gender.Male, 1976, "London (UK)", "UK"));
            actors.Add(new Actor("Jolie", "Angelina", Actor.Gender.Female, 1975, "Los Angeles (USA)", "USA"));
            actors.Add(new Actor("Cafmeyer", "Maaike ", Actor.Gender.Female, 1973, "Torhout (BE)", "BE"));
        }
        private void PopulateListbox()
        {
            actors = actors.OrderBy(o => o.lastName).ThenBy(o => o.firstName).ToList();
            lstActors.ItemsSource = null;
            lstActors.ItemsSource = actors;

            ClearControls();
            lstActors.SelectedIndex = -1;
        }
        private void ClearControls()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtNationality.Text = "";
            txtPlaceOfBirth.Text = "";
            txtYearOfBirth.Text = "";
            rdbFemale.IsChecked = false;
            rdbMale.IsChecked = false;
        }

        private void lstActors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if(lstActors.SelectedItem == null)
            {
                return;
            }
            Actor actor = (Actor)lstActors.SelectedItem;
            txtFirstName.Text = actor.firstName;
            txtLastName.Text = actor.lastName;
            txtNationality.Text = actor.nationality;
            txtPlaceOfBirth.Text = actor.placeOfBirth;
            txtYearOfBirth.Text = actor.yearOfBirth.ToString();
            if (actor.gender == Actor.Gender.Male)
                rdbMale.IsChecked = true;
            else
                rdbFemale.IsChecked = true;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            isNew = true;
            ClearControls();
            grpActors.IsEnabled = false;
            grpData.IsEnabled = true;
            txtLastName.Focus();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(lstActors.SelectedItem == null)
            {
                return;
            }    
            isNew = false;
            grpActors.IsEnabled = false;
            grpData.IsEnabled = true;
            txtLastName.Focus();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstActors.SelectedItem == null)
            {
                return;
            }
            Actor actor = (Actor)lstActors.SelectedItem;
            actors.Remove(actor);
            PopulateListbox();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            Actor.Gender gender = Actor.Gender.Male;
            if (rdbFemale.IsChecked == true)
            {
                gender = Actor.Gender.Female;
            }
            int yearOfBirth;
            int.TryParse(txtYearOfBirth.Text, out yearOfBirth);
            string placeOfBirth = txtPlaceOfBirth.Text.Trim();
            string nationality = txtNationality.Text.Trim(); ;

            if (lastName.Length == 0)
            {
                MessageBox.Show("Waarde invoeren", "Naam", MessageBoxButton.OK, MessageBoxImage.Error);
                txtLastName.Focus();
                return;
            }
            if (firstName.Length == 0)
            {
                MessageBox.Show("Waarde invoeren", "Voornaam", MessageBoxButton.OK, MessageBoxImage.Error);
                txtFirstName.Focus();
                return;
            }
            if(rdbMale.IsChecked == false && rdbFemale.IsChecked == false)            
            {
                MessageBox.Show("Waarde selecteren", "Geslacht", MessageBoxButton.OK, MessageBoxImage.Error);
                rdbMale.Focus();
                return;
            }
            if(yearOfBirth == 0)
            {
                MessageBox.Show("Waarde invoeren", "Geboortejaar", MessageBoxButton.OK, MessageBoxImage.Error);
                txtYearOfBirth.Focus();
                return;
            }

            Actor actor;
            if(isNew)
            {
                actor = new Actor(lastName, firstName, gender, yearOfBirth, placeOfBirth, nationality);
                actors.Add(actor);
            }
            else
            {
                actor = (Actor)lstActors.SelectedItem;
                actor.firstName = firstName;
                actor.lastName = lastName;
                actor.gender = gender;
                actor.yearOfBirth = yearOfBirth;
                actor.placeOfBirth = placeOfBirth;
                actor.nationality = nationality;
            }

            grpActors.IsEnabled = true;
            grpData.IsEnabled = false;

            PopulateListbox();
            lstActors.SelectedItem = actor;
            lstActors_SelectionChanged(null, null);

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            grpActors.IsEnabled = true;
            grpData.IsEnabled = false;
            lstActors_SelectionChanged(null, null);
        }
    }
}
