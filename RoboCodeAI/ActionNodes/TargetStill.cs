using CVB;

namespace BehaviourTree {
    /// <summary>
    /// Will run successfully if the target does not move at all (velocity == null).
    /// </summary>
    public class TargetStill : Action {
        public TargetStill(Blackboard bb) : base(bb) { }

        public override NodeStatus Run() {
            if (blackboard.robot.LastScanEvent != null) {
                if (blackboard.robot.LastScanEvent.Velocity == 0) {
                    return NodeStatus.Success;
                }
            }

            return NodeStatus.Failed;
        }
    }
}
