namespace TaskBoardApp.Models
{
    public class HomeViewModel
    {
        public int AllTasksCount { get; set; }
        public int UserTasksCount { get; set; }
        public IList<HomeBoardModel> BoardsWithTasksCount { get; set; }
    }
}
