namespace BehaviourTree {
    public abstract class Composite : BTNode {
        protected BTNode[] children;
        public BTNode[] Children => children;

        protected Composite(params BTNode[] children) {
            this.children = children;
        }
    }
}
