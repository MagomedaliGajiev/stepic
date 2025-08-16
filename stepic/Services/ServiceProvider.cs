namespace stepic.Services;

public static class ServiceProvider
{
    public static IUsersService usersService = new EF.UsersService();
    public static ICoursesService coursesService = new EF.CoursesService();
    public static ICertificatesService certificatesService = new EF.CertificatesService();
    public static ICommentsService commentsService = new EF.CommentsService();
    public static UsersProcessing usersProcessing = new();
    public static WrongChoice wrongChoice = new();
}
