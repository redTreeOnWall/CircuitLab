
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using state.GameClasses.Behiviors;
using UnityEngine;

namespace state.GameClasses.states.GameStates{
    /**
     * 
     */
    public class MainGameState : GameState
    {
        
        public MainGameState(GameBehivior g) : base(g)
        {
        }
        public override void stateInit(){
            game.MainStatePanel.transform.localScale = new Vector3(1,1,1);
        }
        public override void stateUpdate(){
            
        }
    }
}