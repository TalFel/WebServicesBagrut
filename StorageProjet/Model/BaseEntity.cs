namespace Model
{
    public enum Status
    {
        NotOrdered,
        Ordered,
        InProcess,
        Ready,
        Given,
        Returned
    }
    public enum RequestStatus
    {
        Requested,
        InProcess,
        Completed,
        Deleted
    }
    public class BaseEntity
    {
        
    }
}