using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniProjet.Domain.Models;
using MiniProjet.Infrastructure.Repositories;

namespace MiniProjet.Services.UsersRole
{
    public class UsersRoleService
    {
        private readonly IUsersRoleRepository _usersRoleRepository;

        public UsersRoleService(IUsersRoleRepository usersRoleRepository)
        {
            _usersRoleRepository = usersRoleRepository;
        }

        public async Task<List<UsersRole>> GetAllUsersRolesAsync()
        {
            return await _usersRoleRepository.GetAllUsersRolesAsync();
        }

        public async Task<List<UsersRole>> GetUsersRolesByUserIdAsync(int userId)
        {
            return await _usersRoleRepository.GetUsersRolesByUserIdAsync(userId);
        }

        public async Task<List<UsersRole>> GetUsersRolesByRoleIdAsync(int roleId)
        {
            return await _usersRoleRepository.GetUsersRolesByRoleIdAsync(roleId);
        }

        public async Task<UsersRole> AddUserRoleAsync(int userId, int roleId)
        {
            // Vérifier si l'utilisateur existe
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("L'utilisateur spécifié n'existe pas.");
            }

            // Vérifier si le rôle existe
            var role = await _roleRepository.GetRoleByIdAsync(roleId);
            if (role == null)
            {
                throw new Exception("Le rôle spécifié n'existe pas.");
            }

            // Vérifier si l'utilisateur n'a pas déjà ce rôle
            var existingUserRole = await _usersRoleRepository.GetUserRoleByUserIdAndRoleIdAsync(userId, roleId);
            if (existingUserRole != null)
            {
                throw new Exception("L'utilisateur a déjà ce rôle.");
            }

            return await _usersRoleRepository.AddUserRoleAsync(userId, roleId);
        }

        public async Task<bool> RemoveUserRoleAsync(int userId, int roleId)
        {

            // Vérifier si l'utilisateur existe
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("L'utilisateur spécifié n'existe pas.");
            }

            // Vérifier si le rôle existe
            var role = await _roleRepository.GetRoleByIdAsync(roleId);
            if (role == null)
            {
                throw new Exception("Le rôle spécifié n'existe pas.");
            }

            // Vérifier si l'utilisateur a ce rôle
            var userRole = await _usersRoleRepository.GetUserRoleByUserIdAndRoleIdAsync(userId, roleId);
            if (userRole == null)
            {
                throw new Exception("L'utilisateur n'a pas ce rôle.");
            }



            return await _usersRoleRepository.RemoveUserRoleAsync(userId, roleId);
        }
    }
}
