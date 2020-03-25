namespace BehaviourTree {
    public abstract class Decorator : BTNode {
        protected readonly BTNode child;

        protected Decorator(BTNode child) {
            this.child = child;
        }
    }
}
