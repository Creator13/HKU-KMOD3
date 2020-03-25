using CVB;
using Robocode;

namespace BehaviourTree {
    public class Fire : Action {
        public Fire(Blackboard bb) : base(bb) { }

        public override NodeStatus Run() {
            blackboard.robot.Fire(Rules.MAX_BULLET_POWER);
            return NodeStatus.Success;
        }
    }
}
