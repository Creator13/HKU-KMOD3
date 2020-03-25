using System.Drawing;
using BehaviourTree;
using Robocode;

// ReSharper disable FunctionNeverReturns

namespace CVB {
    public class Peter : AdvancedRobot {
        public ScannedRobotEvent LastScanEvent { get; private set; }

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
            const double hpSwitch1 = 75;
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
                        new Fire(bb)
                    )
                )
            );
            var evasiveTree = new Sequence();
            var frenzyTree = new Sequence();

            var targetTreeWrapper = new Sequence(new EnergyConditional(bb, hp => hp > hpSwitch1), new Sequence(
                new SetColor(bb, Color.Chartreuse), targetTree
            ));

            var evasiveTreeWrapper = new Sequence(new EnergyConditional(bb, hp => hp <= hpSwitch1 && hp > hpSwitch2), new Sequence(
                new SetColor(bb, Color.Aqua), evasiveTree
            ));

            var frenzyTreeWrapper = new Sequence(new EnergyConditional(bb, hp => hp <= hpSwitch2), new Sequence(
                new SetColor(bb, Color.DarkRed), frenzyTree
            ));

            // var bt = new Sequence(new SetColor(bb, Color.Aqua), new PerformScan(bb));
            // return new Selector(targetTreeWrapper, evasiveTreeWrapper, frenzyTreeWrapper);
            return targetTree;
        }
    }
}
