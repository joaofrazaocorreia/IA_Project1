using Locals;

namespace Entities
{
    public abstract class MobileAgent : Agent
    {
        private int _timer;

        public Destination initialDestination;
        private Destination _currentDestination;

        public abstract Destination ChooseRandomDestination(); //using DT
    }
}