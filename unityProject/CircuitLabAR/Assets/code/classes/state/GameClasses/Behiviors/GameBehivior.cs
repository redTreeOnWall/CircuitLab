
using state.GameClasses.states;
using state.GameClasses.states.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace state.GameClasses.Behiviors{
    /**
     * 游戏行为，继承MonoBehavior
     */
    
    public class GameBehivior :MonoBehaviour {

        public static bool TrackFound= false;
        public BaseState FirstFindingState;
        public BaseState InitGameSate;
        public BaseState MainGameState;
        public BaseState ReFindingGameState;
        
        //UI 一次全部载入，再一次全部隐藏;
        public GameObject firstFindingPanel;
        public GameObject initPanel;
        public GameObject MainStatePanel;
        public Dictionary<string,GameObject> uiobj;

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
        public BaseState thisState;
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