
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using state.GameClasses.Behiviors;
using UnityEngine;
using UnityEngine.UI;

namespace state.GameClasses.states.GameStates
{
    /**
     * 
     */
    public class InitGameSate : GameState
    {
        public Button startButton;
        public InitGameSate(GameBehivior g) : base(g)
        {
            startButton = GameObject.Find("StartButton").GetComponent<Button>();
            //显示UI
            this.startButton.gameObject.transform.parent.localScale= new Vector3(1,1,1);
            startButton.onClick.AddListener(() =>
            {
                changeState(ref game.thisState, game.FirstFindingState);
            });
        }
        public override void stateInit()
        {
            
        }
        float colorf = 0.0f;
        Color bcolor = new Color();
        bool isUping = true;
        float speed = 0.02f;
        public override void stateUpdate()
        {
            //颜色闪烁
            if (isUping)
            {
                colorf = colorf + speed;
                if (colorf > 0.5f)
                {
                    colorf = 0.5f;
                    isUping= false;
                }
            }else{
                colorf = colorf - speed;
                if (colorf < -0.5f)
                {
                    colorf = -0.5f;
                    isUping = true;
                }
            }
            bcolor.r = this.startButton.gameObject.GetComponent<Image>().color.r;
            bcolor.g = this.startButton.gameObject.GetComponent<Image>().color.g;
            bcolor.b = this.startButton.gameObject.GetComponent<Image>().color.b;
            bcolor.a = colorf/2+0.75f;
            this.startButton.gameObject.GetComponent<Image>().color = bcolor;

            //位置变化的动画;
            this.startButton.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(
                colorf*100,
                this.startButton.gameObject.GetComponent<RectTransform>().localPosition.y,
                this.startButton.gameObject.GetComponent<RectTransform>().localPosition.z
            );
        }
        public override void stateEnd(){
            this.startButton.gameObject.transform.parent.localScale= new Vector3(0,0,0);
        }
    }

}