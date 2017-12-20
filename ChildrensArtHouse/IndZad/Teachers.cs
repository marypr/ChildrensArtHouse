using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace IndZad
{
    [Serializable]
    class Teachers
    {
        public Date dateofbirth;
        //DateTime day_of_born;
        public string name;
        public string surname;
        public int day;
        public int month;
        public int year;

       
        public Teachers()
        {

            surname = null;
            name = null;
            dateofbirth = null;
            day = 0;
            month = 0;
            year = 0;

            
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }
        public int Day
        {
            get
            {
                return day;
            }
            set
            {
                day = value;
            }
        }
        public int Month
        {
            get
            {
                return month;
            }
            set
            {
                month = value;
            }
        }
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
            }
        }


        public Teachers(string Name, string Surname, Date Dateofbirth) // с параметрами
        {
            this.name = Name;
            this.surname = Surname;
            this.dateofbirth = Dateofbirth;

        }

        public Teachers(string Name, string Surname, int day, int month, int year) // с параметрами
        {
            this.name = Name;
            this.surname = Surname;
            this.day = Day;
            this.year = Year;
            this.month = Month;

        }

        public Teachers(string Name) // с параметрами
        {
            this.name = Name;
           // this.surname = Surname;
           

        }

        public string showName()
        {
            return Name;
        }



        public Teachers(String Name, String Surname)
        {
       


            if (Regex.IsMatch(Name, "[1-9]"))
                {
                    throw new Exception("Проверьте имя");
                }
            if (Regex.IsMatch(Surname, "[1-9]"))
            {
                throw new Exception("Проверьте фамилию");
            }

                else
                {
                    this.name = Name;
                    this.surname = Surname;
                }

            
           // this.name = Name;
           // this.surname = Surname;
           

        }
       
        public String getSurname() { return this.surname; }
        public void setSurname(String surname) { this.surname = surname; }

        public String getName() { return this.name; }
        public void setName(String name) { this.name = name; }

        public Date getDateOfBirth() { return this.dateofbirth; }
        public void setDateOfBirth(Date dateOfBirth) { this.dateofbirth = dateOfBirth; }

        public String getComeptitorFullName()
        {
            return surname + " " + name + " " + dateofbirth.showDate();
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Teachers e = obj as Teachers;
            if (e == null)
            {
                return false;
            }

            return (Name == e.Name) && (e.Surname == e.Surname);
        }
        public static bool operator ==(Teachers e1, Teachers e2)
        {
            if ((e1.surname == e2.surname) && (e1.name == e2.name))
                return true;
            return false;
        }
        public static bool operator !=(Teachers e1, Teachers e2)
        {
          if ((e1.surname != e2.surname) && (e1.name != e2.name))
              return true;
            return false;
        
        }
    }
    class Diplom : Teachers
    {
        
        public bool HasHighEducation;
        public Diplom() { }

        public Diplom(string Name, string Surname, Date Dateofbirth, bool hasHighEducation)
        {

            this.name = Name;
            this.surname = Surname;
            this.dateofbirth = Dateofbirth;
            this.HasHighEducation = hasHighEducation;
        }
        public  bool Check() {
            Random rand = new Random();
            HasHighEducation = rand.NextDouble() > 0.5;
            return HasHighEducation;
        }

        
        
        }


    }

