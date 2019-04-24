using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Helpers.Interfaces;
using Web.Models;

namespace Web.Helpers.Classes
{
    public class UserHelper : IUserHelper
    {
        private readonly ApplicationDbContext dbContext;

        public UserHelper(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public UserViewModel BuildUserViewModel(UserViewModel model)
        {
            model = model ?? new UserViewModel();

            model.Roles = dbContext.Roles.Select(r => r.Name).ToList();

            return model;
        }

        public UserViewModel BuildUserViewModel(string id)
        {
            ApplicationUser dataModel = dbContext.Users.Find(id);
            if (dataModel == null)
            {
                return null;
            }
            var viewModel = new UserViewModel
            {
                Email = dataModel.Email,
            };

            viewModel.Roles = dbContext.Roles.Select(r => r.Name).ToList();
            viewModel.SelectedRoles.AddRange(dbContext.Roles
                .Where(r => r.Users.Any(ur => ur.UserId == id)).Select(r => r.Name));

            return viewModel;
        }

        public void EditUser(UserViewModel model)
        {
            ApplicationUser userDataModel = dbContext.Users.Find(model.Id);
            if (userDataModel == null)
            {
                throw new KeyNotFoundException();
            }

            userDataModel.Email = model.Email;
            
            foreach (var role in model.SelectedRoles)
            {
                IdentityRole roleDataModel = dbContext.Roles
                    .FirstOrDefault(r => r.Name == role.ToLower());

                if (roleDataModel == null)
                {
                    roleDataModel = new IdentityRole
                    {
                        Name = role
                    };

                    roleDataModel = dbContext.Roles.Add(roleDataModel);
                }

                if (!roleDataModel.Users.Any(u => u.UserId == model.Id))
                {
                    var userRoleDataModel = new IdentityUserRole
                    {
                        RoleId = roleDataModel.Id,
                        UserId = userDataModel.Id
                    };

                    roleDataModel.Users.Add(userRoleDataModel);
                    userDataModel.Roles.Add(userRoleDataModel);
                }
            }

            dbContext.SaveChanges();
        }
    }
}