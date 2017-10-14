//Andrew Smirthwaite
//15/10/17
//a program to catalog bugs in a 1x1 meter square. Object Orientated Edition.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            //create a new instance of InsectCatalog
            InsectCatalog insectCatalog = new InsectCatalog();
            insectCatalog.CatalogInsects();
        }
     }
}
