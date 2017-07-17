using FunBooksAndVideos.Model;

namespace FunBooksAndVideos.Repositories
{
    public interface ICustomerRepository
    {
        void ActivateMembership(int id, Membership membership);
    }
}
