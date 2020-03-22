namespace BehaviourTree {
    public class Selector : CompositeNode {
        public Selector(params BTNode[] children) {
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
