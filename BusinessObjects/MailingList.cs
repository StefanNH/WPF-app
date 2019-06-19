using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    //Method CustomerExists() returning True if Customer object already exists in the list Added: 01/11/2018 by Stefan Hadzhiev


    public class MailingList
    {
        private List<Customer> _list = new List<Customer>();

        public void add(Customer newCustomer)
        {
            _list.Add(newCustomer);
        }

        public Customer find(int id)
        {
            foreach (Customer c in _list)
            {
                if (id == c.ID)
                {
                    return c;
                }
            }

            return null;

        }

        public void delete(int id)
        {
            Customer c = this.find(id);
            if (c != null)
            {
                _list.Remove(c);
            }

        }

        public List<int> ids
        {
            get
            {
               List<int> res = new List<int>();
               foreach(Customer p in _list)
                   res.Add(p.ID);
                return res;
            }
           
        }

        //method to check if customer is already in the list
        public bool CustomerExists(Customer c)
        {
            foreach (Customer z in _list)
            {
                if (z.FirstName.Equals(c.FirstName) && z.Surname.Equals(c.Surname) && z.Telephone.Equals(c.Telephone))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
