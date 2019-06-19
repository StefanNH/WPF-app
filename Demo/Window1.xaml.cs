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
using System.Windows.Shapes;
using BusinessObjects;

namespace Demo
{
    /// Author: Stefan Hadzhiev
    /// ==========================================
    /// Description: Form to display information in a listbox
    /// ==========================================
    /// Date last modified: 01/11/2018
    public partial class Window1 : Window
    {
        //public listbox to be used as a pointer to the listbox in the main window form
        public ListBox lb;

        public Window1(ListBox listBox1)
        {
            InitializeComponent(); 
            lb = listBox1;
            string getPref;
            //Looping through each customer objects and using String.Format to display them aligned from the left side in the listbox lb;
            foreach (Customer c in lb.Items)
            {
                getPref = c.GetPreferredContact();
                listBox2.Items.Add(String.Format("ID:{0,-5} First name: {1,-10} Surname: {2,-10} Skype: {3,-10} E-mail: {4,-10} Telephone: {5,-10} Preferred contact {6,-10}",
                c.ID, c.FirstName, c.Surname, c.Skype, c.Mail, c.Telephone, getPref));
            }
        }

        //Closing the current form
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void listBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
