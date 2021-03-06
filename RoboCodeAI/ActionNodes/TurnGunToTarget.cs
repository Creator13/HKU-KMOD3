﻿using System;
using CVB;
using Utils = Robocode.Util.Utils;

namespace BehaviourTree {
    /// <summary>
    /// Turns the gun directly towards the target.
    /// </summary>
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
                    blackboard.robot.SetTurnGunLeft(Math.Abs(bearingFromGun));
            }
            else if (bearingFromGun > 0) {
                    blackboard.robot.SetTurnGunRight(Math.Abs(bearingFromGun));
            }

            if (!useParallel) {
                blackboard.robot.Execute();
            }

            return NodeStatus.Success;
        }
    }
}
