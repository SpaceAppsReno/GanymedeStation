using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ganymede.Communications;

namespace CommunicationsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string Uri = "http://ec2-54-186-39-241.us-west-2.compute.amazonaws.com:3000/api";

            using (CommunicationScope objScope = new CommunicationScope(Uri))
            {
                var basestations = objScope.GetAllBaseStations();

                Console.ReadLine();
            }
        }

        
    }
}
