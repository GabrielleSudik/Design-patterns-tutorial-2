using System;
using System.Collections.Generic;

/// <summary>
/// This code demonstrates the Command pattern used in a simple calculator 
/// with unlimited number of undo's and redo's. Note that in C#  the 
/// word 'operator' is a keyword. Prefixing it with '@' allows using it 
/// as an identifier.
/// </summary>
namespace Command.Demonstration
{
    /// <summary>
    /// Command Design Pattern.
    /// </summary>
    class Client
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            //this block comes after all the setup below.

            //Create user
            User user = new User();

            //Use press calculator buttons
            //Compute method is below in User class.
            user.Compute('+', 100);
            user.Compute('-', 50);
            user.Compute('*', 10);
            user.Compute('/', 2);

            //Undo 4 commands
            //each of the Computes was added to a list of executed commands.
            //this Undo method appears to be like a quasi pop.
            user.Undo(4);

            //Redo 3 commands
            user.Redo(3);

            //Wait for user
            Console.ReadKey();

        }
    }

    /// <summary>
    /// The 'Command' abstract class
    /// </summary>
    //Here it's abstract but could be interface.
    //Defines the two common methods the calculator can do.

    //I do believe: Command class should have only two methods:
    //do it, and undo it.
    //aka, execute and unexecute.
    //aka, "I command you to do this thing!"

    //all other details of HOW it will do/undo
    //are contained in the implementing class.
    //presumably, multiple classes can implement this one
    //and they don't have to be versions of a calculator
    //so long as they can DO and thing and UNDO a thing.
    abstract class Command
    {
        public abstract void Execute();
        public abstract void UnExecute();
    }

    /// <summary>
    /// The 'ConcreteCommand' class
    /// </summary>
    //This is the implementation of the Command class
    //it contains the two methods
    //and the related properties and methods that the
    //two common methods will need to actually work.
    class CalculatorCommand : Command
    {
        private char _operator;
        private int _operand;
        private Calculator _calculator;

        // Constructor
        public CalculatorCommand(Calculator calculator,
          char @operator, int operand)
            //fyi "operator" is a C# keyword;
            //the @ lets the code know this use is NOT a keyword.
        {
            this._calculator = calculator;
            this._operator = @operator;
            this._operand = operand;
        }

        // Gets operator
        public char Operator
        {
            set { _operator = value; }
        }

        // Get operand
        public int Operand
        {
            set { _operand = value; }
        }

        // Execute new command
        public override void Execute()
        {
            _calculator.Operation(_operator, _operand);
        }

        // Unexecute last command
        public override void UnExecute()
        {
            _calculator.Operation(Undo(_operator), _operand);
        }

        // Returns opposite operator for given operator
        private char Undo(char @operator)
        {
            switch (@operator)
            {
                case '+': return '-';
                case '-': return '+';
                case '*': return '/';
                case '/': return '*';
                default:
                    throw new
            ArgumentException("@operator");
            }
        }
    }

    /// <summary>
    /// The 'Receiver' class
    /// </summary>
    class Calculator
    {
        private int _curr = 0;

        public void Operation(char @operator, int operand)
        {
            switch (@operator)
            {
                case '+': _curr += operand; break;
                case '-': _curr -= operand; break;
                case '*': _curr *= operand; break;
                case '/': _curr /= operand; break;
            }
            Console.WriteLine(
              "Current value = {0,3} (following {1} {2})",
              _curr, @operator, operand);
        }
    }

    /// <summary>
    /// The 'Invoker' class
    /// </summary>
    class User
    {
        // Initializers
        private Calculator _calculator = new Calculator();
        private List<Command> _commands = new List<Command>();
        private int _current = 0;

        public void Redo(int levels)
        {
            Console.WriteLine("\n---- Redo {0} levels ", levels);
            // Perform redo operations
            for (int i = 0; i < levels; i++)
            {
                if (_current < _commands.Count - 1)
                {
                    Command command = _commands[_current++];
                    command.Execute();
                }
            }
        }

        public void Undo(int levels)
        {
            Console.WriteLine("\n---- Undo {0} levels ", levels);
            // Perform undo operations
            for (int i = 0; i < levels; i++)
            {
                if (_current > 0)
                {
                    Command command = _commands[--_current] as Command;
                    command.UnExecute();
                }
            }
        }

        //here in the User class
        //you see how a Command is created.
        //we specify its a CalculatorCommand
        //and pass in the relevant arguments
        //(both to this method Compute and to
        //the creation of CalculatorCOmmand.
        //and then you just say "do it."
        public void Compute(char @operator, int operand)
        {
            // Create command operation and execute it
            Command command = new CalculatorCommand(
              _calculator, @operator, operand);
            command.Execute();

            // Add command to undo list
            _commands.Add(command);
            _current++;
        }
    }
}