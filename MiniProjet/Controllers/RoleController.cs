using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace MiniProjet.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return View(roles);
        }

        public async Task<IActionResult> Details(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new Role
                {
                    Name = roleViewModel.Name,
                    Description = roleViewModel.Description,
                };

                await _roleService.CreateRoleAsync(role);
                return RedirectToAction(nameof(Index));
            }
            return View(roleViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var roleViewModel = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
            };

            return View(roleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var role = new Role
                {
                    Id = roleViewModel.Id,
                    Name = roleViewModel.Name,
                    Description = roleViewModel.Description,
                };

                await _roleService.UpdateRoleAsync(role);
                return RedirectToAction(nameof(Index));
            }
            return View(roleViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roleService.DeleteRoleAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}