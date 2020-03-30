namespace BehaviourTree {
    /// <summary>
    /// Consistently turns failure into success.
    /// </summary>
    public class AlwaysSuccess : Decorator {
        public AlwaysSuccess(BTNode child) : base(child) { }

        public override NodeStatus Run() {
            child.Run();
            return NodeStatus.Success;
        }
    }
}
