namespace ViewModelLayer.ViewModels.Contact
{
    public record ContactDetailsViewModel
    {
        public int Id { get; set; }

        public string NameSurname { get; set; }

        public string EmailAddress { get; set; }

        public string Content { get; set; }

        public string Subject { get; set; }

        public bool IsSent { get; set; }
        public bool IsDraft { get; set; }
        public bool Status { get; set; }
        public string? To { get; set; }

        public DateTime Created { get; set; }

    }
}
