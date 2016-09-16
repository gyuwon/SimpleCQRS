using System;
using System.Linq;
using SimpleCQRS.Models.Data;
using SimpleCQRS.Models.Presentation;

namespace SimpleCQRS.ReadModel
{
    public static class LetExtensions
    {
        public static IQueryable<User> FilterById(
            this IQueryable<User> query, Guid userId)
        {
            if (null == query)
                throw new ArgumentNullException(nameof(query));

            return query.Where(e => e.Id == userId);
        }

        public static IQueryable<UserPresentation> ToPresentation(
            this IQueryable<User> query)
        {
            if (null == query)
                throw new ArgumentNullException(nameof(query));

            return from e in query
                   select new UserPresentation
                   {
                       Id = e.Id,
                       UserName = e.UserName
                   };
        }
    }
}
