using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TripletsAnimalMatch
{
    public class FishkiData : MonoBehaviour
    {
        public enum Shape 
        { 
            Circle,
            Hexagon,
            Pentagon,
            Rectangle
        }

        public enum Color 
        {             
            Blue,
            Gray,            
            Green,
            Pink,
            Purple,
            Red,
            Turquoise,
            Yellow 
        }
        public enum AnimalType 
        {
            Bear,
            Cow, 
            Cat,
            Dog,
            Dragon,
            Monkey,
            Rabbit,
            Rat
        }

        public List<Texture2D> ShapesCircle = new List<Texture2D>();

        public List<Texture2D> ShapesHexagon = new List<Texture2D>();

        public List<Texture2D> ShapesPentagon = new List<Texture2D>();

        public List<Texture2D> ShapesRectangle = new List<Texture2D>();

        public List<Texture2D> AnimalTexture = new List<Texture2D>();

        public List<GameObject> ShapesColliders = new List<GameObject>();

        public Fishka Fishka;

    }
}
