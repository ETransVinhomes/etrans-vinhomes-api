﻿namespace Services.Interfaces
{
    public interface IClaimsService
    {
        public Guid GetCurrentUser { get; }
        public string GetEmail { get; }
    }
}
