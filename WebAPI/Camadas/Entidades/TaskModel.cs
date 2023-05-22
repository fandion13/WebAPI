using System;

namespace WebAPI.Camadas.Entidades
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Completed_at { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
    }
}