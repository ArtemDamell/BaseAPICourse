// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        /// <summary>
        /// Gets the list of IdentityResources.
        /// </summary>
        /// <returns>A list of IdentityResources.</returns>
        public static IEnumerable<IdentityResource> IdentityResources =>
                    new IdentityResource[]
                    {
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile()
                    };

        /// <summary>
        /// Adds ApiScopes to the Config file of the IdentityServer project.
        /// </summary>
        /// <param name="">None</param>
        /// <returns>
        /// An IEnumerable of ApiScopes containing "webapi", "read", and "write".
        /// </returns>
        public static IEnumerable<ApiScope> ApiScopes =>
                    new ApiScope[]
                    {
                        new ApiScope("webapi"),
                        new ApiScope("read"),
                        new ApiScope("write")
                    };

        /// <summary>
        /// Gets a list of Client objects.
        /// </summary>
        /// <returns>A list of Client objects.</returns>
        public static IEnumerable<Client> Clients =>
                    new Client[]
                    {
                new Client
                {
                    ClientId = "console.client", // client name (must be unique)
                    AllowedGrantTypes = GrantTypes.ClientCredentials, // Client password
                    ClientSecrets = { new Secret("secret".Sha256())},
                    AllowedScopes = {"webapi"}
                },
                new Client
                {
                    ClientId = "blazorwasm.client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RedirectUris = {"https://localhost:44359/authentication/login-callback" },
                    PostLogoutRedirectUris = {"https://localhost:44359/authentication/logout-callback" },
                    AllowedCorsOrigins = {"https://localhost:44359" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "webapi",
                        "write"
                    }
                }
                    };
    }
}