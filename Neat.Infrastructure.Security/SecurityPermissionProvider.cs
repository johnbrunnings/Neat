﻿using System.Collections.Generic;
using System.Linq;

namespace Neat.Infrastructure.Security
{
    public class SecurityPermissionProvider : ISecurityPermissionProvider
    {
        public IEnumerable<string> GetActionsForRole(string role)
        {
            var actions = new List<string>();
            actions.Add("Read");
            actions.Add("Create");
            actions.Add("Update");
            actions.Add("Delete");

            return actions;
        }

        public bool CanPerformActionOnProperty(string role, string action, string propertyName)
        {
            var actions = GetActionsForRole(role);

            if (action == "Update") return true;

            return actions.Contains(action);
        }
    }
}