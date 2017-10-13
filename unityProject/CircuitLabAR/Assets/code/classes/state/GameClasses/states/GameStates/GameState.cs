
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using state.GameClasses.Behiviors;
namespace state.GameClasses.states.GameStates{
    /**
     * 游戏状态机
     */
    public class GameState : BaseState {
        public GameBehivior game;

        /**
         * 游戏状态机
         */
        public GameState(GameBehivior g) {
            game = g;
        }

        

        public override void stateEnd()
        {
        }

        public override void stateInit()
        {
        }
        
        public override void stateUpdate()
        {
        }
    }
}