using System;
using System.Collections.Generic;

/// <summary>
/// This is code  showing the adapter pattern for client company to get 
/// employee records for third party organizations whose interface is not 
/// compatible with client.
/// </summary>
namespace Adapter.Demonstration
{
    // 'Adaptee' class

    //start here with the class,
    //just a list of employees.
    //pretend this is a class from a third party org that
    //our company works with.
    //their code might not be compatible with ours.
    //but we will make it so.
    class ThirdPartyEmployee
    {
        public List<string> GetEmployeeList()
        {
            List<string> EmployeeList = new List<string>();
            EmployeeList.Add("Peter");
            EmployeeList.Add("Paul");
            EmployeeList.Add("Puru");
            EmployeeList.Add("Preethi");
            return EmployeeList;
        }
    }

    // 'ITarget' interface
    interface ITarget
    {
        List<string> GetEmployees();
    }

    // 'Adapter' class
    //inherits both the class and the interfaces from above.
    //the method comes from the interface
    //the thing returned is the method in the class.
    class EmployeeAdapter : ThirdPartyEmployee, ITarget
    {
        public List<string> GetEmployees()
        {
            return GetEmployeeList();
        }
    }

    //that's really it.
    //you have some class. in this case our class 
    //instantiates and populates a list of employees.
    //then you have an interface. in this case it will "getEmployees()"
    //and finally you put them together by implementing the interface
    //and activating the other class inside it.

    //why call it an adapter?
    //because it can "adapt" to handle different things in different classes.
    //eg, one class uses a list, another uses an array.
    //prof gives no example, though :\

    // 'Client' class
    class Client
    {
        static void Main(string[] args)
        {
          //how will the Client use that adapter, then?
          
            Console.WriteLine("Employee list from 3rd party org:");
            Console.WriteLine();

            //use the ITarget interface to call the
            //class you need -- ie, ThirdPartyEmployee

            //first create an instance of the thing that will
            //work with both our I and the outside class:
            ITarget adapter = new EmployeeAdapter();

            //then use its info and method to iterate through the list.
            foreach (string employee in adapter.GetEmployees())
            {
                Console.WriteLine(employee);
            }
            Console.ReadLine();
        }
    }
}