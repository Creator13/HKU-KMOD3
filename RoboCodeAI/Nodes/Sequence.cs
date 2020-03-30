namespace BehaviourTree {
    public class Sequence : Composite {
        public Sequence(params BTNode[] children) : base(children) { }

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
