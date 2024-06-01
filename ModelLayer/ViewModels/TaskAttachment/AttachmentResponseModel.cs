namespace ViewModelLayer.ViewModels.TaskAttachment
{
    public record AttachmentResponseModel
    {
        public Guid Id { get; set; }

        public string Path { get; set; }

        public DateTime CreatedDate { get; set; }
    }

}
