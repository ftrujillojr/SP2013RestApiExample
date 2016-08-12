using System;
using System.Collections.Generic;
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

    class HttpHelper : System.Object    // all classes inherit from System.Object.  Just showing how to inherit.
    {
        private bool? myDebug = null;  // the ? makes this variable nullable.
        private Season mySeason;

        public bool MyProperty { get; set; }  // 1. property without private backup.

        public Season MySeason                // 2. auto implemented properties with private backup.
        {
            get { return mySeason; }
            set { mySeason = value; }         // value is a reserved keyword.
        }

        public HttpHelper()  // Default Constructor
        {
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

    }


}
