using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtBloger.Data.Abstract
{
    public interface IUnitOfWork :IAsyncDisposable
    {
        IArticleRepository Articles { get; }
        ICommentRepository Comments { get; }
        IRoleRepository Roles { get; }
        ICategoryRepository Categories { get; }
        IUserRepository Users { get; }
        Task<int> SaveAsync();
    }
}
