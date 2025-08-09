namespace stepic.Services
{
    public static class ServiceProvider
    {
        public static IUsersService usersService = new ADO.NET.UsersService();
        public static ICoursesService coursesService = new ADO.NET.CoursesService();
        public static ICertificatesService certificatesService = new ADO.NET.CertificatesService();
        public static ICommentsService commentsService = new ADO.NET.CommentsService();
        public static UsersProcessing usersProcessing = new();
        public static WrongChoice wrongChoice = new();
    }
}
