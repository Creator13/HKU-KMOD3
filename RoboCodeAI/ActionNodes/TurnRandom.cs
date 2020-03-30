using System;
using CVB;

namespace BehaviourTree {
    /// <summary>
    /// Turns the bot by a random number of degrees. Uses a smoothing of five frames, meaning that every five frames a
    /// new random value is generated.
    /// </summary>
    public class TurnRandom : Action {
        private readonly double min;
        private readonly double max;
        private readonly bool useParallel;

        private int tickCount;
        private double value;

        public TurnRandom(Blackboard bb, double min, double max, bool useParallel = false) : base(bb) {
            if (max < min) {
                throw new ArgumentException("Max value must be larger than min value.");
            }
            
            this.min = min;
            this.max = max;
            this.useParallel = useParallel;
        }

        public override NodeStatus Run() {
            // Only regenerate the value every five frames for smoother movement
            if (value == 0 || tickCount == 5) {
                value = Utils.RandInRange(min, max);
                tickCount = 0;
            }

            tickCount++;

            return new Turn(blackboard, value, useParallel).Run();
        }
    }
}
