using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Circustrein.Test
{
    [TestClass]
    public class TrainTest
    {
        [TestMethod]
        //This test is to show the efficiency of the train there are 30 points worth of animals here in this test.
        //Each container can hold up to 10 points. If the algorithm is as efficient as possible that would mean that only 3 containers should get filled.
        public void ShowEfficiencyWithHerbivores()
        {
            var train = new Train();
            var animals = new List<Animal>
            {
                new Animal(AnimalSize.Large, AnimalType.Herbivore),
                new Animal(AnimalSize.Medium, AnimalType.Herbivore),
                new Animal(AnimalSize.Medium, AnimalType.Herbivore),
                new Animal(AnimalSize.Medium, AnimalType.Herbivore),
                new Animal(AnimalSize.Large, AnimalType.Herbivore),
                new Animal(AnimalSize.Large, AnimalType.Herbivore),
                new Animal(AnimalSize.Small, AnimalType.Herbivore),
                new Animal(AnimalSize.Large, AnimalType.Herbivore)
            };

            train.Embark(animals);
            Assert.AreEqual(train.Containers.Count, 3);
        }

        [TestMethod]
        public void ShowEfficiencyWithAll()
        {
            var train = new Train();
            var animals = new List<Animal>
            {
                new Animal(AnimalSize.Large, AnimalType.Herbivore),
                new Animal(AnimalSize.Large, AnimalType.Herbivore),
                new Animal(AnimalSize.Large, AnimalType.Herbivore),
                new Animal(AnimalSize.Large, AnimalType.Herbivore),
                new Animal(AnimalSize.Large, AnimalType.Herbivore),
                new Animal(AnimalSize.Large, AnimalType.Carnivore),
                new Animal(AnimalSize.Medium, AnimalType.Herbivore),
                new Animal(AnimalSize.Medium, AnimalType.Herbivore),
                new Animal(AnimalSize.Large, AnimalType.Carnivore),
                new Animal(AnimalSize.Medium, AnimalType.Herbivore),
                new Animal(AnimalSize.Small, AnimalType.Herbivore),
                new Animal(AnimalSize.Medium, AnimalType.Carnivore),
                new Animal(AnimalSize.Medium, AnimalType.Carnivore),
                new Animal(AnimalSize.Small, AnimalType.Carnivore),
            };
            train.Embark(animals);

            var index = 0;
            foreach (var container in train.Containers)
            {
                var debug = $"Container{index}: ";
                debug = container.Animals.Aggregate(debug, (current, @group) => current + (@group.AnimalType + "|" + @group.AnimalSize + ","));
                Console.WriteLine(debug.TrimEnd(','));
                index++;
            }
            Assert.AreEqual(train.Containers.Count, 7);
        }
    }
}
