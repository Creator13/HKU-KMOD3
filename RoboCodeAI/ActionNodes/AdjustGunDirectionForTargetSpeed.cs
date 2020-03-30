using System;
using CVB;
using Robocode;
using Utils = Robocode.Util.Utils;

namespace BehaviourTree {
    /// <summary>
    /// Turns the gun predicting the position of the target in the next frame.
    /// </summary>
    public class AdjustGunDirectionForTargetVelocity : Action {
        public AdjustGunDirectionForTargetVelocity(Blackboard bb) : base(bb) { }

        public override NodeStatus Run() {
            var evt = blackboard.robot.LastScanEvent;
            if (evt == null) return NodeStatus.Failed;

            // Calculation stolen from http://robowiki.net/wiki/Linear_Targeting
            var bulletPower = 3;
            var headOnBearing = blackboard.robot.HeadingRadians + evt.BearingRadians;
            var linearBearing = headOnBearing + Math.Asin(evt.Velocity / Rules.GetBulletSpeed(bulletPower) * Math.Sin(evt.HeadingRadians - headOnBearing));
            blackboard.robot.TurnGunRightRadians(Utils.NormalRelativeAngle(linearBearing - blackboard.robot.GunHeadingRadians));

            return NodeStatus.Success;
        }
    }
}
