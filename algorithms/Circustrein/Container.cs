using System.Collections.Generic;
using System.Linq;

namespace Circustrein
{
    public class Container
    {
        public List<Animal> Animals { get; }

        public Container(Animal animal)
        {
            Animals = new List<Animal> { animal };
        }

        public bool TryAddAnimal(Animal animal)
        {
            if (Animals.Sum(anml => (int)anml.AnimalSize) + (int)animal.AnimalSize > 10)
                return false;
            if (animal.AnimalType.Equals(AnimalType.Carnivore))
            {
                if (Animals.Exists(anml => anml.AnimalType.Equals(AnimalType.Carnivore)))
                    return false;
                if (Animals.Any(anml => (int)anml.AnimalSize <= (int)animal.AnimalSize))
                    return false;
            }
            else if (Animals.Exists(anml => anml.AnimalType.Equals(AnimalType.Carnivore) && (int)anml.AnimalSize >= (int)animal.AnimalSize))
                return false;
            Animals.Add(animal);
            return true;
        }
    }
}