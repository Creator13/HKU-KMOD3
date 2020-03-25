using CVB;

namespace BehaviourTree {
    public class ExecutePending : Action {
        public ExecutePending(Blackboard bb) : base(bb) { }
        
        public override NodeStatus Run() {
            blackboard.robot.Execute();
            return NodeStatus.Success;
        }
    }
}
