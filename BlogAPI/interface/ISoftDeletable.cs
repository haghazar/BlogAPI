namespace BlogAPI.@interface
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }

        //DateTime? DeletedOnUtc { get; set; }
    }
}
