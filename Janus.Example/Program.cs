
using Janus.Example.model;
using Janus.Utils;
using System;

namespace Janus.Example
{
    class Program
    {
        static void Main(string[] args)
        {      
            Repository repo = Repository.GlobalRepository;
            
            Person paul = new Person() { Name = "paul", Age = 22 };
            Person marco = new Person() { Name = "marco", Age = 45 };
            Person polo = new Person() { Name = "polo", Age = 57, Dog = new Dog { Name = "punk" } };
            var diff = repo.Commit("feat: my 1st commit");
     
            polo.Age = 23;
            paul.Dog = polo.Dog;
            polo.Dog = new Dog { Name = "graphic" };
            polo.Dog = polo.Dog;
            polo.Dog = polo.Dog;
            polo.Dog = polo.Dog;
            polo.Dog = polo.Dog;
            polo.Dog = polo.Dog;
            polo.Dog = polo.Dog;
            polo.Dog = polo.Dog;
            polo.Friend = polo.Dog;
            diff = repo.Commit("feat: my 2nd commit");
            Console.WriteLine("press enter");
            Console.ReadLine();
        }

        private static int Add(int a, int b)
        {
            return a + b;
        }
    }

}
