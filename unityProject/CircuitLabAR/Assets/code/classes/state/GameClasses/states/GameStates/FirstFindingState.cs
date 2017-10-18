
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using state.GameClasses.Behiviors;
using UnityEngine;
using DG.Tweening;
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
            game.helpPanel.SetActive(true);
            game.HelpkText.SetActive(false);
            game.PagekText.SetActive(true);
            game.MenuPanel.SetActive(true);
            game.MenuPanel.transform.DOMove (
                 new Vector3(-200,game.MenuPanel.transform.position.y,game.MenuPanel.transform.position.z),
                 1f
            );
            game.Blackboard.transform.localScale =  new Vector3(0,0,0);
            game.Blackboard.transform.DOScale (
                 new Vector3(1f,1f,1f),
                 1f
            );
        }
        public override void stateUpdate(){
            if(GameBehivior.TrackFound){
                changeState(ref game.thisState,game.MainGameState);
                return ;
            }
        }
        public override void stateEnd(){

            //隐藏UI
            game.HelpkText.SetActive(true);
            game.PagekText.SetActive(false);
        }
        
    }
}