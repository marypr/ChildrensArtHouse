using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndZad
{
    class TheHouse_for_arts_for_children
    {
        private string address;
        private string name;
        

      public TheHouse_for_arts_for_children()
        {

            name = null;
            address = null;

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
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }
        
        public TheHouse_for_arts_for_children(string Name, string Address) // с параметрами
        {
            this.name = Name;
            this.address= Address;
       
        }
        public String getNameandAddress() {
            return name + address;

        }

    }
}

