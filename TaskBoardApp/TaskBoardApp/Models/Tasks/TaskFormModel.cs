using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Data.DataConstants.Task;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace TaskBoardApp.Models.Tasks
{
    public class TaskFormModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMixLength
            , ErrorMessage = "Should be at least {2} characters long.")]
        public string Title { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMixLength
            , ErrorMessage = "Should be at least {2} characters long.")]
        public string Description { get; set; }

        [DisplayName("Board")]
        public int BoardId { get; set; }

        public IEnumerable<TaskBoardModel> Boards { get; set; }
    }
}
