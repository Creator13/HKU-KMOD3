using CVB;

namespace BehaviourTree {
    public class MoveToTarget : Action {
        private readonly Range range;
        private readonly bool useParallel;

        public MoveToTarget(Blackboard bb, Range range, bool useParallel = false) : base(bb) {
            this.range = range;
            this.useParallel = useParallel;
        }

        public MoveToTarget(Blackboard bb, double distance) : base(bb) {
            range = new Range(distance - 50, distance, distance + 50);
        }

        public override NodeStatus Run() {
            var evt = blackboard.robot.LastScanEvent;

            // Can't move without valid target scan event
            if (evt == null) return NodeStatus.Failed;

            // Fail when the bot is not facing the target (allowance of 8deg)
            if (!(evt.Bearing > -8 && evt.Bearing < 8)) return NodeStatus.Failed;

            BTNode node;
            if (evt.Distance < range.minDistance) {
                // Move back if too close
                node = new Move(blackboard, range.minDistance - evt.Distance, useParallel);
            }
            else {
                // Move forward if too far
                node = new Move(blackboard, evt.Distance - range.targetDistance, useParallel);
            }

            node.Run();

            return NodeStatus.Running;
        }
    }
}
