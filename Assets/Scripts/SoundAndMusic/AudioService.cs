using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using FMOD.Studio;

namespace TripletsAnimalMatch
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        /// <summary>
        /// Music
        /// </summary>
        private string _gameplayMisic = "event:/Music/Misic";

        /// <summary>
        /// Sound
        /// </summary>        
        //Gameplay
        private string _clickOnTile = "event:/Sound/Gameplay/ClickOnTile";
        private string _explousenTile = "event:/Sound/Gameplay/ExplousenTile";
        private string _match = "event:/Sound/Gameplay/Match";
        private string _fallingLoop = "event:/Sound/Gameplay/Falling";

        public enum AudioGameplay
        {
            ClickOnTile,
            ExplousenTile,
            Match
        }

        //UI
        private string _clickOnButton = "event:/Sound/UI/ClickOnButton";
        private string _looseGame = "event:/Sound/UI/LooseGame";
        private string _winner = "event:/Sound/UI/Winner";

        public enum AudioUI
        {
            ClickOnButton,
            LooseGame,
            Winner
        }

        private EventInstance _musicEvent;
        private EventDescription _musicDes;
        private EventInstance _fallingEvent;

        private string _tempPath;

        private void Start()
        {
            StartMusic();
        }

        public void StartMusic()
        {
            _musicEvent = RuntimeManager.CreateInstance(_gameplayMisic);
            _musicEvent.set3DAttributes(RuntimeUtils.To3DAttributes(_camera.transform.position));
            _musicEvent.start();
        }

        public void StopMusic()
        {
            _musicEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        public void PlayGameplayAudio(AudioGameplay audioGameplay)
        {
            string soundEvent = _clickOnTile;

            switch(audioGameplay)
            {
                case AudioGameplay.ExplousenTile:
                    soundEvent = _explousenTile;
                    break;

                case AudioGameplay.Match:
                    soundEvent = _match;
                    break;
            }

            RuntimeManager.PlayOneShotAttached(soundEvent, _camera.gameObject);
        }

        public void PlayUIAudio(AudioUI audioUI)
        {
            string soundEvent = _clickOnButton;

            switch (audioUI)
            {
                case AudioUI.LooseGame:
                    soundEvent = _looseGame;
                    break;

                case AudioUI.Winner:
                    soundEvent = _winner;
                    break;
            }

            RuntimeManager.PlayOneShotAttached(soundEvent, _camera.gameObject);
        }

        public void StartFalling()
        {
            _fallingEvent = RuntimeManager.CreateInstance(_fallingLoop);
            _fallingEvent.set3DAttributes(RuntimeUtils.To3DAttributes(_camera.transform.position));
            _fallingEvent.start();
        }

        public void StopFalling()
        {
            _fallingEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }
}
