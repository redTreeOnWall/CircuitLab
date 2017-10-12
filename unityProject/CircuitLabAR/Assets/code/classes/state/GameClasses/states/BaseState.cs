
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace state.GameClasses.states{
    /**
     * 状态机基类
     */
    public abstract class BaseState {

        /**
         * 状态机基类
         */
        public BaseState() {
        }

        /**
         * 状态机名字
         */
        public string stateName;

        /**
         * 初始化状态机
         */
        public abstract void stateInit();

        public void stateloop(){
            this.stateUpdate();
        }
        /**
         * 每一帧循环
         */
        public abstract void stateUpdate();

        /**
         * 
         */
        public abstract void stateEnd();

        /**
         * @param nextState
         */
        public void changeState(ref BaseState thisState, BaseState nextState) {
            thisState.stateEnd();
            thisState = nextState;
            nextState.stateInit();
        }

    }
}