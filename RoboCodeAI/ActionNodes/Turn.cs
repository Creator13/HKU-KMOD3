using CVB;

namespace BehaviourTree {
    /// <summary>
    /// Turns the bot (left) by a specified angle.
    /// </summary>
    public class Turn : Action {
        private readonly double angle;
        private readonly bool useParallel;

        public Turn(Blackboard bb, double angle, bool useParallel = false) : base(bb) {
            this.angle = angle;
            this.useParallel = useParallel;
        }

        public override NodeStatus Run() {
            if (useParallel) {
                blackboard.robot.SetTurnLeft(angle);
            }
            else {
                blackboard.robot.TurnLeft(angle);
            }

            return NodeStatus.Success;
        }
    }
}
