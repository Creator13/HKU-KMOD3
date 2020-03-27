using System;
using CVB;

namespace BehaviourTree {
    public class Move : Action {
        private readonly bool useParallel;
        private double distance;

        /// <param name="bb">Robot blackboard</param>
        /// <param name="distance">The distance the bot will move</param>
        /// <param name="useParallel">If true, this node will use the "set" variant of the move function (AdvancedRobot
        /// only)</param>
        public Move(Blackboard bb, double distance, bool useParallel = false) : base(bb) {
            this.distance = distance;
            this.useParallel = useParallel;
        }

        public override NodeStatus Run() {
            var bot = blackboard.robot;

            // Minimal wall bump prevention
            if (bot.Direction == Direction.East || bot.Direction == Direction.West) {
                if (bot.X + distance > bot.BattleFieldWidth - 100) {
                    // Heading towards east wall
                    if (bot.Direction == Direction.East && distance > 0 || bot.Direction == Direction.West && distance < 0) {
                        distance *= -1;
                    }
                }
                else if (bot.X - distance < 100) {
                    // Heading towards west wall
                    if (bot.Direction == Direction.West && distance > 0 || bot.Direction == Direction.East && distance < 0) {
                        distance *= -1;
                    }
                }
            }
            else {
                if (bot.Y + distance > bot.BattleFieldHeight - 100) {
                    // Heading towards north wall
                    if (bot.Direction == Direction.North && distance > 0 || bot.Direction == Direction.South && distance < 0) {
                        distance *= -1;
                    }
                }
                else if (bot.Y - distance < 100) {
                    // Heading towards south wall
                    if (bot.Direction == Direction.South && distance > 0 || bot.Direction == Direction.North && distance < 0) {
                        distance *= -1;
                    }
                }
            }

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
