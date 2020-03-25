namespace BehaviourTree {
    public class SuccessIfFailed : Decorator {
        public SuccessIfFailed(BTNode child) : base(child) { }

        public override NodeStatus Run() {
            child.Run();
            return NodeStatus.Success;
        }
    }
}
