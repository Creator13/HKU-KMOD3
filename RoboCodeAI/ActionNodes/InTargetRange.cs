using CVB;

namespace BehaviourTree {
    public class InTargetRange : Action {
        private readonly FiringRange range;

        public InTargetRange(Blackboard bb, FiringRange range) : base(bb) {
            this.range = range;
        }

        /// <inheritdoc />
        public override NodeStatus Run() {
            var evt = blackboard.robot.LastScanEvent;
            if (evt == null) return NodeStatus.Failed;

            return evt.Distance > range.minDistance && evt.Distance < range.maxDistance ? NodeStatus.Success : NodeStatus.Failed;
        }
    }
}
