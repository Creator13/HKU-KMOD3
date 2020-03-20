using CVB;

namespace BehaviourTree {
    public class Sequence : CompositeNode {
        private readonly Blackboard blackboard;
        
        public Sequence(Blackboard blackboard, params BTNode[] children) {
            this.blackboard = blackboard;
            this.children = children;
        }
        
        public override NodeStatus Run() {
            foreach (var child in children) {
                if (child.Run() == NodeStatus.Failed) {
                    return NodeStatus.Failed;
                }
            }

            return NodeStatus.Success;
        }
    }
}
