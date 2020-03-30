using System;
using CVB;

namespace BehaviourTree {
    /// <summary>
    /// Moves the bot by a random amount. Uses a smoothing of five frames, meaning that every five frames a new random
    /// value is generated.
    /// </summary>
    public class MoveRandom : Action {
        private readonly double min;
        private readonly double max;
        private readonly bool useParallel;

        private int tickCount;
        private double value;

        public MoveRandom(Blackboard bb, double min, double max, bool useParallel = false) : base(bb) {
            if (max < min) {
                throw new ArgumentException("Max value must be larger than min value.");
            }
            
            this.min = min;
            this.max = max;
            this.useParallel = useParallel;
        }
        
        public override NodeStatus Run() {
            // Only regenerate a new random value every 5 frames, or if the value is 0.
            if (value == 0 || tickCount == 5) {
                value = Utils.RandInRange(min, max);
                tickCount = 0;
            }

            tickCount++;

            return new Move(blackboard, value, useParallel).Run();
        }
    }
}
