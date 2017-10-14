using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugObjects
{
    class InsectCatalog
    {
        public InsectCatalog()
        {
            
        }

        public void CatalogInsects()
        {
            //declare an IList<Type> and assign it the output of GetAllInsectTypes
            IList<Type> insectTypes = GetAllInsectTypes();
            //create a foundInsects List
            IList<Insect> foundInsects = new List<Insect>();
            //call the procedure userInsects passing the parameters insectTypes and using ref to pass the memory location of the foundInsects List
            UserInsects(insectTypes, ref foundInsects);
            //call the outputFoundInsects procedure passing the parameters of List<Insect> foundInsects and IList insectTypes
            OutputFoundInsects(foundInsects, insectTypes);
        }

        //GetAllInsectTypes function finds all subclasses of Insect and returns it as a list
        public IList<Type> GetAllInsectTypes()
        {
            //get a Type Array of all the types in the current assembly of the type Insect
            Type[] objectNames = typeof(Insect).Assembly.GetTypes();
            List<Type> insectTypes = new List<Type>();
            //step though each location of the objectNames array exposing each location as type
            foreach (Type type in objectNames)
            {
                //if the current type is a subclass of Insect
                if (type.IsSubclassOf(typeof(Insect)))
                {
                    //add The type Name to the list
                    insectTypes.Add(type);
                }
            }
            //return insectTypes List<Type>
            return insectTypes;
        }

        //outputFoundInsects procedure this displays the insects inputted by the user back to the user
        private static void OutputFoundInsects(IList<Insect> foundInsects, IList<Type> insectTypes)
        {
            //itterate the loop the same amount of times as there insectNames in insects
            for (int i = 0; i < insectTypes.Count; i++)
            {
                int totalInsect = TallyInsect(foundInsects, insectTypes[i]);
                
                Console.WriteLine("You found {0} {1}", totalInsect, insectTypes[i].Name);
                if (totalInsect > 0)
                {
                    //call outputLocations passing the parrameters of Insect[] foundInsects and Insect insects.insectNames[loop index]
                    OutputLocations(foundInsects, insectTypes[i]);
                }
            }
        }

        //outputLocations procedure this displays the selected insect X Y Coords to the user 
        private static void OutputLocations(IList<Insect> foundInsects, Type type)
        {
            //run through each of the locations in Insect[] foundInsects with the current location being shown as insect
            foreach (Insect insect in foundInsects)
            {
                //if the insect Type Name is equal to the type we passed into the procedure
                if (insect.GetType() == type)
                {
                    //output the current insects X Y 
                    Console.WriteLine("X {0} Y {1}", insect.GetX(), insect.GetY());
                }
            }
        }

        //tallyInsect function this returns how many of one type of insect there is
        private static byte TallyInsect(IList<Insect> foundInsects, Type type)
        {
            //declare and initilize the output variable
            byte output = 0;
            //run through each of the locations in Insect[] foundInsects with the current location being shown as insect
            foreach (Insect insect in foundInsects)
            {
                //if the insect Type Name is equal to the type we passed into the funcation
                if (insect.GetType() == type)
                {
                    //add to the total
                    output++;
                }
            }
            //pass the output variable out
            return output;
        }

        //userInsects procedure this is where the user inputs the insects they found
        private static void UserInsects(IList<Type> insectTypes, ref IList<Insect> foundInsects)
        {
            //declare and initilize variables
            int insectInput = 0;
            int insectAmount = 0;
            bool[] hasRun = new bool[insectTypes.Count];

            //iterate a loop for the same amount of insectNames there are
            for (int i = 0; i < insectTypes.Count; i++)
            {
                //call the outputInsectMenu procedure passing the IList<Type> insectTypes and the Bool Array hasRun
                OutputInsectMenu(insectTypes, hasRun);
                Console.WriteLine("What Insect Would You Like To Catalog?");
                //assign insectInput the result of the userinput Converted to a int16
                insectInput = Convert.ToInt16(Console.ReadLine());
                //if the input is equal to the last index location this is the exit command
                if(insectInput == insectTypes.Count)
                {
                    Environment.Exit(0);
                }
                //if the insectInput is less than 0 or greater than the ammount of insects there are
                else if (insectInput < 0 || insectInput > insectTypes.Count - 1)
                {
                    //prompt the user they have entered an invaid number and output the min max
                    Console.WriteLine("Please Enter A Valid insect number 0 - {0}", insectTypes.Count);
                    //decrement the loop index
                    i--;
                }
                //if the selection has been made before and the insect has already been cataloged
                else if (hasRun[insectInput])
                {
                    //prompt the user that they have already cataloged the insect
                    Console.WriteLine("Insect Has Already Been Cataloged");
                    //decrement loop index
                    i--;
                }
                else
                {
                    Console.WriteLine("How Many {0}s did you find? Max 15", insectTypes[insectInput].Name);
                    insectAmount = Convert.ToInt16(Console.ReadLine());
                    //loop while the insectAmount is below 0 or above the max 15
                    while (insectAmount < 0 || insectAmount > 15)
                    {
                        Console.WriteLine("Invalid Amount 0 - 15 {0}s", insectTypes[insectInput].Name);
                        Console.WriteLine("How Many {0}s did you find? Max 15", insectTypes[insectInput].Name);
                        insectAmount = Convert.ToInt16(Console.ReadLine());
                    }
                    //itterate a loop the same amount of times as the user input insectAmount
                    for (int k = 0; k < insectAmount; k++)
                    {
                        //add the output of createInsect(InsectType[user inputted index])
                        foundInsects.Add(CreateInsect(insectTypes[insectInput]));
                    }
                    //set the hasRun bool to true for the current insect
                    hasRun[insectInput] = true;
                }
            }
        }

        //createInsect function this creates a new derived insect and returns it
        public static Insect CreateInsect(Type type)
        {
            //return a new instance of an object thats been cast to an Insect
            return (Insect)Activator.CreateInstance(type);
        }


        //outputInsectMenu procedure to display the insects available to catalog to the user
        private static void OutputInsectMenu(IList<Type> insectTypes, bool[] hasRun)
        {
            //for each location in the List insectNames expose insect as the current location
            foreach (Type insect in insectTypes)
            {
                //if hasRun for the index locatiuon of the index is false
                if (!hasRun[insectTypes.IndexOf(insect)])
                {
                    //output the insectName index and the insect name using {0} placeholders
                    Console.WriteLine("{0}) {1}", insectTypes.IndexOf(insect), insect.Name);
                }
            }
            Console.WriteLine("{0}) Exit",insectTypes.Count);

        }


    }
}
