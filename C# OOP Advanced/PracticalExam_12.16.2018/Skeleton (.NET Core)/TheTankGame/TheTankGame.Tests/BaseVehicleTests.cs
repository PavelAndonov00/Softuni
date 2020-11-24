namespace TheTankGame.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TheTankGame.Entities.Miscellaneous;
    using TheTankGame.Entities.Parts;
    using TheTankGame.Entities.Parts.Contracts;
    using TheTankGame.Entities.Vehicles;
    using TheTankGame;

    [TestFixture]
    public class BaseVehicleTests
    {
        private Type baseVehicleType;

        [SetUp]
        public void SetUp()
        {
            baseVehicleType = this.GetType("BaseVehicle");
        }

        [Test]
        public void ValidateBaseVehicleIsAbstract()
        {
            Assert.That(baseVehicleType.IsAbstract, $"BaseVehicle class must not be instantiated.");
        }

        [Test]
        public void ValidateTotalPropertie_AddPartsMethods_PartsProperty()
        {
            var revenger = new Revenger("Reven", 6d, 6m, 6, 6, 6, new VehicleAssembler());

            var arsenalPart = new ArsenalPart("Ars", 6d, 6m, 6);
            revenger.AddArsenalPart(arsenalPart);

            Assert.AreEqual(revenger.TotalAttack, 12, $"TotalAttack property returns invalid result");
            Assert.AreEqual(revenger.TotalDefense, 6, $"TotalDefense property returns invalid result");
            Assert.AreEqual(revenger.TotalHitPoints, 6, $"TotalHitPoints property returns invalid result");
            Assert.AreEqual(revenger.TotalPrice, 12, $"TotalPrice property returns invalid result");
            Assert.AreEqual(revenger.TotalWeight, 12, $"TotalWeight property returns invalid result");


            var endurancePart = new EndurancePart("End", 6d, 6m, 6);
            revenger.AddEndurancePart(endurancePart);

            Assert.AreEqual(revenger.TotalAttack, 12, $"TotalAttack property returns invalid result");
            Assert.AreEqual(revenger.TotalDefense, 6, $"TotalDefense property returns invalid result");
            Assert.AreEqual(revenger.TotalHitPoints, 12, $"TotalHitPoints property returns invalid result");
            Assert.AreEqual(revenger.TotalPrice, 18, $"TotalPrice property returns invalid result");
            Assert.AreEqual(revenger.TotalWeight, 18, $"TotalWeight property returns invalid result");

            var shellPart = new ShellPart("End", 6d, 6m, 6);
            revenger.AddShellPart(shellPart);

            Assert.AreEqual(revenger.TotalAttack, 12, $"TotalAttack property returns invalid result");
            Assert.AreEqual(revenger.TotalDefense, 12, $"TotalDefense property returns invalid result");
            Assert.AreEqual(revenger.TotalHitPoints, 12, $"TotalHitPoints property returns invalid result");
            Assert.AreEqual(revenger.TotalPrice, 24, $"TotalPrice property returns invalid result");
            Assert.AreEqual(revenger.TotalWeight, 24, $"TotalWeight property returns invalid result");

            var actualParts = revenger.Parts;
            var expectedParts = new List<IPart>() { arsenalPart, shellPart, endurancePart};

            Assert.AreEqual(actualParts, expectedParts, $"Parts property returns invalid result");
        }

        [Test]
        public void ValidateProperties()
        {
            var revenger = new Vanguard("Reven", 6d, 6m, 6, 6, 6, new VehicleAssembler());

            Assert.AreEqual(revenger.Attack, 6, $"Attack property returns invalid result");
            Assert.AreEqual(revenger.Defense, 6, $"Defense property returns invalid result");
            Assert.AreEqual(revenger.HitPoints, 6, $"HitPoints property returns invalid result");
            Assert.AreEqual(revenger.Price, 6, $"Price property returns invalid result");
            Assert.AreEqual(revenger.Weight, 6, $"Weight property returns invalid result");          
        }

        [Test]
        public void ValidateToStringMethod()
        {
            var revenger = new Vanguard("Reven", 6d, 6m, 6, 6, 6, new VehicleAssembler());

            Assert.AreEqual(revenger.Attack, 6, $"Attack property returns invalid result");
            Assert.AreEqual(revenger.Defense, 6, $"Defense property returns invalid result");
            Assert.AreEqual(revenger.HitPoints, 6, $"HitPoints property returns invalid result");
            Assert.AreEqual(revenger.Price, 6, $"Price property returns invalid result");
            Assert.AreEqual(revenger.Weight, 6, $"Weight property returns invalid result");

            var expected = "Vanguard - Reven\r\nTotal Weight: 6.000\r\nTotal Price: 6.000\r\nAttack: 6\r\nDefense: 6\r\nHitPoints: 6\r\nParts: None";

            Assert.AreEqual(revenger.ToString(), expected, $"ToString method returns invalid result.");
        }

        private Type GetType(string type)
        {
            var targetType = typeof(StartUp)
                .Assembly
                .GetTypes()
                .FirstOrDefault(x => x.Name == type);

            return targetType;
        }       
    }
}