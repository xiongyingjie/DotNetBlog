﻿using DotNetBlog.Core.Data;
using DotNetBlog.Core.Entity;
using DotNetBlog.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetBlog.Core.Extensions;

namespace DotNetBlog.Core.Service
{
    public class UserService
    {
        private BlogContext BlogContext { get; set; }

        public UserService(BlogContext blogContext)
        {
            BlogContext = blogContext;
        }

        public async Task<OperationResult> ChangePassword(int id, string oldPassword, string newPassword)
        {
            oldPassword = Utilities.EncryptHelper.MD5(oldPassword);
            newPassword = Utilities.EncryptHelper.MD5(newPassword);

            User entity = await BlogContext.Users.SingleOrDefaultAsync(t => t.ID == id);

            if (entity == null)
            {
                return OperationResult.Failure("用户不存在");
            }
            if (entity.Password != oldPassword)
            {
                return OperationResult.Failure("密码错误");
            }

            entity.Password = newPassword;
            await BlogContext.SaveChangesAsync();

            BlogContext.RemoveUserCache();

            return new Model.OperationResult();
        }
    }
}
