namespace BehaviourTree {
    public abstract class Decorator : BTNode {
        protected BTNode child;
        public BTNode Child => child;
    }
}
