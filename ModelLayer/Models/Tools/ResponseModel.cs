namespace ViewModelLayer.Models.Tools
{
    public record ResponseModel
    {
        public bool success { get; set; }

        public string responseMessage { get; set; } = null!;
    }
}
