using System.Collections.Generic;
using System.Linq;
using FbCollector.Domain;
using FbCollector.Domain.Mapper;
using FbCollector.Intefraces;
using FbCollector.Models;
using Microsoft.Practices.ServiceLocation;
using NHibernateCfg;

namespace FbCollector.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;

        public RoleService()
        {
            _roleRepository = ServiceLocator.Current.GetInstance<IRepository<Role>>();
        }

        public IEnumerable<RoleModel> GetRoles()
        {
            var roles =
                _roleRepository.GetAll()
                    .Select(r => r.ToModel());

            return roles;
        }
    }
}
