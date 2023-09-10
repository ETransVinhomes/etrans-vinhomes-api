﻿using Domain.Enums;
using Domains.Entities;

namespace Domain.Entities
{
    public class Route : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Status { get; set; } = nameof(StatusEnum.Active);

        public Guid ProviderId { get; set; } = default!;
        public Provider Provider { get; set; } = default!;
        public ICollection<Trip> Trips { get; set; } = default!;
        public ICollection<RouteLocation> RouteLocations { get; set; } = default!;

    }
}
