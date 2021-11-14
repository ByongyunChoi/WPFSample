using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample
{
    internal class Person : INotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;

                OnPropertyChanged(nameof(Name));
                NotifyPropertyChanged();
            }
        }

        public string Address { get; private set; }
        public int Age { get; set; }

        private string lastName;
        private string middleName;
        private string firstName;

        public string FullName
        {
            get
            {
                return $"{lastName}^{firstName}^{middleName}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Person()
        {
            lastName = "last";
            middleName = "middle";
            firstName = "first";
        }
    }

    public static class PropertyTest
    {
        public static void Execute()
        {
            Person person = new Person();
            person.PropertyChanged += Person_PropertyChanged;

            //person.Address = "abcd"; // Error
            person.Age = 15;
            person.Name = "Kim";
        }

        private static void Person_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("Person_PropertyChanged : " + e.PropertyName);
        }
    }
}