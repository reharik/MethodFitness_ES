﻿using System;
using System.Web;
using MethodFitness.Core.Config;
using MethodFitness.Core.Domain;

namespace MethodFitness.Core.Services
{
    public interface ISessionContext
    {
        int GetCompanyId();
        int GetUserId();
        object RetrieveSessionObject(Guid sessionKey);
        object RetrieveSessionObject(string sessionKey);
        SessionItem RetrieveSessionItem(string sessionKey);
        void AddUpdateSessionItem(SessionItem item);
        void RemoveSessionItem(Guid sessionKey);
        void RemoveSessionItem(string sessionKey);
        User GetCurrentUser();
    }

    public class SessionContext : ISessionContext
    {
        private readonly IRepository _repository;

        public SessionContext(IRepository repository)
        {
            _repository = repository;
        }

        public User GetCurrentUser()
        {
            return _repository.Find<User>(GetUserId());
        }

        public int GetCompanyId()
        {
            var httpContext = HttpContext.Current;
            var customPrincipal = httpContext != null ? httpContext.User as CustomPrincipal : null;
            return customPrincipal != null ? customPrincipal.CompanyId : 0;
        }

        public int GetUserId()
        {
            var httpContext = HttpContext.Current;
            var customPrincipal = httpContext != null ? httpContext.User as CustomPrincipal : null;
            return customPrincipal != null ? customPrincipal.UserId : 0;
        }

        public object RetrieveSessionObject(Guid sessionKey)
        {
            SessionItem item = (SessionItem)HttpContext.Current.Session[sessionKey.ToString()];
            return item.SessionObject;
        }

        public object RetrieveSessionObject(string sessionKey)
        {
            SessionItem item = (SessionItem)HttpContext.Current.Session[sessionKey];
            return item != null ? item.SessionObject : null;
        }

        public SessionItem RetrieveSessionItem(string sessionKey)
        {
            return (SessionItem)HttpContext.Current.Session[sessionKey];
        }

        public void AddUpdateSessionItem(SessionItem item)
        {
            HttpContext.Current.Session[item.SessionKey] = item;
        }

        public void RemoveSessionItem(Guid sessionKey)
        {
            HttpContext.Current.Session.Remove(sessionKey.ToString());
        }

        public void RemoveSessionItem(string sessionKey)
        {
            HttpContext.Current.Session.Remove(sessionKey);
        }

    }


    public class DataLoaderSessionContext : ISessionContext
    {
        public int GetCompanyId()
        {
            return 1;
        }

        public int GetUserId()
        {
            return 1;
        }

        public object RetrieveSessionObject(Guid sessionKey)
        {
            throw new NotImplementedException();
        }

        public object RetrieveSessionObject(string sessionKey)
        {
            throw new NotImplementedException();
        }

        public SessionItem RetrieveSessionItem(string sessionKey)
        {
            throw new NotImplementedException();
        }

        public void AddUpdateSessionItem(SessionItem item)
        {
            throw new NotImplementedException();
        }

        public void RemoveSessionItem(Guid sessionKey)
        {
            throw new NotImplementedException();
        }

        public void RemoveSessionItem(string sessionKey)
        {
            throw new NotImplementedException();
        }

        public User GetCurrentUser()
        {
            throw new NotImplementedException();
        }

    }



    public class SessionItem
    {
        public DateTime TimeStamp { get; set; }
        public string SessionKey { get; set; }
        public object SessionObject { get; set; }
    }
}