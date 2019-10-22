using System;
using System.Collections.Generic;

/// <summary>
/// This code demonstrates the Mediator pattern facilitating loosely 
/// coupled communication between different Participants registering 
/// with a Chatroom. The Chatroom is the central hub through which all 
/// communication takes place.
/// </summary>
namespace Mediator.Demonstration
{
    /// <summary>
    /// Mediator Design Pattern.
    /// </summary>
    class Client
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            //This block is written last.

            //create a chatroom:
            Chatroom chatroom = new Chatroom();

            //and people in it:
            Participant eddie = new Actor("Eddie");
            Participant wally = new Actor("Wally");
            Participant beaver = new Actor("Beaver");
            Participant june = new Actor("June");
            Participant ward = new NonActor("Ward");

            chatroom.Register(eddie);
            chatroom.Register(wally);
            chatroom.Register(beaver);
            chatroom.Register(june);
            chatroom.Register(ward);

            //have them chat:
            eddie.Send("Wally", "Lets skip school.");
            june.Send("Ward", "You were a little hard on the beaver last night.");
            beaver.Send("Wally", "Awww, man.");
            beaver.Send("Eddie", "You stink, and you know it.");
            eddie.Send("June", "You look awfully pretty today, Mrs. CLeaver.");

            //run the program, you'll get a list of the chat messages.
            //presumably it can be more intricate, like messages
            //will go right to a recipient, etc.
        }
    }

    /// <summary>
    /// The 'Mediator' abstract class
    /// </summary>
    //two methods here.
    abstract class AbstractChatroom
    {
        public abstract void Register(Participant participant);
        public abstract void Send(
          string from, string to, string message);
    }

    /// <summary>
    /// The 'ConcreteMediator' class
    /// </summary>
    //The concrete chatroom
    //Implements the two methods, plus a list of everyone registered.
    class Chatroom : AbstractChatroom
    {
        private Dictionary<string, Participant> _participants =
          new Dictionary<string, Participant>();

        public override void Register(Participant participant)
        {
            if (!_participants.ContainsValue(participant))
            {
                _participants[participant.Name] = participant;
            }

            participant.Chatroom = this;
        }

        public override void Send(
          string from, string to, string message)
        {
            Participant participant = _participants[to];

            if (participant != null)
            {
                participant.Receive(from, message);
            }
        }
    }

    /// <summary>
    /// The 'AbstractColleague' class
    /// </summary>
    //The abstract colleague.
    //includes two methods to Send and Receive
    class Participant
    {
        private Chatroom _chatroom;
        private string _name;

        // Constructor
        public Participant(string name)
        {
            this._name = name;
        }

        // Gets participant name
        public string Name
        {
            get { return _name; }
        }

        // Gets chatroom
        public Chatroom Chatroom
        {
            set { _chatroom = value; }
            get { return _chatroom; }
        }

        // Sends message to given participant
        public void Send(string to, string message)
        {
            _chatroom.Send(_name, to, message);
        }

        // Receives message from given participant
        public virtual void Receive(
          string from, string message)
        {
            Console.WriteLine("{0} to {1}: '{2}'",
              from, Name, message);
        }
    }

    /// <summary>
    /// A 'ConcreteColleague' class
    /// </summary>
    //the first concrete colleague class -- actor
    //note it has Receive but not send
    //must be because the Participant class is abstract, not interface.
    class Actor : Participant
    {
        // Constructor
        public Actor(string name)
          : base(name)
        {
        }

        public override void Receive(string from, string message)
        {
            Console.Write("To an Actor: ");
            base.Receive(from, message);
        }
    }

    /// <summary>
    /// A 'ConcreteColleague' class
    /// </summary>
    //The other concrete colleague class.
    class NonActor : Participant
    {
        // Constructor
        public NonActor(string name)
          : base(name)
        {
        }

        public override void Receive(string from, string message)
        {
            Console.Write("To a non-Actor: ");
            base.Receive(from, message);
        }
    }
}
