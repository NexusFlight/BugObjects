using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugObjects
{
    class Insect
    {
        //declare and initilize class variables
        protected byte X = 0;
        protected byte Y = 0;
        protected byte maxPosition = 100;

        //insect constructor that will run when derived classes are instanciated
        public Insect()
        {
            //call the getPositionFromUser passing the calling object as a parameter
            getPositionFromUser(this);
        }
        
        
        //getX function this returns the X of the current object
        public int getX()
        {
            //return the value of X
            return X;
        }

        //getY function this returns the Y of the current object
        public int getY()
        {
            //return the value of Y
            return Y;
        }

        //getPositionFromUser Procedure this gets the insects current location from the user and stores it
        private void getPositionFromUser(Insect insect)
        {
            //prompt the user for the X coord of the current insect using {0} placeholder to display the insects Type Name
            Console.WriteLine("Please Enter The X Coordinate of your {0}. Max 100",insect.GetType().Name);
            //assign X the converted user input using Convert.ToByte
            X = Convert.ToByte(Console.ReadLine());
            //loop while X is less than 0 and greater than maxPostition class variable
            while(X < 0 || X > maxPosition)
            {
                Console.WriteLine("Invalid entry Max {0}",maxPosition);
                Console.WriteLine("Please Enter The X Coordinate of your {0}. Max {1}", insect.GetType().Name,maxPosition);
                X = Convert.ToByte(Console.ReadLine());
            }

            Console.WriteLine("Please Enter The Y Coordinate of your {0}. Max {1}", insect.GetType().Name,maxPosition);
            Y = Convert.ToByte(Console.ReadLine());
            while (Y < 0 || Y > maxPosition)
            {
                Console.WriteLine("Invalid entry Max {0}",maxPosition);
                Console.WriteLine("Please Enter The Y Coordinate of your {0}. Max {1}", insect.GetType().Name,maxPosition);
                Y = Convert.ToByte(Console.ReadLine());
            }
        }

    }
}
