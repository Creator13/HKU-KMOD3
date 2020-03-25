using System;
using CVB;

namespace BehaviourTree {
    public class Move : Action {
        private readonly bool useParallel;
        private double distance;

        public Move(Blackboard bb, double distance, bool useParallel = false) : base(bb) {
            this.distance = distance;
            this.useParallel = useParallel;
        }
        
        public override NodeStatus Run() {
            var bot = blackboard.robot;
            
            // if ((bot.Heading > 45 && bot.Heading < 135) || (bot.Heading > 225 && bot.Heading < 315)) {
                if (bot.X + Math.Abs(distance) > bot.BattleFieldWidth - 25) {
                    // distance = bot.BattleFieldWidth - bot.X;
                    distance *= -1;
                }
                else if (bot.X - Math.Abs(distance) < 25) {
                    // distance = bot.X;
                    distance *= -1;
                }
            // }
            // else {
                if (bot.Y + Math.Abs(distance) > bot.BattleFieldHeight - 25) {
                    // distance = bot.BattleFieldHeight - bot.Y;
                    distance *= -1;
                }
                else if (bot.Y - Math.Abs(distance) < 25) {
                    // distance = bot.Y;
                    distance *= -1;
                }
            // }

            if (useParallel) {
                blackboard.robot.SetAhead(distance);
            }
            else {
                blackboard.robot.Ahead(distance);
            }
            
            return NodeStatus.Success;
        }
    }
}
