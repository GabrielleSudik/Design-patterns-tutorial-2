using System;
using System.Collections.Generic;

/// <summary>
/// This code demonstrates the composite pattern for an application where 
/// any employee can see their own performance record. The supervisor can 
/// see their own and their subordinate’s performance record
/// </summary>
namespace Composite.Demonstration
{
    //'IComponent' interface
    //3 properties, 1 method.
    //will be implemented by plain employees and supervisors.
    public interface IEmployee
    {
        int EmployeeID { get; set; }
        string Name { get; set; }
        int Rating { get; set; }
        void PerformanceSummary();
    }
    //'Leaf' class - will be leaf node in tree structure
    //this is for employees who do not supervise anyone
    //they can see only their own records.
    public class Employee : IEmployee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }

        public void PerformanceSummary()
        {
            Console.WriteLine("\nPerformance summary of employee: " +
                              $"{Name} is {Rating} out of 5");
        }
    }
    //'Composite' class - will be branch node in tree structure
    //a supervisor who can see her own records and the
    //records of her staff.
    //notice the extra methods to list staff and add staff.
    public class Supervisor : IEmployee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }

        public List<IEmployee> ListSubordinates = new List<IEmployee>();

        public void PerformanceSummary()
        {
            Console.WriteLine("\nPerformance summary of supervisor: " +
                              $"{Name} is {Rating} out of 5");
        }

        public void AddSubordinate(IEmployee employee)
        {
            ListSubordinates.Add(employee);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //start with creating some employees:
            Employee lucy = new Employee {EmployeeID = 1,
                Name = "Lucy", Rating = 4};
            Employee ricky = new Employee {EmployeeID = 2,
                Name = "Ricky", Rating = 3};
            Employee ginger = new Employee {EmployeeID = 3,
                Name = "Ginger", Rating = 5};
            
            //and supervisors:
            Supervisor thurston = new Supervisor {EmployeeID = 4,
                Name = "Thurston", Rating = 4};
            Supervisor donna = new Supervisor {EmployeeID = 5,
                Name = "Donna", Rating = 5};

            //create their supervisor/employee relationships:
            thurston.AddSubordinate(lucy);
            donna.AddSubordinate(ricky);
            donna.AddSubordinate(ginger);

            Console.WriteLine("Employee can see their performance summary:");
            lucy.PerformanceSummary();

            Console.WriteLine("Supervisors can see their subordinates:");
            donna.PerformanceSummary();

            Console.WriteLine("Subordinate performaces:");
            foreach (Employee employee in donna.ListSubordinates)
	        {
                employee.PerformanceSummary();
	        }

            Console.ReadLine();
        }
    }
}