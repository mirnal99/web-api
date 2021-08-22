using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace day4.Model
{
    public class Person
    {
        //public static Dictionary<int, string> family = new Dictionary<int, string>();
        public static List<Person> friends = new List<Person>();

        public int id { get; set; } = 0;
        public string firstName { get; set; } = "";
        public string lastName { get; set; } = "";
    }

    public class MShip
    {
        public static List<MShip> memberships = new List<MShip>();


        public int id { get; set; } = 0;
        public int OIB { get; set; } = 0;
        public int cijena { get; set; } = 0;

    }
}
