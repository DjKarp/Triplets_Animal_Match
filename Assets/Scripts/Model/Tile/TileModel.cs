namespace TripletsAnimalMatch
{
    public class TileModel
    {
        public TileData.Shape Shape;
        public TileData.Color Color;
        public TileData.AnimalType AnimalType;
        public TileData.TileEffect TileEffect;

        public TileModel(TileData.Shape shape, TileData.Color color, TileData.AnimalType animalType)
        {
            Shape = shape;
            Color = color;
            AnimalType = animalType;
            TileEffect = TileData.TileEffect.None;
        }

        public string GetKey()
        {
            return $"{Shape}_{Color}_{AnimalType}";
        }
    }
}
