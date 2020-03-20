using CVB;

namespace BehaviourTree {
    public class Selector : CompositeNode {
        private readonly Blackboard blackboard;
        
        public Selector(Blackboard blackboard, params BTNode[] children) {
            this.blackboard = blackboard;
            this.children = children;
        }
        
        public override NodeStatus Run() {
            foreach (var child in children) {
                if (child.Run() == NodeStatus.Success) {
                    return NodeStatus.Success;
                }
            }

            return NodeStatus.Failed;
        }
    }
}
