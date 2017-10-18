
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using state.GameClasses.Behiviors;

namespace state.GameClasses.states.CircuitStates{
    /**
     *
     */
    public class CircuitState : BaseState {
        
        /**
         */

        public GameBehivior game;

        public CircuitState(GameBehivior g) {
            this.game = g;
        }

        

        public override void stateEnd()
        {
            throw new NotImplementedException();
        }

        public override void stateInit()
        {
            throw new NotImplementedException();
        }

        public override void stateUpdate()
        {
            throw new NotImplementedException();
        }
    }
}