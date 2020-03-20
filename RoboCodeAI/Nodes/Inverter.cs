namespace BehaviourTree {
    public class Inverter : Decorator {
        public Inverter(BTNode child) {
            this.child = child;
        }

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
