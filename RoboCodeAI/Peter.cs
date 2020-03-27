using System.Drawing;
using BehaviourTree;
using Robocode;

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
            const double hpSwitch1 = 50;
            const double hpSwitch2 = 30;

            var range = new FiringRange(75, 150, 200);

            var targetTree = new Sequence(
                new PerformScan(bb),
                new Selector(
                    new InTargetRange(bb, range),
                    new Sequence(
                        new TurnToTarget(bb),
                        new MoveToTarget(bb, range)
                    )
                ),
                new Selector(
                    new Invert(new InTargetRange(bb, range)),
                    new Sequence(
                        new TurnGunToTarget(bb),
                        new SuccessIfFailed(new AdjustGunDirectionForTargetVelocity(bb)),
                        new Fire(bb, Rules.MAX_BULLET_POWER)
                    )
                )
            );
            // Evasive tree is the most powerful one
            var evasiveTree = new Sequence(
                new PerformScan(bb),
                new MoveRandom(bb, -250, 250, true),
                new TurnRandom(bb, -100, 100, true),
                new TurnGunToTarget(bb, true),
                new ExecutePending(bb),
                new Selector(
                    new InTargetRange(bb, new FiringRange(0, 100, 250)),
                    new TargetStill(bb)
                ),
                new AdjustGunDirectionForTargetVelocity(bb),
                new Fire(bb, Rules.MAX_BULLET_POWER)
            );
            var frenzyTree = new Sequence(
                new Turn(bb, 360)
            );

            var targetTreeWrapper = new Sequence(new EnergyConditional(bb, hp => hp > hpSwitch1), new Sequence(
                new SetColor(bb, Color.Chartreuse), targetTree
            ));

            var evasiveTreeWrapper = new Sequence(new EnergyConditional(bb, hp => hp <= hpSwitch1), new Sequence(
                new SetColor(bb, Color.Aqua), evasiveTree
            ));

            // var frenzyTreeWrapper = new Sequence(new EnergyConditional(bb, hp => hp <= hpSwitch2), new Sequence(
            //     new SetColor(bb, Color.DarkRed), frenzyTree
            // ));

            return new Selector(targetTreeWrapper, evasiveTreeWrapper);
            // return evasiveTree;
        }
    }
}
