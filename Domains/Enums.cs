namespace Domain.Enums
{
    public enum StatusEnum
    {
        InActive = 0,
        Active = 1,

    }

    // Enum for Vehicle, Driver
    public enum TransportationStatusEnum
    {
        InActive = 0,
        Active = 1,
        OnGoing = 2
    }

    public enum TripStatusEnum
    {
        InActive = 0,
        Active = 1,
        OnGoing = 2,
        Full = 3,// Full but not start yet,
        Finished = 4
    }

    public enum TransactionStatusEnum
    {
        Created = 0,
        Failed = 1,
        Completed = 2,
    }
    public enum EventType
    {
        UserCreation,
        UserDelete,
        UserModified,
        Undefined
    }

    public enum RoleEnum
    {
        Customer,
        Provider,
        Driver
    }
}
