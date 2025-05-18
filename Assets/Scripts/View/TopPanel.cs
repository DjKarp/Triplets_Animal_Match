using System;
using System.Linq;
using UnityEngine;
using DG.Tweening;


namespace TripletsAnimalMatch
{
    public class TopPanel : EnvironmentDynamic
    {
        [SerializeField] private Transform[] _tilesPositionContainer = new Transform[7];
        private Tile[] _tilesContainer;
        public Tile[] TilesContainer { get => _tilesContainer; }

        private bool[] _placesUse;

        public int PlaceUseCount { get => _placesUse.Count(x => x == true); }

        protected override void Init()
        {
            _tilesContainer = new Tile[7];
            _placesUse = new bool[7];

            HidePosition = StartPosition + new Vector3(10, 0, 0);
            Transform.position = HidePosition;
        }

        public override void Hide()
        {
            Tween = Transform
                .DOMoveX(HidePosition.x, AnimationTime)
                .From(StartPosition)
                .SetEase(Ease.InBounce);
        }

        public override void Show(Action callback = null)
        {
            Tween = Transform
                .DOMoveX(StartPosition.x, AnimationTime)
                .From(HidePosition)
                .SetEase(Ease.OutBounce);
        }

        public Vector3 GetNextTilePosition(Tile tile)
        {
            for (int i = 0; i < _tilesContainer.Length; i++)
            {
                if (_placesUse[i] == false)
                {
                    tile.TopPanelSlotIndex = i;
                    _placesUse[i] = true;
                    return _tilesPositionContainer[i].position;
                }
            }

            return Vector3.zero;
        }

        public void RemoveTileFromPanel(Tile tile, bool isDestroy = false)
        {
            for (int i = 0; i < _tilesContainer.Length; i++)
                if (_tilesContainer[i] == tile)
                {
                    if (isDestroy)
                        _tilesContainer[i].DestroyFromGamefield();
                    else
                        _tilesContainer[i] = null;

                    _placesUse[i] = false;
                }
        }

        public void AddedTileOnPanel(Tile tile, int number)
        {
            _tilesContainer[number] = tile;
        }

        public bool IsAllClickedTileMoveOnPanel()
        {
            return _tilesContainer.Count(x => x != null) == _placesUse.Count(x => x == true);
        }
    }
}
