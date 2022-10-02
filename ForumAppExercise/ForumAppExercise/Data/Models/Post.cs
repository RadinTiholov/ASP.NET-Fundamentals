using System.ComponentModel.DataAnnotations;
using static ForumAppExercise.Data.DataConstants.Post;

namespace ForumAppExercise.Data.Models
{
    public class Post
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }
    }
}
