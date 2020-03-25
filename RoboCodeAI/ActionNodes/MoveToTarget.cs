using CVB;

namespace BehaviourTree {
    public class MoveToTarget : Action {
        private readonly FiringRange range;

        public MoveToTarget(Blackboard bb, FiringRange range) : base(bb) {
            this.range = range;
        }

        public MoveToTarget(Blackboard bb, double distance) : base(bb) {
            range = new FiringRange(distance - 50, distance, distance + 50);
        }

        public override NodeStatus Run() {
            var evt = blackboard.robot.LastScanEvent;

            // Can't move without valid target scan event
            if (evt == null) return NodeStatus.Failed;

            // Fail when the bot is not facing the target (allowance of 8deg)
            if (!(evt.Bearing > -8 && evt.Bearing < 8)) return NodeStatus.Failed;

            if (evt.Distance < range.minDistance) {
                // Move back if too close
                blackboard.robot.Back(range.minDistance - evt.Distance);
            }
            else {
                // Move forward if too far
                blackboard.robot.Ahead(evt.Distance - range.targetDistance);
            }

            return NodeStatus.Running;
        }
    }
}
