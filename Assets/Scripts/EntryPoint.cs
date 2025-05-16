using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TripletsAnimalMatch
{
    public class EntryPoint : MonoBehaviour
    {
        private GamePresenter _gamePresenter;

        public void Construct(GamePresenter gamePresenter)
        {
            _gamePresenter = gamePresenter;
        }

        private void Awake()
        {
            
        }

        private void Start()
        {
            
        }
    }
}
