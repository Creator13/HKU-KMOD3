namespace BehaviourTree {
    public abstract class CompositeNode : BTNode {
        protected BTNode[] children;
        public BTNode[] Children => children;
    }
}
