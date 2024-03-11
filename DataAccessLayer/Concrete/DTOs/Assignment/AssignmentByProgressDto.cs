namespace DataAccessLayer.Concrete.DTOs.Assignment
{
   public record AssignmentByProgressDto
    {
      
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public string Publisher { get; set; }
        public int SubTasks { get; set; }
        public int SubTasksDone { get; set; }
        public string Title { get; set; }

        public List<string> UserImagePaths { get; set; }
    }
}