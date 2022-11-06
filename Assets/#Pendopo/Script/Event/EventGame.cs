using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Pendopo.Event
{
    public class EventGame : Misc.Singleton<EventGame>
    {
        private event Action<EnumGame> action;

        public EnumGame CurrentGame { get; private set; }

        /// <summary>
        /// Mengontrol controller yang aktif.
        /// Hanya satu controller yang dapat aktif dalam satu waktu.
        /// </summary>
        /// <param name="_game"></param>
        /// <param name="_lock"></param>
        public void Invoke(EnumGame _game)
        {
            action?.Invoke(_game);
            CurrentGame = _game;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_action"></param>
        public void AddListener(Action<EnumGame> _action)
        {
            action += _action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_action"></param>
        public void RemoveListener(Action<EnumGame> _action)
        {
            action -= _action;
        }
    }

    public interface IEventGame
    {
        /// <summary>
        /// Dipanggil ketika Trigger (EventController) dipanggil.
        /// </summary>
        /// <param name="_game"></param>
        /// <param name="_isLocked"></param>
        public void OnEventController(EnumGame _game);
    }
}