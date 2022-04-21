using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Application.Implementation;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Infrastructure.EfCore
{
    public class CommentBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ICommentApplication, CommentApplication>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            services.AddDbContext<CommentContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
