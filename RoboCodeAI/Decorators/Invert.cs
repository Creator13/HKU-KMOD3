namespace BehaviourTree {
    public class Invert : Decorator {
        public Invert(BTNode child) : base(child) { }

        public override NodeStatus Run() {
            var res = child.Run();
            return res switch {
                NodeStatus.Failed => NodeStatus.Success,
                NodeStatus.Success => NodeStatus.Failed,
                _ => res
            };
        }
    }
}
