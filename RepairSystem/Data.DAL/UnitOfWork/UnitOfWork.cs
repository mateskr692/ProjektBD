using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DAL.Repositories;

namespace Data.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private RepairSystemContext Context { get; set; }

        public IActivityRepository      Activities { get; }
        public IActivityTypeRepository  ActivityTypes { get; }
        public IClientRepository        Clients { get; }
        public IObjectRepository        Objects { get; }
        public IObjectTypeRepository    ObjectTypes { get; }
        public IPersonelRepository      Personel { get; }
        public IRequestRepository       Requests { get; }
        public IUserRepository          Users { get; }

        public UnitOfWork()
        {
            this.Context = new RepairSystemContext();

            this.Activities = new ActivityRepository( this.Context );
            this.ActivityTypes = new ActivityTypeRepository( this.Context );
            this.Clients = new ClientRepository( this.Context );
            this.Objects = new ObjectRepository( this.Context );
            this.ObjectTypes = new ObjectTypeRepository( this.Context );
            this.Personel = new PersonelRepository( this.Context );
            this.Requests = new RequestRepository( this.Context );
            this.Users = new UserRepository( this.Context );
        }


        public int Complete() => this.Context.SaveChanges();
        public void Dispose() => this.Context.Dispose();
    }
}
