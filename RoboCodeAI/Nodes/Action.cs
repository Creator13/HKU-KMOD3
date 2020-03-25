using CVB;

namespace BehaviourTree {
    public abstract class Action : BTNode {
        protected readonly Blackboard blackboard;

        protected Action(Blackboard blackboard) {
            this.blackboard = blackboard;
        }
    }
}
