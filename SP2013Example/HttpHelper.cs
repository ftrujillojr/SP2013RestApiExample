using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP2013Example
{
    enum Season { Spring = 101, Summer, Fall, Winter }

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


    } // class HttpHelper


} // namespace SP2013Example
