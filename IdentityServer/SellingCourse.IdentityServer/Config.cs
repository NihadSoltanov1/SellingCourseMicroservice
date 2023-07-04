﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace SellingCourse.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes=new[] {"catalog_fullpermission"}},
            new ApiResource("resource_photo_stock"){Scopes=new[]{"photo_stock_fullpermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Email(),
                    new IdentityResources.Profile(),
                    new IdentityResource(){Name="roles",DisplayName="Roles",Description="User Roles", UserClaims=new []{"role"}}

            };



        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
               new ApiScope("catalog_fullpermission","Catalog Api'sine tam giris"),
               new ApiScope("photo_stock_fullpermission","Photo Stock Api'sine tam giris"),
               new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                    {
                        ClientName = "Asp.net Core MVC",
                        ClientId = "WebMvcClient",
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        AllowedScopes = { "catalog_fullpermission", "photo_stock_fullpermission",
                        IdentityServerConstants.LocalApi.ScopeName }
                    },
                 new Client
                    {
                        ClientName = "Asp.net Core MVC",
                        ClientId = "WebMvcClientForUser",
                        AllowOfflineAccess=true,
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                        AllowedScopes = { IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName, "roles" },
                        AccessTokenLifetime=1*60*60,
                        RefreshTokenExpiration=TokenExpiration.Absolute,
                        AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                        RefreshTokenUsage=TokenUsage.ReUse
                    }
            };
    }
}


