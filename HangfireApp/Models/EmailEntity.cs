using System.ComponentModel.DataAnnotations;

namespace HangfireApp.Models
{
    public class EmailEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(64)]
        public string Subject { get; set; }

        [MaxLength(1024)]
        public string Body { get; set; }
    }
}
