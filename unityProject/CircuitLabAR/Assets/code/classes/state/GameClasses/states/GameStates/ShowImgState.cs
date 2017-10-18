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
    public class ShowImgState : GameState
    {
        float initTime;
        public ShowImgState(GameBehivior g) : base(g)
        {
            
        }
        public override void stateInit(){
            game.showImgPanel.SetActive(true);
            this.initTime = Time.time;
        }
        public override void stateUpdate(){
            if(Time.time - initTime >= 1f){
                changeState(ref game.thisState,game.InitGameSate);
            }
        }
        public override void stateEnd(){
            game.showImgPanel.SetActive(false);
        }
    }
}