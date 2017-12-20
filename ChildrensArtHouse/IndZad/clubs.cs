using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndZad
{
    class clubs
    {
        private string nameOfClub;
        private Sections section;
        private Teachers teacher;
        int price;
        int numberOfLessons;
        int numberOfPupils;


        public clubs()
        {

            nameOfClub = null;
            price = 0;
            numberOfLessons = 0;
            numberOfPupils = 0;
            teacher = null;

        }
        public string NameOfClub
        {
            get
            {
                return nameOfClub;
            }
            set
            {
                nameOfClub = value;
            }
        }
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        public int NumberOfLessons
        {
            get
            {
                return numberOfLessons;
            }
            set
            {
                numberOfLessons = value;
            }
        }
        public int NumberOfPupils
        {
            get
            {
                return numberOfPupils;
            }
            set
            {
                numberOfPupils = value;
            }
        }
        public clubs(string NameOfClub, int Price, int NumberOfLessons, int NumberOfPupils,  Teachers Teachers) // с параметрами
        {
            this.nameOfClub = NameOfClub;
            this.price = Price;
            this.teacher = Teachers;
            this.numberOfLessons = NumberOfLessons;
            this.numberOfPupils = NumberOfPupils;

        }

        public clubs(string NameOfClub, int Price, int NumberOfLessons, int NumberOfPupils, string Name, string Section)
       {
           this.nameOfClub = NameOfClub;
           this.price = Price;
           this.numberOfLessons = NumberOfLessons;
           this.numberOfPupils = NumberOfPupils;
           this.teacher = new Teachers(Name);
           this.section = new Sections();
       }

        public static bool operator >(clubs c1, clubs c2)
        {
            if ((c1.NumberOfPupils > c2.NumberOfPupils))
                return true;
            return false;
        }

         public static bool operator <(clubs c1, clubs c2)
        {
            if ((c1.NumberOfPupils < c2.NumberOfPupils))
                return true;
            return false;
        }

    }
}
