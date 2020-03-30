using CVB;

namespace BehaviourTree {
    /// <summary>
    /// Will fail when the bot is not in the specified range of the target.
    /// </summary>
    public class InTargetRange : Action {
        private readonly Range range;

        public InTargetRange(Blackboard bb, Range range) : base(bb) {
            this.range = range;
        }

        public override NodeStatus Run() {
            var evt = blackboard.robot.LastScanEvent;
            // Definitely not in range when no bot was scanned
            if (evt == null) return NodeStatus.Failed;

            return range.ValueInRange(evt.Distance) ? NodeStatus.Success : NodeStatus.Failed;
        }
    }
}
