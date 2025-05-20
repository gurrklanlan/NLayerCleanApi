using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace App.Persistance.Interceptors
{
    public class AuditDbContextInterceptor:SaveChangesInterceptor
    {
        private static readonly Dictionary<EntityState,Action<DbContext, IAuditEntity>> _behaviours = new()
        {
            { EntityState.Added, AddBehaviour },
            { EntityState.Modified, ModifiedBehaviour }
        };
        private static void AddBehaviour(DbContext context, IAuditEntity auditEntity)
        {
            auditEntity.Created = DateTime.Now;
            context.Entry(auditEntity).Property(x=>x.Updated).IsModified = false;
        }

        private static void ModifiedBehaviour(DbContext context, IAuditEntity auditEntity)
        {
            context.Entry(auditEntity).Property(x => x.Created).IsModified = false;
            auditEntity.Updated = DateTime.Now;
        }



        public override ValueTask<InterceptionResult<int>> 
        SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {

            foreach (var entityEntry in eventData.Context.ChangeTracker.Entries().ToList())
            {

                if (entityEntry.Entity is not IAuditEntity auditEntity)
                    continue;
                _behaviours[entityEntry.State](eventData.Context, auditEntity);





                //1.way
                //switch (entityEntry.State)
                //{
                //    case EntityState.Added:


                //        AddBehaviour(eventData.Context, (IAuditEntity)entityEntry.Entity);




                //        break;

                //    case EntityState.Modified:

                //        ModifiedBehaviour(eventData.Context, (IAuditEntity)entityEntry.Entity);
                //        break;


                //}









            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
