using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniProjet.Domain.Models;
using MiniProjet.Infrastructure.Repositories;

namespace MiniProjet.Services.Role
{
    public class RoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleService>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllRolesAsync();
        }

        public async Task<RoleService> GetRoleByIdAsync(int roleId)
        {
            return await _roleRepository.GetRoleByIdAsync(roleId);
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            var existingRole = await _roleRepository.GetRoleByNameAsync(role.Name);
            if (existingRole != null)
            {
                throw new Exception("Un rôle avec ce nom existe déjà.");
            }


            return await _roleRepository.CreateRoleAsync(role);
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            var existingRole = await _roleRepository.GetRoleByIdAsync(role.Id);
            if (existingRole == null)
            {
                throw new Exception("Le rôle spécifié n'existe pas.");
            }


            return await _roleRepository.UpdateRoleAsync(role);
        }

        public async Task<bool> DeleteRoleAsync(int roleId)
        {
            var existingRole = await _roleRepository.GetRoleByIdAsync(roleId);
            if (existingRole == null)
            {
                throw new Exception("Le rôle spécifié n'existe pas.");
            }


            return await _roleRepository.DeleteRoleAsync(roleId);
        }
    }
}
