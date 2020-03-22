using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DAL.Repositories;

namespace Data.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //Repositories
        IActivityRepository     Activities { get; }
        IActivityTypeRepository ActivityTypes { get; }
        IClientRepository       Clients { get; }
        IObjectRepository       Objects { get; }
        IObjectTypeRepository   ObjectTypes { get; }
        IPersonelRepository     Personel { get; }
        IRequestRepository      Requests { get; }
        IUserRepository         Users { get; }

        //Methods
        int Complete();
    }
}
