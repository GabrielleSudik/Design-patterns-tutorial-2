﻿using System;
using System.Collections.Generic;

/// <summary>
/// This code demonstrates the Visitor pattern in which two 
/// objects traverse a list of Employees and performs the same 
/// operation on each Employee. The two visitor objects 
/// define different operations -- one adjusts vacation days and 
/// the other income.
/// </summary>
namespace Visitor.Demonstration
{
    /// <summary>
    /// Visitor Design Pattern.
    /// </summary>
    class Client
    {
        static void Main()
        {
            //this block is written last.

            //create a collection of employees:
            Employees ees = new Employees();
            ees.Attach(new Clerk());
            ees.Attach(new Director());
            ees.Attach(new President());

            //employees are visited:
            ees.Accept(new IncomeVisitor());
            ees.Accept(new VacationVisitor());

            Console.ReadLine();

            //run the program, see the income visitor
            //update everyone's income.
            //and the vacation visitor update everyone's vacation days.
            //(ie, run the visitor methods on the Employee classes)
        }
    }

    /// <summary>
    /// The 'Visitor' interface
    /// </summary>
    interface IVisitor
    {
        void Visit(Element element);
    }

    /// <summary>
    /// A 'ConcreteVisitor' class
    /// </summary>
    class IncomeVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            Employee employee = element as Employee;

            // Provide 10% pay raise
            employee.Income *= 1.10;
            Console.WriteLine("{0} {1}'s new income: {2:C}",
              employee.GetType().Name, employee.Name,
              employee.Income);
        }
    }

    /// <summary>
    /// A 'ConcreteVisitor' class
    /// </summary>
    class VacationVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            Employee employee = element as Employee;

            // Provide 3 extra vacation days
            employee.VacationDays += 3;
            Console.WriteLine("{0} {1}'s new vacation days: {2}",
              employee.GetType().Name, employee.Name,
              employee.VacationDays);
        }
    }

    /// <summary>
    /// The 'Element' abstract class
    /// </summary>
    abstract class Element
    {
        public abstract void Accept(IVisitor visitor);
        //this confuses me -- IVisitor and its concrete classes
        //have methods that accept an argument of type Element.
        //but this method, in Element, accepts an argument
        //of type IVisitor.
        //what am I missing?
    }

    /// <summary>
    /// The 'ConcreteElement' class
    /// </summary>
    class Employee : Element
    {
        private string _name;
        private double _income;
        private int _vacationDays;

        // Constructor
        public Employee(string name, double income,
          int vacationDays)
        {
            this._name = name;
            this._income = income;
            this._vacationDays = vacationDays;
        }

        // Gets or sets the name
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        // Gets or sets income
        public double Income
        {
            get { return _income; }
            set { _income = value; }
        }

        // Gets or sets number of vacation days
        public int VacationDays
        {
            get { return _vacationDays; }
            set { _vacationDays = value; }
        }

        //when you call Accept on an employee,
        //the passed in visitor gets to visit and do its thing.
        //I'm still seeing (incorrectly?) a really circular dependency going on??
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    /// <summary>
    /// The 'ObjectStructure' class
    /// </summary>
    class Employees
    {
        private List<Employee> _employees = new List<Employee>();

        public void Attach(Employee employee)
        {
            _employees.Add(employee);
        }

        public void Detach(Employee employee)
        {
            _employees.Remove(employee);
        }

        //allowing a visitor to visit the Employees class.
        //this specific loop allows a visit to each Employee.
        public void Accept(IVisitor visitor)
        {
            foreach (Employee e in _employees)
            {
                e.Accept(visitor);
            }
            Console.WriteLine();
        }
    }

    // Three employee types

    class Clerk : Employee
    {
        // Constructor
        public Clerk()
          : base("Harry", 25000.0, 14)
        {
        }
    }

    class Director : Employee
    {
        // Constructor
        public Director()
          : base("Edward", 35000.0, 16)
        {
        }
    }

    class President : Employee
    {
        // Constructor
        public President()
          : base("Damond", 45000.0, 21)
        {
        }
    }
}