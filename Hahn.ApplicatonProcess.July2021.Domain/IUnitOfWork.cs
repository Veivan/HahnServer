using Hahn.ApplicatonProcess.July2021.Domain.Models;

namespace Hahn.ApplicatonProcess.July2021.Domain
{
    public interface IUnitOfWork
    {
        object UserRepository { get; set; }
    }
}
