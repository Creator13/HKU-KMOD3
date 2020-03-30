using CVB;

namespace BehaviourTree {
    /// <summary>
    /// Run a scan.
    /// </summary>
    public class PerformScan : Action {
        private readonly double angle;
        private readonly bool relative;

        public PerformScan(Blackboard bb, double angle = 360, bool relative = false) : base(bb) {
            this.angle = angle;
            this.relative = relative;
        }

        public override NodeStatus Run() {
            if (!relative) {
                blackboard.robot.TurnRadarLeft(angle);
            }
            else {
                blackboard.robot.TurnRadarLeft(angle / 2);
                blackboard.robot.TurnRadarRight(angle);
                blackboard.robot.TurnRadarLeft(angle / 2);
            }

            if (blackboard.robot.LastScanEvent != null) {
                return NodeStatus.Success;
            }
            else {
                return NodeStatus.Failed;
            }
        }
    }
}
