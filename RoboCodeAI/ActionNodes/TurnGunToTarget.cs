using System;
using CVB;
using Robocode.Util;

namespace BehaviourTree {
    public class TurnGunToTarget : Action {
        public TurnGunToTarget(Blackboard bb) : base(bb) { }

        public override NodeStatus Run() {
            var evt = blackboard.robot.LastScanEvent;

            if (evt == null) return NodeStatus.Failed;
            
            var absoluteBearing = blackboard.robot.Heading + evt.Bearing;
            var bearingFromGun = Utils.NormalRelativeAngleDegrees(absoluteBearing - blackboard.robot.GunHeading);

            if (bearingFromGun < 0) {
                blackboard.robot.TurnGunLeft(Math.Abs(bearingFromGun));
            }
            else if (bearingFromGun > 0) {
                blackboard.robot.TurnGunRight(Math.Abs(bearingFromGun));
            }
            
            return NodeStatus.Success;
        }
    }
}
