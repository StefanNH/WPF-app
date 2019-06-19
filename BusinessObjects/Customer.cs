using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    /*  Author: Stefan Hadzhiev
     *  ============================================================================================================================
     *  Description: Customer class to allow for saving and manipulating customer details(aka ID,names, mail, skype, tel. number, preferred contact),
     *  Validated properties are: ID must be between 10001 and 50000, names and telephone must not be empty, 
     *  and if e-mail is entered should contain "@" symbol.
     *  Method GetPreferredContact() is returning a string with the preferred contact.
     *  ============================================================================================================================
     *  Date last modified: 01/11/2018
     */

    public class Customer
    {
        private int _customerID;
        private string _firstName;
        private string _surname;
        private string _mail;
        private string _skype;
        private string _telephone;
        private string _prefContact;

        //Public setter and getter for _customerID proprety
        public int ID
        {
            get
            {
                return _customerID;
            }
            set
            {
                //validating if the value of ID is between 10001 and 50000.
                if (value < 10001 || value > 50000)
                {
                    throw new ArgumentException("Customer ID should be between 10001 and 50000");
                }
                else
                {
                    _customerID = value;
                }
            }
        }

        //Public setter and getter for _firstName property
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                //Validating that First name is not empty string
                if (value.Length == 0)
                {
                    throw new ArgumentException("First name should not be empty");
                }
                else
                {
                    _firstName = value;
                }
            }
        }

        //Public setter and getter for _surname property
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                //Validating that Surname is not empty string
                if (value.Length == 0)
                {
                    throw new ArgumentException("Surname should not be empty");
                }
                else
                {
                    _surname = value;
                }
            }
        }

        //Public setter and getter for _mail property
        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                //Validating if the email contains "@" symbol
                if (value.Contains('@') || value.Length == 0)
                {
                    _mail = value;
                }
                else
                {
                    throw new ArgumentException("Should contain @ symbol");
                }
            }
        }

        //Public setter and getter for _skype property
        public string Skype
        {
            get
            {
                return _skype;
            }
            set
            {
                _skype = value;
            }
        }

        //Public setter and getter for _telephone property
        public string Telephone
        {
            get
            {
                return _telephone;
            }
            set
            {
                //validating the telephone number is not empty by checking the lenght of the input string.
                if (value.Length == 0)
                {
                    throw new ArgumentException("Telephone nunmber should be entered");
                }
                else
                {
                    _telephone = value;
                }
            }
        }

        //Public setter and getter for _prefCotanct proprety
        public string PrefContact
        {
            get
            {
                return _prefContact;
            }
            set
            {
                //validating the preferred contact is the same as the telephone, skype or the mail.
                if (value.Equals(this.Telephone) || value.Equals(this.Skype) || value.Equals(this.Mail) && value.Length != 0)
                {
                    _prefContact = value;
                }
                else
                {
                    throw new ArgumentException("Preferred contact is not from the list");
                }
            }
        }

        // Public method returning string with the prefered contact.
        public string GetPreferredContact()
        {
            if (this.PrefContact.Equals(this.Telephone))
            {
                return "Tel: " + this.PrefContact;
            }
            else if (this.PrefContact.Equals(this.Skype))
            {
                return "Skype: " + this.PrefContact;
            }
            else if (this.PrefContact.Equals(this.Mail))
            {
                return "Email: " + this.PrefContact;
            }
            else return null;
        }

        //Overriding the toString() to be used to display ID, FirstName and surname properties in Listbox.
        public override string ToString()
        {
            return ID + ": " + FirstName + " " + Surname;
        }
    }

}
