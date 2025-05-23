using System.Collections.Generic;
using UnityEngine;

namespace TripletsAnimalMatch
{
    public class TileData : MonoBehaviour
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

        public enum TileEffect
        {
            None,
            Heavy,
            Sticky,
            Exploding,
            Frozen
        }

        public List<Sprite> ShapesCircle = new List<Sprite>();

        public List<Sprite> ShapesHexagon = new List<Sprite>();

        public List<Sprite> ShapesPentagon = new List<Sprite>();

        public List<Sprite> ShapesRectangle = new List<Sprite>();

        public List<Sprite> AnimalTexture = new List<Sprite>();

        public List<GameObject> ShapesColliders = new List<GameObject>();

        public Tile Tile;
        public List<TilePrefabByEffect> TilePrefabByEffects = new List<TilePrefabByEffect>();

    }
}
