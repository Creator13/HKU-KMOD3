using System;
using CVB;

namespace BehaviourTree {
    public class TurnToTarget : Action {
        private readonly bool useParallel;
        public TurnToTarget(Blackboard bb, bool useParallel = false) : base(bb) {
            this.useParallel = useParallel;
        }

        public override NodeStatus Run() {
            var evt = blackboard.robot.LastScanEvent;

            // Can't turn without a valid scan event
            if (evt == null) return NodeStatus.Failed;

            // Allow error margin of 1 degree
            if (!(evt.Bearing > -1 && evt.Bearing < 1)) {
                if (evt.Bearing < 0) {
                    blackboard.robot.SetTurnLeft(Math.Abs(evt.Bearing));
                }
                else if (evt.Bearing > 0) {
                    blackboard.robot.SetTurnRight(Math.Abs(evt.Bearing));
                }
            }

            if (!useParallel) {
                blackboard.robot.Execute();
            }

            return NodeStatus.Success;
        }
    }
}
