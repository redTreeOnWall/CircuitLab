
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using state.GameClasses.Behiviors;
using state.GameClasses.states.CircuitStates;
using UnityEngine;
using DG.Tweening;

namespace state.GameClasses.states.GameStates
{
    /**
     * 
     */

    public class MainGameState : GameState
    {

        //是否是第一次进入此状态
        bool isFirstInit = true;
        //黑板是否已经隐藏
        bool isBlackBoardHided = true;

        public Vector3 circuitCenterPosition = new Vector3(0.6009981f,0.108f,-21.30801f);
        public Vector3 circuitLeftPosition =  new Vector3(0.6009981f,0.108f,145.2f);
        public Vector3 circuitRightPosition = new Vector3(0.6009981f,0.108f,-209f);

        public MainGameState(GameBehivior g) : base(g)
        {
        }
        public override void stateInit()
        {
            if (isFirstInit)
            {
                firstInit();
                isFirstInit = false;
            }

            game.MainStatePanel.SetActive(true);

            //动画效果
            this.game.MenuPanel.transform.DOMove(
                new Vector3(
                    653,
                    game.MenuPanel.transform.position.y,
                    game.MenuPanel.transform.position.z
                ),
                1f
            );
            //    game.Blackboard.transform.localScale =  new Vector3(1,1,1);
            game.Blackboard.transform.DOScale(
                 new Vector3(0f, 0f, 0f),
                 1f
            );
        }
        public void firstInit()
        {
            //添加按钮事件
            //书本点击事件
            game.InstructionButton.onClick.AddListener(
                () =>
                {
                    if (isBlackBoardHided)
                    {
                        game.Blackboard.transform.DOScale(
                            new Vector3(1f, 1f, 1f),
                            1f
                        );
                        isBlackBoardHided = false;
                    }
                    else
                    {
                        game.Blackboard.transform.DOScale(
                            new Vector3(0f, 0f, 0f),
                            1f
                        );
                        isBlackBoardHided = true;
                    }
                }
            );
            //场景切换按钮点击事件
            //通路
            game.connectButton.onClick.AddListener(
                ()=>{
                    game.connectedCircuit.SetActive(true);
                    game.connectedCircuit.transform.position = new Vector3(
                        this.circuitLeftPosition.x,
                        this.circuitLeftPosition.y,
                        this.circuitLeftPosition.z
                    );
                    game.connectedCircuit.transform.DOMove(this.circuitCenterPosition,1);
                    game.disConnectedCircuit.transform.DOMove(this.circuitRightPosition,1).OnComplete(
                        ()=>{
                            game.disConnectedCircuit.SetActive(false);
                        }
                    );
                    game.shortCircuit.transform.DOMove(this.circuitRightPosition,1).OnComplete(
                        ()=>{
                            game.shortCircuit.SetActive(false);
                        }
                    );
                }
            );
            //开路
            game.disconneButton.onClick.AddListener(
                ()=>{
                    game.disConnectedCircuit.SetActive(true);
                    game.disConnectedCircuit.transform.position = new Vector3(
                        this.circuitLeftPosition.x,
                        this.circuitLeftPosition.y,
                        this.circuitLeftPosition.z
                    );
                    game.disConnectedCircuit.transform.DOMove(this.circuitCenterPosition,1);
                    game.connectedCircuit.transform.DOMove(this.circuitRightPosition,1).OnComplete(
                        ()=>{
                            game.connectedCircuit.SetActive(false);
                        }
                    );
                    game.shortCircuit.transform.DOMove(this.circuitRightPosition,1).OnComplete(
                        ()=>{
                            game.shortCircuit.SetActive(false);
                        }
                    );
                }
            );
            //短路
            game.shortButton.onClick.AddListener(
                ()=>{
                    game.shortCircuit.SetActive(true);
                    game.shortCircuit.transform.position = new Vector3(
                        this.circuitLeftPosition.x,
                        this.circuitLeftPosition.y,
                        this.circuitLeftPosition.z
                    );
                    game.shortCircuit.transform.DOMove(this.circuitCenterPosition,1);
                    game.disConnectedCircuit.transform.DOMove(this.circuitRightPosition,1).OnComplete(
                        ()=>{
                            game.disConnectedCircuit.SetActive(false);
                        }
                    );
                    game.connectedCircuit.transform.DOMove(this.circuitRightPosition,1).OnComplete(
                        ()=>{
                            game.connectedCircuit.SetActive(false);
                        }
                    );
                }
            );

        }
        public CircuitState newState(string className)
        {
            Type t = Type.GetType("state.GameClasses.states.CircuitStates." + className);
            CircuitState state = (CircuitState)Activator.CreateInstance(t, new GameBehivior[] { this.game });
            state.stateName = className;
            return state;
        }
        public override void stateUpdate()
        {
            if (!GameBehivior.TrackFound)
            {
                this.changeState(ref game.thisState, game.FirstFindingState);
            }
        }
    }
}