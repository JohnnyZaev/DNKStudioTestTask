namespace MyEventBus
{
    public interface IDisposable
    {
        /// <summary>
        /// Unsubscribe from all events
        /// </summary>
        public void Dispose();
    }
}
