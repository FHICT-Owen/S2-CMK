namespace Circustrein
{
    public class Animal
    {
        public readonly AnimalSize AnimalSize;

        public readonly AnimalType AnimalType;

        public Animal(AnimalSize size, AnimalType type)
        {
            AnimalSize = size;
            AnimalType = type;
        }
    }
}
