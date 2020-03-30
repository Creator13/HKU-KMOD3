using CVB;

namespace BehaviourTree {
    /// <summary>
    /// Runs execute on all open robot actions.
    /// </summary>
    public class ExecutePending : Action {
        public ExecutePending(Blackboard bb) : base(bb) { }
        
        public override NodeStatus Run() {
            blackboard.robot.Execute();
            return NodeStatus.Success;
        }
    }
}
