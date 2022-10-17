namespace TaskBoardApp.Data
{
    public class DataConstants
    {
        public class User
        {
            public const int MaxUserFirstName = 15;
            public const int MaxUserLastName = 15;
            public const int MaxUserUsername = 30;
        }
        public class Task
        {
            public const int TitleMaxLength = 70;
            public const int TitleMixLength = 5;

            public const int DescriptionMaxLength = 1000;
            public const int DescriptionMixLength = 10;
        }
        public class Board
        {
            public const int NameMaxLength = 30;
            public const int NameMinLength = 3;
        }
    }
}
