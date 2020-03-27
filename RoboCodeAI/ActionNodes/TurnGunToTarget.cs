using System;
using CVB;
using Robocode.Util;
using Utils = Robocode.Util.Utils;

namespace BehaviourTree {
    public class TurnGunToTarget : Action {
        private readonly bool useParallel;
        public TurnGunToTarget(Blackboard bb, bool useParallel = false) : base(bb) {
            this.useParallel = useParallel;
        }

        public override NodeStatus Run() {
            var evt = blackboard.robot.LastScanEvent;

            if (evt == null) return NodeStatus.Failed;

            // Calculate bearing from gun to target
            var absoluteBearing = blackboard.robot.Heading + evt.Bearing;
            var bearingFromGun = Utils.NormalRelativeAngleDegrees(absoluteBearing - blackboard.robot.GunHeading);

            // Turn left or right depending on what is shortest
            if (bearingFromGun < 0) {
                if (useParallel) {
                    blackboard.robot.SetTurnGunLeft(Math.Abs(bearingFromGun));
                }
                else {
                    blackboard.robot.TurnGunLeft(Math.Abs(bearingFromGun));
                }
            }
            else if (bearingFromGun > 0) {
                if (useParallel) {
                    blackboard.robot.SetTurnGunRight(Math.Abs(bearingFromGun));
                }
                else {
                    blackboard.robot.TurnGunRight(Math.Abs(bearingFromGun));
                }
            }

            return NodeStatus.Success;
        }
    }
}
