using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Api.Core.Dtos;
using Api.Core.Entities;
using Api.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Api.Core.Dtos.Common;

namespace Api.Core.Admin
{
    public abstract class BaseAdmin<TID, TE, TD, TF>
        where TF : FilterBase
        where TE : EntityBase<TID>
    {
        public MyContext MyContext;

        public IMapper Mapper;
        public string UsuarioLogged { get; set; }

        public UsuarioAuth CurrentUser { get; set; }
        private AuthAdmin auth;

        //public UserManager<ApplicationUser> _userManager;

        public BaseAdmin(MyContext context, AppSettings appSettings) : this()
        {
            MyContext = context;
            Mapper = new Mapper(BootStrapper.MapperConfiguration);
            auth = new AuthAdmin(appSettings, context);

        }

        public BaseAdmin()
        {
            Mapper = new Mapper(BootStrapper.MapperConfiguration);
        }

        public virtual TD GetById(TID id)
        {
            var query = MyContext.Set<TE>().Include(MyContext.GetIncludePaths(typeof(TE)));
            var entity = query.Where(e => e.Id.Equals(id)).FirstOrDefault();

            return Mapper.Map<TE, TD>(entity);
        }

        public virtual IList<TD> GetAll()
        {
            var entities = (IList<TE>)MyContext.Set<TE>().AsQueryable().OfType<TE>().ToList();
            return Mapper.Map<IList<TE>, IList<TD>>(entities);
        }

        public virtual PagedListResponse<TD> GetByFilter(TF filter)
        {
            var Filter = typeof(TE);
            var query = GetQuery(filter).OfType<TE>();
            query = query.Where(e => !e.Deleted);

            var pageSize = filter.PageSize ?? 10;
            var currentPage = filter.CurrentPage ?? 1;

            try
            {
                var data = query.Skip(pageSize * (currentPage - 1)).Take(pageSize).ToList();

                return new PagedListResponse<TD>
                {
                    Count = query.Count(),
                    Data = Mapper.Map<IList<TE>, IList<TD>>(data)
                };
            }
            catch (Exception ex)
            {
                return new PagedListResponse<TD>
                {
                    Count = 0,
                    Data = new List<TD>()
                };
            }
            

        }

        public virtual TD Create(TD dto)
        {
            var x = dto;
            var entity = ToEntity(dto);

            entity.Enabled = true;
            entity.CreateDate = DateTime.Now;
            entity.UpdateDate = DateTime.Now;
            entity.CreatedBy = CurrentUser.user_id;

            MyContext.Set<TE>().Add(entity);
            MyContext.SaveChanges();

            return Mapper.Map<TE, TD>(entity);

        }

        public virtual TD Update(TD dto)
        {
            //Validate(dto); //gvillafuerte - implementar desde controller
            var entity = ToEntity(dto);

            entity.UpdateDate = DateTime.Now;
            entity.UpdatedBy = CurrentUser?.user_id;

            MyContext.SaveChanges();

            return Mapper.Map<TE, TD>(entity);
        }

        public virtual TD Update(int id, TD dto)
        {
            var entity = ToEntity(dto);

            entity.UpdateDate = DateTime.Now;

            MyContext.SaveChanges();

            return Mapper.Map<TE, TD>(entity);
        }

        public virtual void Delete(TID id)
        {
            var entity = (TE)MyContext.Set<TE>().Find(id);

            entity.DeleteDate = DateTime.Now;
            entity.DeleteBy = CurrentUser?.user_id;
            entity.Deleted = true;

            MyContext.SaveChanges();
        }


        public virtual object GetDataList()
        {
            return null;
        }

        public virtual object GetDataEdit()
        {
            return null;
        }

        protected bool HasValue(object id)
        {
            var typeInfo = id.GetType();
            return typeInfo.Equals(typeof(String)) && string.IsNullOrEmpty(id.ToString()) ||
                   typeInfo.Equals(typeof(int)) && Convert.ToInt32(id) > 0;
        }

        protected bool IsSimple(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return IsSimple(typeInfo.GetGenericArguments()[0]);
            }
            return typeInfo.IsPrimitive
              || typeInfo.IsEnum
              || type.Equals(typeof(String))
              || type.Equals(typeof(String))
              || type.Equals(typeof(DateTime));
        }


        public UsuarioAuth GetCurrentUser(string accessToken, AppSettings appSettings)
        {
            var authAdmin = new AuthAdmin(appSettings, MyContext);
            var usuarioAuth = authAdmin.GetCurrentUser(accessToken);

            return usuarioAuth;
        }

        #region Abstract Methods

        public abstract TE ToEntity(TD dto);
        public abstract Dtos.Response.ResultMessage<TD> Validate(TD dto, int Type);
        public abstract IQueryable GetQuery(TF filter);
        #endregion
    }

    public static partial class CustomExtensions
    {
        public static IQueryable<T> Include<T>(this IQueryable<T> source, IEnumerable<string> navigationPropertyPaths)
            where T : class
        {
            return navigationPropertyPaths.Aggregate(source, (query, path) => query.Include(path));
        }

        public static IEnumerable<string> GetIncludePaths(this DbContext context, Type clrEntityType)
        {
            var entityType = context.Model.FindEntityType(clrEntityType);
            var includedNavigations = new HashSet<INavigation>();
            var stack = new Stack<IEnumerator<INavigation>>();

            while (true)
            {
                var entityNavigations = new List<INavigation>();

                foreach (var navigation in entityType.GetNavigations())
                {
                    if (includedNavigations.Add(navigation))
                        entityNavigations.Add(navigation);
                }

                if (entityNavigations.Count == 0)
                {
                    if (stack.Count > 0)
                        yield return string.Join(".", stack.Reverse().Select(e => e.Current.Name));
                }
                else
                {
                    foreach (var navigation in entityNavigations)
                    {
                        var inverseNavigation = navigation.FindInverse();
                        if (inverseNavigation != null)
                            includedNavigations.Add(inverseNavigation);
                    }

                    stack.Push(entityNavigations.GetEnumerator());
                }

                while (stack.Count > 0 && !stack.Peek().MoveNext())
                    stack.Pop();

                if (stack.Count == 0) break;

                entityType = stack.Peek().Current.GetTargetType();
            }
        }

    }
}
