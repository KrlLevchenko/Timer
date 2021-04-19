using System;

namespace Timer.Api.Home.GetTime
{
    public class Response
    {
        public DateTime Now { get; set; }
        public Guid ContainerId { get; set; }
    }
}