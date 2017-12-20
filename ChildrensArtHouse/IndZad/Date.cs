using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndZad
{
    [Serializable]
    class Date
    {
        private int day;
        private int month;
        private int year;

        public Date()
        {
            year = 0;   //  year = DateTime.Today.Year;
            month = 0;// month = DateTime.Today.Month;
            day = 0;// day = DateTime.Today.Day;
        }

        public Date(int theday, int themonth, int theyear)
        {
            if (theday <= 0 || theday > 31)
            {
                throw new Exception("Проверьте день рождения(от 1 до 31)");
            }
            if (themonth <= 0 || themonth >= 13)
            {
                throw new Exception("Проверьте месяц рождения (от 1 до 12)");
            }
             if (theyear <= 1947 || theyear >= 1999)
            {
                throw new Exception("Руководитель должен быть старше 17 и младше 70");
            }
            else
            {
                day = theday;
                month = themonth;
                year = theyear;
            }
           
        }
   
       
        public int Year
        { //акссесоры
            get
            {
                return year;
            }
            private set { year = value; }
        }
        public int Month
        {
            get
            {
                return month;
            }
            private set
            {
                if (value > 0 && value < 13)
                    month = value;
                else
                {
                    Console.WriteLine("Invalid month ({00}), set to 1", value);
                    month = 1;
                }
            }
        }

        public int Day
        {
            get
            {
                return day;
            }
            private set { day = value; }
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}", Day.ToString("00."), Month.ToString("00."), Year);
        }
        public string Show()
        {
           
            return (Day.ToString("00.") + "." + Month.ToString("00.") + "." + Year);
        }

        ~Date()//деструктор 
        {
        }


        public String showDate()
        {
            return year + "/" + month + "/" + day;
        }
    }
}
