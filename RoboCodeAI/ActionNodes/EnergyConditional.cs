using System;
using CVB;

namespace BehaviourTree {
    public class EnergyConditional : Action {
        private readonly Predicate<double> condition;

        public EnergyConditional(Blackboard bb, Predicate<double> condition) : base(bb) {
            this.condition = condition;
        }

        public override NodeStatus Run() {
            var checkResult = condition.Invoke(blackboard.robot.Energy);
            return checkResult ? NodeStatus.Success : NodeStatus.Failed;
        }
    }
}
