﻿using CVB;

namespace BehaviourTree {
    /// <summary>
    /// Fires a bullet with a specified power.
    /// </summary>
    public class Fire : Action {
        private readonly double power;

        public Fire(Blackboard bb, double power) : base(bb) {
            this.power = power;
        }

        public override NodeStatus Run() {
            // Do not fire if found no target was scanned
            if (blackboard.robot.LastScanEvent == null) return NodeStatus.Failed;

            blackboard.robot.Fire(power);
            return NodeStatus.Success;
        }
    }
}
