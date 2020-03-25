using System.Drawing;
using CVB;

namespace BehaviourTree {
    public class SetColor : Action {
        private Color color;

        public SetColor(Blackboard bb, Color c) : base(bb) {
            color = c;
        }

        public override NodeStatus Run() {
            blackboard.robot.BodyColor = color;
            return NodeStatus.Success;
        }
    }
}
