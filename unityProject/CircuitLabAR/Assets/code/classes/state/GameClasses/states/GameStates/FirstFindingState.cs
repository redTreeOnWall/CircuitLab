
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
    public class FirstFindingState : GameState
    {
        public FirstFindingState(GameBehivior g) : base(g)
        {
        }

        public override void stateInit(){
            //显示ui
            game.firstFindingPanel.transform.localScale=new Vector3(1,1,1);

        }
        public override void stateUpdate(){
            if(GameBehivior.TrackFound){
                changeState(ref game.thisState,game.MainGameState);
                return ;
            }
        }
        public override void stateEnd(){

            //隐藏UI
            game.firstFindingPanel.transform.localScale=new Vector3(0,0,0);
        }
        
    }
}