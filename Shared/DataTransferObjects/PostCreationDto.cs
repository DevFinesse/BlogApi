namespace Shared.DataTransferObjects
{
    public record PostCreationDto(string Title, string Content, bool IsPublished, Guid CategoryId);
}
