namespace BehaviourTree {
    public enum NodeStatus {
        Success,
        Failed,
        Running
    }

    public abstract class BTNode {
        public abstract NodeStatus Run();
    }
}
