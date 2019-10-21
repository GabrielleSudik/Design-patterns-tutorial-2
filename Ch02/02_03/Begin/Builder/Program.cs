using System;
using System.Collections.Generic;

/// <summary>
/// This code demonstrates the Builder pattern in which different cars 
/// are assembled in a step-by-step fashion. The CarFactory uses 
/// CarBuilders to construct a two types of cars in a series of 
/// sequential steps.
/// </summary>
namespace Builder
{
    class Program
    {
        /// <summary>
        /// The Client
        /// </summary>
        static void Main(string[] args)
        {
            //all other classes were written by the prof ahead of time
            
            //instantiate a couple builders:
            var superBuilder = new SuperCarBuilder();
            var notSoSuperBuilder = new NotSoSuperCarBuilder();
            
            //instantiate a car factory:
            var factory = new Carfactory();
            
            //create a collection of the car builders:
            //(so we can later loop through them)
            var builders = new List<CarBuilder> {
                superBuilder, notSoSuperBuilder
            };

            //for each builder
            //first assign "car" within this loop
            //to one builder at a time.
            //then get the specs for that car.
            foreach (var builder in builders){
                var car = factory.Build(builder);
                Console.WriteLine($"The car requested by " +
                    $"{builder.GetType().Name}: " +
                    $"\nHorsepower: {car.HorsePower}" +
                    $"\nImpressive feature: {car.MostImpressiveFeature}" +
                    $"\nTop Speed: {car.TopSpeedMPH} mph.\n");
            }

        }
    }

    /// <summary>
    /// The 'Product' class
    /// </summary>
    public class Car
    {
        public int TopSpeedMPH { get; set; }
        public int HorsePower { get; set; }
        public string MostImpressiveFeature { get; set; }
    }

    /// <summary>
    /// The 'Builder' abstract class
    /// </summary>
    public abstract class CarBuilder
    {
        protected readonly Car _car = new Car();
        public abstract void SetHorsePower();
        public abstract void SetTopSpeed();
        public abstract void SetImpressiveFeature();

        public virtual Car GetCar()
        {
            return _car;
        }
    }

    /// <summary>
    /// The 'Director' class
    /// </summary>
    public class Carfactory
    {
        public Car Build(CarBuilder builder)
        {
            builder.SetHorsePower();
            builder.SetTopSpeed();
            builder.SetImpressiveFeature();
            return builder.GetCar();
        }
    }

    /// <summary>
    /// The 'ConcreteBuilder1' class
    /// </summary>
    public class NotSoSuperCarBuilder : CarBuilder
    {
        public override void SetHorsePower()
        {
            _car.HorsePower = 120;
        }
        public override void SetTopSpeed()
        {
            _car.TopSpeedMPH = 70;
        }
        public override void SetImpressiveFeature()
        {
            _car.MostImpressiveFeature = "Has Air Conditioning";
        }
    }

    /// <summary>
    /// The 'ConcreteBuilder2' class
    /// </summary>
    public class SuperCarBuilder : CarBuilder
    {

        public override void SetHorsePower()
        {
            _car.HorsePower = 1640;
        }

        public override void SetTopSpeed()
        {
            _car.TopSpeedMPH = 600;
        }
        public override void SetImpressiveFeature()
        {
            _car.MostImpressiveFeature = "Can Fly";
        }

    }
}