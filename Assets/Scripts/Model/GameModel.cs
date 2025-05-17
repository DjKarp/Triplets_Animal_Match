using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class GameModel : MonoBehaviour
    {
        private GamePresenter _gamePresenter;
        private FishkiData _fishkiData;
        private GameplayData _gameplayData;
        private TopPanel _topPanel;
        private SignalBus _signalBus;

        private Transform _transform;

        private List<Fishka> _createFishkiList = new List<Fishka>();

        [Inject]
        public void Construct(GamePresenter gamePresenter, FishkiData fishkiData, GameplayData gameplayData, TopPanel topPanel, SignalBus signalBus)
        {
            _gamePresenter = gamePresenter;
            _fishkiData = fishkiData;
            _gameplayData = gameplayData;
            _topPanel = topPanel;

            _signalBus = signalBus;
        }

        private void Awake()
        {
            _transform = gameObject.transform;
        }

        public List<Fishka> GetCreatePoolFishek()
        {
            _createFishkiList.Clear();
            List<Fishka> fishki = new List<Fishka>();
            Fishka fishka;
            var random = new System.Random();

            List<FishkaModel> fishkaModels = new List<FishkaModel>(CreateUniqueFishkaModels());

            foreach (FishkaModel model in fishkaModels)
            {
                fishka = null;
                for (int i = 0; i < _gameplayData.FishkiCountOnMatch; i++)
                {
                    fishka = Instantiate(_fishkiData.Fishka, _transform);
                    fishka.Init(model, GetShapeSprite((int)model.Shape, (int)model.Color), _fishkiData.AnimalTexture[(int)model.AnimalType], _fishkiData.ShapesColliders[(int)model.Shape], _signalBus);
                    fishka.gameObject.SetActive(false);
                    fishki.Add(fishka);
                }
            }

            _createFishkiList = fishki.OrderBy(_ => random.Next()).ToList();

            return _createFishkiList;
        }

        private List<FishkaModel> CreateUniqueFishkaModels()
        {
            int tempUniqueFishkiCount = _gameplayData.FishkiMaxCountOnScene / _gameplayData.FishkiCountOnMatch;
            FishkaModel fishkaModel;
            List<FishkaModel> _fishkiModels = new List<FishkaModel>();
            var random = new System.Random();

            for (int i = 0; i < tempUniqueFishkiCount; i++)
            {
                do
                {
                    fishkaModel = new FishkaModel(
                        (FishkiData.Shape)random.Next(0, System.Enum.GetNames(typeof(FishkiData.Shape)).Length),
                        (FishkiData.Color)random.Next(0, System.Enum.GetNames(typeof(FishkiData.Color)).Length),
                        (FishkiData.AnimalType)random.Next(0, System.Enum.GetNames(typeof(FishkiData.AnimalType)).Length));
                }
                while (_fishkiModels.Contains(fishkaModel));
                
                _fishkiModels.Add(fishkaModel);
            }

            return _fishkiModels;
        }        

        private Sprite GetShapeSprite(int shapeNumber, int colorNumber)
        {
            switch (shapeNumber)
            {
                default:
                case 0:
                    return _fishkiData.ShapesCircle[colorNumber];

                case 1:
                    return _fishkiData.ShapesHexagon[colorNumber];

                case 2:
                    return _fishkiData.ShapesPentagon[colorNumber];

                case 3:
                    return _fishkiData.ShapesRectangle[colorNumber];
            }
        }
    }
}
