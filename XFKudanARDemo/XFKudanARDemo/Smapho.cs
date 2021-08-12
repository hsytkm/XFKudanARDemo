using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XFKudanARDemo
{
    public static class Smapho
    {
        public static async Task<bool> CheckAndRequestPermissionsAsync(IEnumerable<Permissions.BasePermission> permissions)
        {
            foreach (var permission in permissions)
            {
                var status = await CheckAndRequestPermissionAsync(permission);
                if (status != PermissionStatus.Granted)
                    return false;   // Notify user permission was denied
            }
            return true;
        }

        private static async Task<PermissionStatus> CheckAndRequestPermissionAsync<T>(T permission)
            where T : Permissions.BasePermission
        {
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }
            return status;
        }
    }
}
