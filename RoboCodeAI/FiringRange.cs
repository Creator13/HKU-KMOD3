namespace CVB {
    public class FiringRange {
        public readonly double minDistance;
        public readonly double targetDistance;
        public readonly double maxDistance;

        /// <param name="minDistance">Minimal distance from the bot to the target</param>
        /// <param name="targetDistance">Distance the bot will keep to its target</param>
        /// <param name="maxDistance">Maximal distance from the bot to the target</param>
        public FiringRange(double minDistance, double targetDistance, double maxDistance) {
            if (maxDistance < targetDistance) {
                throw new System.ArgumentException("Maximum distance cannot be smaller than target distance");
            }

            if (minDistance > targetDistance) {
                throw new System.ArgumentException("Minimum distance cannot be larger than target distance");
            }
            
            this.minDistance = minDistance;
            this.targetDistance = targetDistance;
            this.maxDistance = maxDistance;    
        }
    }
}
