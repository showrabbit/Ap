using UnityEngine;
using System.Collections;
using Ap.Core;
using Ap.Event;

namespace Ap.Game
{
    /// <summary>
    /// 游戏状态
    /// </summary>
    public enum GameStatus
    {
        Start,
        Pause,
        Over
    }


    /// <summary>
    /// 游戏管理
    /// 负责游戏的暂停,继续,结束
    /// 管理游戏的运行中的层级节点
    /// </summary>
    public class GameManager : ManagerBase<GameManager>
    {

        public GameStatus State
        {
            set
            {
                mState = value;
            }
            get
            {
                return mState;
            }
        }
        private GameStatus mState = GameStatus.Pause;

        protected override void Init()
        {
            

        }

        public void OnGameOver(object sender, EventData data)
        {
            //todo处理游戏结束
            mState = GameStatus.Over;
            EventManager.Instance.Trigge(EventTypes.GameOver, this, new EventData());

        }
        /// <summary>
        /// 处理游戏暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public void OnGamePause(object sender, EventData data)
        {
            mState = GameStatus.Pause;
            EventManager.Instance.Trigge(EventTypes.GamePause, this, new EventData());

        }
        /// <summary>
        /// 处理游戏开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public void OnGameStart(object sender, EventData data)
        {
            mState = GameStatus.Start;
            EventManager.Instance.Trigge(EventTypes.GameStart, this, new EventData());

        }

      
    }

}
