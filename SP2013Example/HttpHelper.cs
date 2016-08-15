using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP2013Example
{
    enum Season { Spring = 101, Summer, Fall, Winter }

    struct Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    struct Time   // compare struct using Equals() method, not ==
    {
        private int hours, minutes, seconds;

        public Time(int hh, int mm, int ss)
        {
            this.hours = hh % 24;
            this.minutes = mm % 60;
            this.seconds = ss % 60;
        }

        public override string ToString()  // same as @Override in Java
        {
            string str = String.Format("{0} hours {1} mins {2} sec", this.hours, this.minutes, this.seconds);
            return str;
        }
    }

    interface XYCoordinates
    {
        int X { get; set; }
        int Y { get; set; }
    }

    // Use properties to initialize an object.
    //
    // HttpHelper httpHelper = new HttpHelper {X = 1, Y=2};

    class HttpHelper : System.Object, IDisposable, XYCoordinates
    {
        private bool? myDebug = null;  // the ? makes this variable nullable.
        private Season mySeason;
        private int x;
        private int y;
        private bool disposed = false;  // flag to indicate whether the resource
        // has already been disposed
        public bool MyProperty { get; set; }  // 1. property without private backup.
        public Season MySeason                // 2. auto implemented properties with private backup.
        {
            // get is public and set is private
            get { return this.mySeason; }          // You don't have to define both get and set.
            private set { this.mySeason = value; } // value is a reserved keyword.
        }

        public virtual int X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public virtual int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public HttpHelper()  // Default Constructor
        {
            //allocate some HUGE memory resource
            this.disposed = false;   // This will be used in Dispose()

            if (!this.myDebug.HasValue)  // or  this.debug == null
            {
                this.myDebug = false;
            }

            this.mySeason = Season.Spring;
        }

        public HttpHelper(HttpHelper rhs) // Copy Constructor
        {
            this.myDebug = rhs.myDebug;
        }

        ~HttpHelper()  // Destructor.   Can not use public or any params.
        {
            this.myDebug = null;   // close handles and set vars to null to tell GC ok to release memory.
        }



        private void someMethodWithOut(out bool boolValue) // boolValue passed by Reference, must assign to it inside.
        {
            boolValue = false;
        }

        // virtual says any derived class can override this method.  must be public method.
        public virtual void someMethodWithRef(ref bool boolValue) // boolValue passed by Reference, will fail if pass in null.
        {
            boolValue = false;
        }

        private void arrayExamples()
        {
            int size = int.Parse(Console.ReadLine());
            // int[] pins = new int[size]; // array pins
            int[] pins = new int[4] { 9, 3, 7, 2 };

            for (int index = 0; index < pins.Length; index++)
            {
                int pin = pins[index];
                Console.WriteLine(pin);
            }

            foreach (int pin in pins)
            {
                Console.WriteLine(pin);
            }

            // Time[] schedule = { new Time(12, 30), new Time(5, 30) };

            // var keyword lets compiler decide types to store.  MUST be the same type. string in this case.
            var names = new[] { "John", "Diana", "James", "Francesca" };

            var names2 = new[] 
            { 
                new { Name = "John", Age = 47 },
                new { Name = "Diana", Age = 46 },
                new { Name = "James", Age = 20 },
                new { Name = "Francesca", Age = 18 } 
            };

            foreach (var familyMember in names2)
            {
                Console.WriteLine("Name: {0}, Age: {1}", familyMember.Name, familyMember.Age);
            }

            // COPY an array.
            int[] copy = new int[pins.Length];
            for (int i = 0; i < pins.Length; i++)
            {
                copy[i] = pins[i];
            }

            int[] copy2 = new int[pins.Length];  // allocate memory first
            pins.CopyTo(copy2, 0);  // copy pins to copy2 starting at index 0.
            //OR  =>  Array.Copy(pins, copy2, copy.Length);
            //OR  =>  int[] copy3 = (int[])pins.Clone();

            int[,] items = new int[4, 6];  // multi-dimentional arrays.


            int[][] items2 = new int[4][];   // jagged array
            int[] columnForRow0 = new int[3];
            int[] columnForRow1 = new int[10];
            int[] columnForRow2 = new int[40];
            int[] columnForRow3 = new int[25];
            items2[0] = columnForRow0;
            items2[1] = columnForRow1;
            items2[2] = columnForRow2;
            items2[3] = columnForRow3;

        }

        public void tryCatchExample(string filename)
        {
            TextReader reader = new StreamReader(filename);
            try
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
            finally
            {
                reader.Close();
            }
        }

        public void usingStatementEquivilantToTryCatch(string filename)
        {
            // The variable you declare in a using statement must be of a type that implements the IDisposable interface.
            // reader in this case.
            using (TextReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }

        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /*  How to use using block for IDisposable resources
         * 
         *   using(HttpHelper httpHelper = new HttpHelper())
         *   {
         *          // do work.
         *          // Dispose() will be called automatically.
         *   }
         * 
         */
        protected virtual void Dispose(bool disposing)
        {
            lock (this) // Eliminate the chances of two concurrent threads disposing of the same resources
            // in the same object simultaneously.
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        // release large, managed resource here
                    }
                    // release unmanaged resources here
                    this.disposed = true;
                }
            }
        }

        /* C#                                       Java
         * ========================      ========   ====================================
         * List<T>                                  List<> interface on ArrayList<> class
         * Queue<T>                      FIFO       Queue<> interface on LinkedList<> class
         * Stack<T>                      LIFO       Stack<> class
         * LinkedList<T>                            LinkedList<> class
         * HashSet<T>                               Set<> interface on HashSet<> or LinkedHashSet<> or TreeSet<> classes
         * 
         * Dictionary<TKey, TValue>                 Map<> interface on HashMap<> or LinkedHashMap classes
         * SortedList<TKey, TValue>                 Map<> interface on TreeMap<> class
         */
        public void collectionsExample()
        {
            List<int> numbers = new List<int>();
            // Fill the List<int> by using the Add method
            foreach (int number in new int[12] { 10, 9, 8, 7, 7, 6, 5, 10, 4, 3, 2, 1 })
            {
                numbers.Add(number);
            }
            // Insert an element in the penultimate position in the list, and move the last item up
            // The first parameter is the position; the second parameter is the value being inserted
            numbers.Insert(numbers.Count - 1, 99);

            // Remove first element whose value is 7 (the 4th element, index 3)
            numbers.Remove(7);

            // Remove the element that's now the 7th element, index 6 (10)
            numbers.RemoveAt(6);

            // Iterate remaining 11 elements using a for statement
            Console.WriteLine("Iterating using a for statement:");

            for (int i = 0; i < numbers.Count; i++)
            {
                int number = numbers[i]; // Note the use of array syntax
                Console.WriteLine(number);
            }
            // Iterate the same 11 elements using a foreach statement
            Console.WriteLine("\nIterating using a foreach statement:");

            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }

            // ********************************************************************************

            Dictionary<string, int> ages = new Dictionary<string, int>();
            // Dictionary<string, int> ages = new Dictionary<string, int>() {{"John", 44}, {"Diana", 45}, {"James", 17}, {"Francesca", 15}};

            // fill the Dictionary
            ages.Add("John", 47); // using the Add method
            ages.Add("Diana", 46);
            ages["James"] = 20; // using array notation
            ages["Francesca"] = 18;

            // iterate using a foreach statement
            // the iterator generates a KeyValuePair item
            Console.WriteLine("The Dictionary contains:");

            foreach (KeyValuePair<string, int> element in ages)
            {
                string name = element.Key;
                int age = element.Value;
                Console.WriteLine("Name: {0}, Age: {1}", name, age);
            }

            // ********************************************************************************

            // Create and populate the personnel list
            List<Person> personnel = new List<Person>()
            {
                 new Person() { ID = 1, Name = "John", Age = 47 },
                 new Person() { ID = 2, Name = "Sid", Age = 28 },
                 new Person() { ID = 3, Name = "Fred", Age = 34 },
                 new Person() { ID = 4, Name = "Paul", Age = 22 },
            };
            // Find the member of the list that has an ID of 3
            Person match = personnel.Find((Person p) => { return p.ID == 3; });

            Console.WriteLine("ID: {0}\nName: {1}\nAge: {2}", match.ID, match.Name, match.Age);
        }

        public void linqExamples()
        {
            // In a real-world application, you would populate these arrays by reading the data
            // from a file or a database.

            var customers = new[] {
                 new { CustomerID = 1, FirstName = "Kim", LastName = "Abercrombie", CompanyName = "Alpine Ski House" },
                 new { CustomerID = 2, FirstName = "Jeff", LastName = "Hay", CompanyName = "Coho Winery" },
                 new { CustomerID = 3, FirstName = "Charlie", LastName = "Herb", CompanyName = "Alpine Ski House" },
                 new { CustomerID = 4, FirstName = "Chris", LastName = "Preston", CompanyName = "Trey Research" },
                 new { CustomerID = 5, FirstName = "Dave", LastName = "Barnett", CompanyName = "Wingtip Toys" },
                 new { CustomerID = 6, FirstName = "Ann", LastName = "Beebe", CompanyName = "Coho Winery" },
                 new { CustomerID = 7, FirstName = "John", LastName = "Kane", CompanyName = "Wingtip Toys" },
                 new { CustomerID = 8, FirstName = "David", LastName = "Simpson", CompanyName = "Trey Research" },
                 new { CustomerID = 9, FirstName = "Greg", LastName = "Chapman", CompanyName = "Wingtip Toys" },
                 new { CustomerID = 10, FirstName = "Tim", LastName = "Litton", CompanyName = "Wide World Importers" }
            };

            var addresses = new[] {
                 new { CompanyName = "Alpine Ski House", City = "Berne", Country = "Switzerland"},
                 new { CompanyName = "Coho Winery", City = "San Francisco", Country = "United States"},
                 new { CompanyName = "Trey Research", City = "New York", Country = "United States"},
                 new { CompanyName = "Wingtip Toys", City = "London", Country = "United Kingdom"},
                 new { CompanyName = "Wide World Importers", City = "Tetbury", Country = "United Kingdom"}
            };

            IEnumerable<string> customerFirstNames = customers.Select(cust => cust.FirstName);

            // The Select method is not actually a method of the Array type. It is an extension method of
            // the Enumerable class. The Enumerable class is located in the System.Linq namespace.

            foreach (string name in customerFirstNames)
            {
                Console.WriteLine(name);
            }

            var customerNames = customers.Select(cust => new { FirstName = cust.FirstName, LastName = cust.LastName });

            IEnumerable<string> usCompanies = addresses.Where(addr => String.Equals(addr.Country, "United States"))
                                                        .Select(usComp => usComp.CompanyName);

            foreach (string name in usCompanies)
            {
                Console.WriteLine(name);
            }

            // OrderBy()  OrderByDescending()
            IEnumerable<string> companyNames = addresses.OrderBy(addr => addr.CompanyName)
                                                        .Select(comp => comp.CompanyName);

            foreach (string name in companyNames)
            {
                Console.WriteLine(name);
            }

            int numberOfCompanies2 = addresses.Select(addr => addr.CompanyName).Count();
            Console.WriteLine("Number of companies: {0}", numberOfCompanies2);

            int numberOfCountries = addresses.Select(addr => addr.Country).Distinct().Count();
            Console.WriteLine("Number of countries: {0}", numberOfCountries);

            // Join
            var companiesAndCustomers = customers.Select(c => new { c.FirstName, c.LastName, c.CompanyName })
                                                 .Join(addresses, custs => custs.CompanyName, addrs => addrs.CompanyName,
                                                         (custs, addrs) => new { custs.FirstName, custs.LastName, addrs.Country });

            foreach (var row in companiesAndCustomers)
            {
                Console.WriteLine(row);
            }

            // FROM SELECT
            var customerNames2 = from cust in customers
                                 select new { cust.FirstName, cust.LastName };
            // FROM WHERE SELECT
            var usCompanies2 = from a in addresses
                              where String.Equals(a.Country, "United States")
                              select a.CompanyName;

            // FROM GROUP
            var companiesGroupedByCountry = from a in addresses
                                            group a by a.Country;

            foreach (var companiesPerCountry in companiesGroupedByCountry)
            {
                Console.WriteLine("Country: {0}\t{1} companies", companiesPerCountry.Key, companiesPerCountry.Count());

                foreach (var companies in companiesPerCountry)
                {
                    Console.WriteLine("\t{0}", companies.CompanyName);
                }
            }

            // COUNT
            int numberOfCompanies3 = (from a in addresses select a.CompanyName).Count();
            // DISTINCT COUNT
            int numberOfCountries4 = (from a in addresses select a.Country).Distinct().Count();

            // JOIN ON
            var countriesAndCustomers = from a in addresses
                                        join c in customers
                                        on a.CompanyName equals c.CompanyName  // Linq DOES NOT support < or >, only equals
                                        //  From ref LHS        Join ref RHS
                                        select new { c.FirstName, c.LastName, a.Country };
        }

    } // class HttpHelper



} // namespace SP2013Example
