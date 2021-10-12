using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Circustrein.Test
{
    [TestClass]
    public class ContainerTest
    {
        [TestMethod]
        //This test is meant to show you that you can not add any more than 10 total points worth of animals.
        //Each large is 5 points, medium is 3 and small is 1 point.
        public void TryAddMoreThan10()
        {
            var largeHerbi1 = new Animal(AnimalSize.Large, AnimalType.Herbivore);
            var largeHerbi2 = new Animal(AnimalSize.Large, AnimalType.Herbivore);
            var smallHerbi = new Animal(AnimalSize.Small, AnimalType.Herbivore);
            var con = new Container(largeHerbi1);

            // should return True 5+5=10
            Assert.AreEqual(con.TryAddAnimal(largeHerbi2), true);

            // Should return False 10+1=11
            Assert.AreEqual(con.TryAddAnimal(smallHerbi), false);
        }

        [TestMethod]
        //This test is to show that you can not add any other animal to a big carnivore
        public void TryAddToLargeCarnivore()
        {
            var largeCarni = new Animal(AnimalSize.Large, AnimalType.Carnivore);

            var smallCarni = new Animal(AnimalSize.Small, AnimalType.Carnivore);
            var mediumCarni = new Animal(AnimalSize.Medium, AnimalType.Carnivore);

            var smallHerbi = new Animal(AnimalSize.Small, AnimalType.Herbivore);
            var mediumHerbi = new Animal(AnimalSize.Medium, AnimalType.Herbivore);
            var largeHerbi = new Animal(AnimalSize.Large, AnimalType.Herbivore);

            var con = new Container(largeCarni);

            Assert.AreEqual(con.TryAddAnimal(smallCarni), false);
            Assert.AreEqual(con.TryAddAnimal(mediumCarni), false);
            Assert.AreEqual(con.TryAddAnimal(largeCarni), false);

            Assert.AreEqual(con.TryAddAnimal(smallHerbi), false);
            Assert.AreEqual(con.TryAddAnimal(mediumHerbi), false);
            Assert.AreEqual(con.TryAddAnimal(largeHerbi), false);
        }

        [TestMethod]
        //This test is to show that you can add a larger herbivore to a smaller carnivore
        public void AddLargerHerbivore()
        {
            var smallCarni = new Animal(AnimalSize.Small, AnimalType.Carnivore);
            var mediumCarni = new Animal(AnimalSize.Medium, AnimalType.Carnivore);

            var mediumHerbi = new Animal(AnimalSize.Medium, AnimalType.Herbivore);
            var largeHerbi = new Animal(AnimalSize.Large, AnimalType.Herbivore);

            var smallCarniCon = new Container(smallCarni);
            var mediumCarniCon = new Container(mediumCarni);

            Assert.AreEqual(smallCarniCon.TryAddAnimal(mediumHerbi), true);
            Assert.AreEqual(mediumCarniCon.TryAddAnimal(largeHerbi), true);
        }
    }
}
