using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessObjects;

namespace Demo
{
    /// Author: Stefan Hadzhiev
    /// ==================================================================
    /// Description: Form to serve as presantation layer alowing to manipulate data using Customer and MailingList classes,
    /// while doing additional checks and validation for the properties of the Customer class
    /// ==================================================================
    /// Date last modified: 01/11/2018
    public partial class MainWindow : Window
    {
        private MailingList store = new MailingList();
        private int countID = 10001;

        public MainWindow()
        {
            InitializeComponent();
            //Hiding the label and the textbox that display the preferred contact
            txtBoxPref.Visibility = Visibility.Hidden;
            lblPrefContactDisplay.Visibility = Visibility.Hidden;
        }

        private void txtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtSurname_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtMail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtSkype_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtTelephone_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtCustomerID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer selectedCust = (Customer)listBox1.SelectedItem;
            try
            {
                //If selected item in the listbox exists, displaying the properties to the corresponding TextBoxes 
                if (listBox1.SelectedItem != null)
                {
                    txtFirstName.Text = selectedCust.FirstName;
                    txtSurname.Text = selectedCust.Surname;
                    txtMail.Text = selectedCust.Mail;
                    txtSkype.Text = selectedCust.Skype;
                    txtTelephone.Text = selectedCust.Telephone;
                    string getPref = selectedCust.GetPreferredContact();
                    comboBox1.SelectedIndex = -1;
                    txtBoxPref.Text = getPref;
                    txtBoxPref.Visibility = Visibility.Visible;
                    lblPrefContactDisplay.Visibility = Visibility.Visible;
                }
            }
            catch(Exception exc0)
            {
                MessageBox.Show(exc0.Message);
            }
        }

        //Adding customer objects to the database and doing additional checks for some of the properties
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Customer cust = new Customer();
            try
            {
                cust.ID = countID;
                //Validating that the first name and surname contain only latters using the method IsOnlyLetters();
                if (IsOnlyLetters(txtFirstName.Text) && IsOnlyLetters(txtSurname.Text))
                {
                    cust.FirstName = txtFirstName.Text;
                    cust.Surname = txtSurname.Text;
                }
                else
                {
                   throw new Exception("First and last name should not contain numbers or special characters");
                }

                //Validating if there is entered customer e-email should contain the '@' symbol
                if (txtMail.Text.Contains('@') || txtMail.Text.Length == 0)
                {
                    cust.Mail = txtMail.Text;
                }
                else
                {
                throw new Exception("Please enter valid e-mail address");
                }

                cust.Skype = txtSkype.Text;

                //Validating if the customer telephone contains numbers only by trying to parse it
                if (int.TryParse(txtTelephone.Text, out int testedInt))
                {
                    cust.Telephone = txtTelephone.Text;
                }
                else
                {
                    throw new Exception("Please enter valid telephone");
                }
                
                //Validating that the textbox for skype is not empty and the selected index is skype
                if (comboBox1.SelectedIndex == 1 && txtSkype.Text.Length != 0)
                {
                    cust.PrefContact = txtSkype.Text;
                }
                //Validating that the textbox for mail is not empty and the selected index is mail
                else if (comboBox1.SelectedIndex == 2 && txtMail.Text.Length != 0)
                {
                    cust.PrefContact = txtMail.Text;
                }
                //Validating that the textbox for telephone is not empty and the selected index is telephone
                else if (comboBox1.SelectedIndex == 0 && txtTelephone.Text.Length != 0)
                {
                    cust.PrefContact = txtTelephone.Text;
                }
                else
                {
                    throw new Exception("Please select preferred contact");
                }

                //Checking if customer is already stored in the database, if not adding it and incremeting the IDcounter
                if (!store.CustomerExists(cust))
                {
                    store.add(cust);
                    listBox1.Items.Add(cust);
                    countID++;
                    ClearAllText();
                }
                else
                {
                    throw new Exception("Customer already exists");
                }
                
               
            }
            catch(Exception exc1)
            {
                    MessageBox.Show(exc1.Message);
            }
         
        }


        //Finding customer object in the database
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Checking if customer object is in the database and displaying the properties to the corresponding TextBoxes
                if (int.TryParse(txtCustomerID.Text, out int findID) && store.find(findID) != null)
                {
                    var displayDetails = store.find(findID);
                    txtFirstName.Text = displayDetails.FirstName;
                    txtSurname.Text = displayDetails.Surname;
                    txtMail.Text = displayDetails.Mail;
                    txtSkype.Text = displayDetails.Skype;
                    txtTelephone.Text = displayDetails.Telephone;
                    string getPreferred = displayDetails.GetPreferredContact();
                    comboBox1.SelectedIndex = -1;
                    txtBoxPref.Text = getPreferred;
                }
                else
                {
                   throw new Exception("Entered ID is missing or not valid");
                }
            }
            catch(Exception exc2)
            {
                MessageBox.Show(exc2.Message);
            }

        }

        //Deleting customer object from the database
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(txtCustomerID.Text, out int delID))
                {
                    //Checking if customer object with the same ID exists in the database
                    if (store.find(delID) == null)
                    {
                        MessageBox.Show("Customer ID does not exist");
                    }
                    else
                    {
                        //Deleting the Customer object from the store database and the listbox
                        store.delete(delID);
                        for (int i = listBox1.Items.Count - 1; i >= 0; i--)
                        {
                            if (listBox1.Items[i].ToString().Contains(delID.ToString()))
                            {
                                listBox1.Items.RemoveAt(i);
                                listBox1.UpdateLayout();
                                ClearAllText();
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Entered ID is missing or not valid");
                }
            }
            catch(Exception exc3)
            {
                MessageBox.Show(exc3.Message);
            }
            
        }

        private void btnListAll_Click(object sender, RoutedEventArgs e)
        {
            //opening instance of Window1 and passing a listbox to display the objects
            Window1 winOne = new Window1(listBox1);
            winOne.Show();
        }

        //Method returning True if string contains only latters
        private bool IsOnlyLetters(string strToCheck)
        {
            foreach (char c in strToCheck)
                if (!Char.IsLetter(c))
                {
                    return false;
                }
            return true;
        }

        private void txtBoxPref_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //Method to delete the text of all TextBoxes, set the combobox index to -1, hide the label and the TextBox used to display preferred contact 
        private void ClearAllText()
        {
            txtFirstName.Clear();
            txtSurname.Clear();
            txtMail.Clear();
            txtSkype.Clear();
            txtTelephone.Clear();
            txtCustomerID.Clear();
            txtBoxPref.Clear();
            comboBox1.SelectedIndex = -1;
            txtBoxPref.Visibility = Visibility.Hidden;
            lblPrefContactDisplay.Visibility = Visibility.Hidden;
        }
    }
}
