using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace homework1.Models
{
    public partial class ContosoUniversityContext : DbContext
    {
        public override int SaveChanges()
        {
            var entities = this.ChangeTracker.Entries();

            foreach (var entry in entities)
            {
                Console.WriteLine("Entity Name: {0}", entry.Entity.GetType().FullName);
                Console.WriteLine("Status: {0}", entry.State);

                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues.SetValues(new { IsDeleted = true });
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.CurrentValues.SetValues(new { ModifiedOn = DateTime.Now });
                }
            }
            return base.SaveChanges();
        }
    }
}