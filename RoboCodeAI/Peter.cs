using BehaviourTree;
using Robocode;

// ReSharper disable FunctionNeverReturns

namespace CVB {
    public class Peter : AdvancedRobot {
        public override void Run() {
            var bb = new Blackboard {
                robot = this
            };

            var bt = new Sequence();
            
            while (true) {
                bt.Run();
            }
        }

        public override void OnWin(WinEvent evt) {
            SetTurnLeft(1800);
            SetTurnGunRight(3600);
            Execute();
        }
    }
}
