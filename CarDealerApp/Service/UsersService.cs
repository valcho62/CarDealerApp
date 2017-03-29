using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CarDealer.Models;
using CarDealer.Models.BindingModels;

namespace CarDealerApp.Service
{
    public class UsersService :Service
    {
        public void AddUser(AddUserBM user)
        {
            var userToAdd = Mapper.Map<User>(user);
            Contex.Users.Add(userToAdd);
            Contex.SaveChanges();
        }

        public void Login(LoginBM user,string sessionId)
        {
            
            if (!this.Contex.Sessions.Any(login => login.SessionId == sessionId))
            {
                this.Contex.Sessions.Add(new Session() { SessionId = sessionId });
                this.Contex.SaveChanges();
            }

            var session = Contex.Sessions.FirstOrDefault(x => x.SessionId == sessionId);
            var userToLog = Contex.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
            if (userToLog != null)
            {
                
                session.User = userToLog;
                session.IsActive = true;
               
                Contex.SaveChanges(); 
            }
        }

        public void Logout(string sessionId)
        {
            var session = Contex.Sessions.FirstOrDefault(x => x.SessionId == sessionId);
            session.IsActive = false;
            Contex.SaveChanges();
        }
    }
}