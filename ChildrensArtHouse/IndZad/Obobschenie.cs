using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndZad
{
    class Obobschenie<Name, Section, Teacher, Price, AmountOfStudents, AmountOfLessons>
    {
        Name obj1;
        Section obj2;
        Teacher obj3;
        Price obj4;
        AmountOfStudents obj5;
        AmountOfLessons obj6;

        public Obobschenie(Name obj1, Section obj2, Teacher obj3, Price obj4, AmountOfStudents obj5, AmountOfLessons obj6)
        {
            this.obj1 = obj1;
            this.obj2 = obj2;
            this.obj3 = obj3;
            this.obj4 = obj4;
            this.obj5 = obj5;
            this.obj6 = obj6;
        }

        public void objectsType()
        {
            Console.WriteLine("\nТип объекта 1: " + typeof(Name) +
                "\nТип объекта 2: " + typeof(Section) +
                "\nТип объекта 3: " + typeof(Teacher) +
                "\nТип объекта 4: " + typeof(Price) +
                "\nТип объекта 5: " + typeof(AmountOfStudents) +
                "\nТип объекта 6: " + typeof(AmountOfLessons));

        }
        public string objectsType1()
        {
            string s = "Тип объекта 1: " + typeof(Name) +
                "\nТип объекта 2: " + typeof(Section) +
                "\nТип объекта 3: " + typeof(Teacher) +
                "\nТип объекта 4: " + typeof(Price) +
                "\nТип объекта 5: " + typeof(AmountOfStudents) +
                "\nТип объекта 6: " + typeof(AmountOfLessons);

            return s;
        }



    }
}
