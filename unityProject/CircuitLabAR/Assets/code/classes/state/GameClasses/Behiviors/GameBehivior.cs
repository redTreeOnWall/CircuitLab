
using state.GameClasses.states;
using state.GameClasses.states.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace state.GameClasses.Behiviors
{
    /**
     * 游戏行为，继承MonoBehavior
     */

    public class GameBehivior : MonoBehaviour
    {

        //是否找到图像
        public static bool TrackFound = false;
        //所有游戏状态
        public BaseState ShowImgState;
        public BaseState FirstFindingState;
        public BaseState InitGameSate;
        public BaseState MainGameState;
        public BaseState ReFindingGameState;

        //UI 一次全部载入，再一次全部隐藏;
        public GameObject firstFindingPanel;
        public GameObject initPanel;
        public GameObject MainStatePanel;
        public GameObject helpPanel;
        public GameObject showImgPanel;
        public GameObject PagekText;
        public GameObject HelpkText;
        public GameObject Blackboard;
        public GameObject MenuPanel;
        public Button InstructionButton;

        public Button disconneButton;
        public Button connectButton;
        public Button shortButton;
        

        public Dictionary<string, GameObject> uiobj;

        //所有的电路场景
        //通路
        public GameObject connectedCircuit;
        //开路
        public GameObject disConnectedCircuit;
        //短路
        public GameObject shortCircuit;

        //配置文件对象；
        public GameObject properties;


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
            //创建所有状态
            FirstFindingState = newState("FirstFindingState");
            InitGameSate = newState("InitGameSate");
            MainGameState = newState("MainGameState");
            ReFindingGameState = newState("ReFindingGameState");
            ShowImgState = newState("ShowImgState");
            //隐藏所有电路场景
            //    this.connectedCircuit.SetActive(false);
            this.disConnectedCircuit.SetActive(false);
            this.shortCircuit.SetActive(false);
            //给draw的材质赋值
            draw.line0M = properties.GetComponent<MeshRenderer>().materials[0];
            draw.line2M = properties.GetComponent<MeshRenderer>().materials[1];

            //隐藏所有UI
            firstFindingPanel.SetActive(false);
            initPanel.SetActive(false);
            MainStatePanel.SetActive(false);
            helpPanel.SetActive(false);
            showImgPanel.SetActive(false);
            PagekText.SetActive(false);
            HelpkText.SetActive(false);
            MenuPanel.SetActive(false);
            //初始化状态

            thisState = ShowImgState;
            thisState.changeState(ref thisState,ShowImgState);

            //test
          //  Debug.Log("UI aim pition.x:"+this.MenuPanel.transform.position.x);
        }
        void Update()
        {
            thisState.stateloop();
        }
    }



}