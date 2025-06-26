namespace MC.IMS.API.Models.Request.Post;

public class PostAttachments
{
    public long ReferenceId { get; set; }
    public long AttachmentTypeId { get; set; }
    public string? ContentType { get; set; }
    public string FileName { get; set; } = string.Empty;
    public byte[] Attachment { get; set; } = [];
}