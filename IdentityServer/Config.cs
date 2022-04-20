// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                // 171. Добавить в файл Config проекта IdentityServer ApiScope
                new ApiScope("webapi"),
                new ApiScope("read"),
                new ApiScope("write")
            };

        public static IEnumerable<Client> Clients =>
            new Client[] 
            {
                // 179. Первым делом после создания консоли, перейти в настройки IdentityServer и добавить нового клиента
                new Client
                {
                    ClientId = "console.client", // client name (must be unique)
                    AllowedGrantTypes = GrantTypes.ClientCredentials, // Client password
                    ClientSecrets = { new Secret("secret".Sha256())},
                    AllowedScopes = {"webapi"}
                },
                // <-- 180. На этом этапе идём в консоль

                // 195. После всех манипуляций, создаём в файле Config IdentityServer'а нового клиента
                new Client
                {
                    ClientId = "blazorwasm.client", 
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false, // Секретный код не нужен, т.к. нам негде его безопасно хранить
                    RedirectUris = {"https://localhost:44359/authentication/login-callback" },
                    PostLogoutRedirectUris = {"https://localhost:44359/authentication/logout-callback" },
                    AllowedCorsOrigins = {"https://localhost:44359" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId, // Стандартные свойства подтягиваются из ресурсов
                        IdentityServerConstants.StandardScopes.Profile,
                        "webapi",
                        "write"
                    }
                }
            };
    }
}