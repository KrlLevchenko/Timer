using System;

namespace Timer
{
    public class AppContainer
    {
        public AppContainer()
        {
            Id = Guid.NewGuid();
            IsAvailable = true;
        }

        public bool IsAvailable { get; set; }
        public Guid Id { get; }
        
    }
}