using System.Collections.Generic;
using System.Linq;

namespace Circustrein
{
    public class Train
    {
        public IList<Container> Containers { get; }

        public Train()
        {
            Containers = new List<Container>();
        }

        public void Embark(List<Animal> animals)
        {
            var carnivores = animals.FindAll(res => res.AnimalType == AnimalType.Carnivore);
            var herbivores = animals.FindAll(res => res.AnimalType == AnimalType.Herbivore);

            var orderedCarnivores = carnivores.OrderByDescending(size => (int)size.AnimalSize).ToList();
            var orderedHerbivores = herbivores.OrderByDescending(size => (int)size.AnimalSize).ToList();

            if (orderedCarnivores.Count == 0)
            {
                foreach (var herbivore in orderedHerbivores)
                {
                    if (!TryToAddAnimalToAnyContainer(herbivore))
                    {
                        Containers.Add(new Container(herbivore));
                    }
                }
            }
            else
            {
                foreach (var carnivore in orderedCarnivores.Where(carnivore => !TryToAddAnimalToAnyContainer(carnivore)))
                {
                    Containers.Add(new Container(carnivore));
                }
                foreach (var herbivore in orderedHerbivores.Where(herbivore => !TryToAddAnimalToAnyContainer(herbivore)))
                {
                    Containers.Add(new Container(herbivore));
                }
            }
        }

        private bool TryToAddAnimalToAnyContainer(Animal animal) => Containers.Any(container => container.TryAddAnimal(animal));
    }
}
