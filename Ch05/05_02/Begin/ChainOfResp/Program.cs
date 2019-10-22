using System;

/// <summary>
/// This code demonstrates the Chain of Responsibility pattern in which 
/// several linked managers and executives can respond to a purchase 
/// request or hand it off to a superior. Each position has can have 
/// its own set of rules which orders they can approve.
/// </summary>
namespace Chain.Demonstration
{
    /// <summary>
    /// Chain of Responsibility Design Pattern.
    /// </summary>
    class Client
    {        
        static void Main()
        {
            //this code comes last after the blueprint for the chain is set up below.
            
            //define some Approvers:
            Approver lucy = new Director();
            Approver ricky = new VicePresident();
            Approver ginger = new President();

            //assign each of their successors (except Pres, who is the end of the chain):
            lucy.SetSuccessor(ricky);
            ricky.SetSuccessor(ginger);

            //generate a purchase request:
            Purchase p1 = new Purchase(888, 350.00, "Assets");
            //then send it to the first link in the approval chain:
            lucy.ProcessRequest(p1);
            //approved by director

            //repeat with other amounts:
            Purchase p2 = new Purchase(889, 40000.00, "Vehicle");
            lucy.ProcessRequest(p2);
            //approved by president

            Purchase p3 = new Purchase(890, 140000.00, "Office Annex");
            lucy.ProcessRequest(p3);
            //not approved; meeting scheduled.

            //wait for user:
            Console.ReadKey();

        }
    }

    /// <summary>
    /// The 'Handler' abstract class
    /// </summary>
    //We name it Approver because pretend you are an employee
    //who needs approval to, say, make a big purchase.
    //could be an interface if we wanted.
    //basically outlines the successor and a method stub
    //for what the actual approver will actually do.
    abstract class Approver
    {
        protected Approver successor;

        public void SetSuccessor(Approver successor)
        {
            this.successor = successor;
        }

        public abstract void ProcessRequest(Purchase purchase);
    }

    /// <summary>
    /// The 'ConcreteHandler' class
    /// </summary>
    //in practice, we want the director to be able to approve
    //up to $10k. if he can't, the request will pass on to the
    //successor (ie, the next person in line)
    class Director : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 10000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                  this.GetType().Name, purchase.Number);
            }
            else if (successor != null)
            {
                successor.ProcessRequest(purchase);
            }
        }
    }

    /// <summary>
    /// The 'ConcreteHandler' class
    /// </summary>
    //Ditto with the VP but up to $25k
    class VicePresident : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 25000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                  this.GetType().Name, purchase.Number);
            }
            else if (successor != null)
            {
                successor.ProcessRequest(purchase);
            }
        }
    }

    /// <summary>
    /// The 'ConcreteHandler' class
    /// </summary>
    //Finally, up to the President, up to $100k.
    //In this case, there is no successor; the chain
    //"stops" with a decision to hold a meeting.
    class President : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 100000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                  this.GetType().Name, purchase.Number);
            }
            else
            {
                Console.WriteLine(
                  "Request# {0} requires an executive meeting!",
                  purchase.Number);
            }
        }
    }

    /// <summary>
    /// Class holding request details
    /// </summary>
    //This is just the request with its pertinent details.
    //it's what gets passed to each approver.
    class Purchase
    {
        private int _number;
        private double _amount;
        private string _purpose;

        // Constructor
        public Purchase(int number, double amount, string purpose)
        {
            this._number = number;
            this._amount = amount;
            this._purpose = purpose;
        }

        // Gets or sets purchase number
        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        // Gets or sets purchase amount
        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        // Gets or sets purchase purpose
        public string Purpose
        {
            get { return _purpose; }
            set { _purpose = value; }
        }
    }
}