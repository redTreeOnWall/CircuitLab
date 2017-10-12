
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace state.GameClasses.Behiviors{
    /**
     * 游戏行为，继承MonoBehavior
     */
    public class GameBehivior {

        /**
         * 游戏行为，继承MonoBehavior
         */
        public GameBehivior() {
        }

        /**
         * 
         */
        public List<GameState> gameStateList;

        /**
         * 
         */
        public GameState thisState;

    }
}