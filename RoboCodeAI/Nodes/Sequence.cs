namespace BehaviourTree {
    public class Sequence : CompositeNode {
        public Sequence(params BTNode[] children) {
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
