using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CarDealer.Models;
using CarDealer.Data;

namespace CarDealerApp.Service
{
    public class AuthenticationManager
    {
        private static CarDealerContext context = new CarDealerContext();
        public static bool IsAuthenticated(string sessionId)
        {
            if (context.Sessions.Any(login => login.SessionId == sessionId && login.IsActive))
            {
                return true;
            }

            return false;
        }

        public static User GetAuthenticatedUser(string sessionId)
        {
            var firstOrDefault = context.Sessions.FirstOrDefault(login => login.SessionId == sessionId && login.IsActive);
            if (firstOrDefault != null)
            {
                var authenticatedUser = firstOrDefault.User;
                if (authenticatedUser != null)
                    return authenticatedUser;
            }

            return null;
        }

        public static void Logout(string sessioId)
        {
            Session login = context.Sessions.FirstOrDefault(login1 => login1.SessionId == sessioId);
            login.IsActive = false;
            context.SaveChanges();
        }
    }
}