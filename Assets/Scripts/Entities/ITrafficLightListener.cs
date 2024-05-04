namespace Entities
{
    /// <summary>
    /// Interface implemented by everything that is affected by a traffic light.
    /// </summary>
    public interface ITrafficLightListener
    {
        public void Resume();
        public void Wait();
        public void SlowDown();
    }
}