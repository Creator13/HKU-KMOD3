using CVB;
using Robocode;

namespace BehaviourTree {
    public class PerformScan : Action {
        public PerformScan(Blackboard bb) : base(bb) { }

        public override NodeStatus Run() {
            blackboard.robot.TurnRadarLeft(360);

            if (blackboard.robot.LastScanEvent != null) {
                return NodeStatus.Success;
            }
            else {
                return NodeStatus.Failed;
            }
        }
    }
}
