﻿namespace Neat.Infrastructure.Security.Model.Response
{
    public class AuthorizationResponse
    {
        public string UserId { get; set; }
        public bool IsAuthorized { get; set; }
        public string AuthorizationMessage { get; set; }
        public object SecuredObject { get; set; }

        public override string ToString()
        {
            return string.Format("UserId: {0}, IsAuthorized: {1}, AuthorizationMessage: {2}, SecuredObject: {3}", UserId, IsAuthorized, AuthorizationMessage, SecuredObject);
        }
    }
}