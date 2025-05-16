using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TripletsAnimalMatch
{
    public class FishkaModel
    {
        public FishkiData.Shape Shape;
        public FishkiData.Color Color;
        public FishkiData.AnimalType AnimalType;

        public FishkaModel(FishkiData.Shape shape, FishkiData.Color color, FishkiData.AnimalType animalType)
        {
            Shape = shape;
            Color = color;
            AnimalType = animalType;
        }
    }
}
