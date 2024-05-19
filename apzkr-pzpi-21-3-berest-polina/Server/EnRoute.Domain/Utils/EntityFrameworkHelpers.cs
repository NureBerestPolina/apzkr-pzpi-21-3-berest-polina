using EnRoute.Domain.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Domain.Utils
{
    public static class EntityFrameworkHelpers
    {
        public static void DetachLocal<T>(this ApplicationDbContext context, T t, Guid entryId)
            where T : class, IODataEntity
        {
            var local = context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entryId));

            if (local is not null)
            {
                context.Entry(local).State = EntityState.Detached;
            }

            context.Entry(t).State = EntityState.Modified;
        }
    }
}
