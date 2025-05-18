namespace TripletsAnimalMatch
{
    public class TileModel
    {
        public TileData.Shape Shape;
        public TileData.Color Color;
        public TileData.AnimalType AnimalType;

        public TileModel(TileData.Shape shape, TileData.Color color, TileData.AnimalType animalType)
        {
            Shape = shape;
            Color = color;
            AnimalType = animalType;
        }

        public string GetKey()
        {
            return $"{Shape}_{Color}_{AnimalType}";
        }
    }
}
