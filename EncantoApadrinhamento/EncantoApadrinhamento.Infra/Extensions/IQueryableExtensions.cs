using EncantoApadrinhamento.Domain.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EncantoApadrinhamento.Infra.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PaginationResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize, bool ignorePagination = false, CancellationToken cancellationToken = default)
        {
            var result = new PaginationResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                Total = await query.CountAsync(cancellationToken).ConfigureAwait(false)
            };

            if (ignorePagination)
            {
                result.PageSize = result.Total;
                result.Results = await query.ToListAsync(cancellationToken).ConfigureAwait(false);

                return result;
            }

            var skip = (page - 1) * pageSize;

            if (pageSize > 0)
            {
                result.Results = await query.Skip(skip).Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                result.Results = await query.Skip(skip).ToListAsync(cancellationToken).ConfigureAwait(false);
            }

            return result;
        }

        public static IQueryable<T> ApplyTracking<T>(this IQueryable<T> query, bool asNoTracking) where T : class
        {
            return asNoTracking ? query.AsNoTracking() : query;
        }

        public static IOrderedQueryable<T> OrderByDirection<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> expression, bool ascending = true) where T : class
        {
            return ascending ? source.OrderBy(expression)
                : source.OrderByDescending(expression);
        }

        /// <summary>
        /// Orders according to the specified option: asc, desc, or defaults to desc.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="query"></param>
        /// <param name="expression"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderByDirection<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> expression, string? order) where T : class
        {
            if (order == null)
                order = "desc";
            else
                order = order.ToLowerInvariant();

            return query.OrderByDirection(expression, order == "asc");
        }
    }
}
