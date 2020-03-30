using System.Drawing;
using BehaviourTree;
using Robocode;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable FunctionNeverReturns

namespace CVB {
    public enum Direction {
        North,
        East,
        South,
        West
    }

    public class Peter : AdvancedRobot {
        public ScannedRobotEvent LastScanEvent { get; private set; }

        public Direction Direction {
            get {
                if (Heading > 315 && Heading < 360 || Heading > 0 && Heading < 45) {
                    return Direction.North;
                }
                else if (Heading > 45 && Heading < 135) {
                    return Direction.East;
                }
                else if (Heading > 135 && Heading < 225) {
                    return Direction.South;
                }
                else if (Heading > 225 && Heading < 315) {
                    return Direction.West;
                }

                return default;
            }
        }

        public override void Run() {
            ClearData();

            var bb = new Blackboard {
                robot = this
            };

            IsAdjustGunForRobotTurn = true;

            var bt = BuildBehaviorTree(bb);

            while (true) {
                bt.Run();
                ClearData();
            }
        }

        public override void OnScannedRobot(ScannedRobotEvent evt) {
            LastScanEvent = evt;
        }

        public override void OnWin(WinEvent evt) {
            SetTurnLeft(3600);
            SetTurnGunRight(3600);
            Execute();
        }

        public void ClearData() {
            LastScanEvent = null;
        }

        private static BTNode BuildBehaviorTree(Blackboard bb) {
            var distanceRange = new Range(75, 160, 200);
            var firingRange = new Range(0, 100, 200);

            var targetTree = new Sequence(
                new PerformScan(bb),
                new Selector(
                    // Try to fire, if that fails (because of not in range) move towards the target
                    new Sequence(
                        // May fire when either the target is in range or if it's not moving
                        new Selector(
                            new InTargetRange(bb, firingRange),
                            new TargetStill(bb)
                        ),
                        new TurnGunToTarget(bb),
                        new AdjustGunDirectionForTargetVelocity(bb),
                        new Fire(bb, Rules.MAX_BULLET_POWER)
                    ),
                    new Sequence(
                        // Move to target when out of preferred distance range
                        new Invert(new InTargetRange(bb, distanceRange)),
                        new TurnToTarget(bb),
                        new MoveToTarget(bb, distanceRange)
                    )
                )
            );
            
            // Evasive tree is the most powerful one
            var evasiveTree = new Sequence(
                new PerformScan(bb),
                // Do random movement
                new MoveRandom(bb, -250, 250, useParallel: true),
                new TurnRandom(bb, -100, 100, useParallel: true),
                // Keep gun aimed at target
                new TurnGunToTarget(bb, true),
                new ExecutePending(bb),
                // Fire if target either in range or standing still
                new Selector(
                    new InTargetRange(bb, firingRange),
                    new TargetStill(bb)
                ),
                new AdjustGunDirectionForTargetVelocity(bb),
                new Fire(bb, Rules.MAX_BULLET_POWER)
            );
            
            var distantRange = new Range(350, 351, 550);
            var distantTree = new Sequence(
                new PerformScan(bb),
                // Move away from target using a big range
                new SuccessIfFailed(new Sequence(
                    new InTargetRange(bb, distantRange),
                    new Sequence(
                        new TurnToTarget(bb, useParallel: true),
                        new MoveToTarget(bb, distantRange, useParallel: true),
                        new ExecutePending(bb)
                    )
                )),
                new Turn(bb, 90, useParallel: true),
                new Move(bb, 50, useParallel: true),
                new TurnGunToTarget(bb, useParallel: true),
                new ExecutePending(bb),
                new Selector(
                    new InTargetRange(bb, firingRange),
                    new TargetStill(bb)
                ),
                new AdjustGunDirectionForTargetVelocity(bb),
                new Fire(bb, Rules.MAX_BULLET_POWER)
            );

            const double hpSwitch1 = 80;
            const double hpSwitch2 = 20;

            var targetTreeWrapper = new Sequence(new EnergyConditional(bb, hp => hp > hpSwitch1), new Sequence(
                new SetColor(bb, Color.Chartreuse), targetTree
            ));

            var evasiveTreeWrapper = new Sequence(new EnergyConditional(bb, hp => hp <= hpSwitch1), new Sequence(
                new SetColor(bb, Color.Aqua), evasiveTree
            ));

            var distantTreeWrapper = new Sequence(new EnergyConditional(bb, hp => hp <= hpSwitch2), new Sequence(
                new SetColor(bb, Color.DarkRed), distantTree
            ));

            return new Selector(targetTreeWrapper, evasiveTreeWrapper, distantTreeWrapper);
            // return evasiveTree;
            // return targetTree;
            // return distantTree;
        }
    }
}
