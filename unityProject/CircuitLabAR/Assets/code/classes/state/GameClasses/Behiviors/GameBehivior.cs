
using state.GameClasses.states;
using state.GameClasses.states.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace state.GameClasses.Behiviors{
    /**
     * 游戏行为，继承MonoBehavior
     */
    
    public class GameBehivior :MonoBehaviour {
        public GameState FirstFindingState;
        public GameState InitGameSate;
        public GameState MainGameState;
        public GameState ReFindingGameState;


        public GameState newState(string className)
        {
            Type t = Type.GetType("state.GameClasses.states.GameStates." + className);
            GameState state = (GameState)Activator.CreateInstance(t, new GameBehivior[] { this });
            state.stateName = className;
            return state;
        }

        /**
         * 
         */
        public List<GameState> gameStateList;

        /**
         * 
         */
        public GameState thisState;
        void Start()
        {
            FirstFindingState = newState("FirstFindingState");
            InitGameSate = newState("InitGameSate");
            MainGameState = newState("MainGameState");
            ReFindingGameState = newState("ReFindingGameState");
            thisState = InitGameSate;
        }
        void Update()
        {
            thisState.stateloop();
        }
    }
}