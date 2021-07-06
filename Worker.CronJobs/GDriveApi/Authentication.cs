using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Worker.CronJobs.GDriveApi
{
    public static class Authentication
    {
    
        public static DriveService Credential()
        {
            UserCredential credential;

            string[] Scopes = { DriveService.Scope.Drive };
            string ApplicationName = "Drive API";

            string cred_path = "credentials.json";
            string token_path = "token.json";



            var res = System.IO.File.Exists("credentials.json");

            using (var stream =
                new FileStream(cred_path, FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(token_path, true)).Result;
                Console.WriteLine("Credential file saved to: " + token_path);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

        public static IServiceCollection AddGoogleAuth(this IServiceCollection services)
        {
            // This configures Google.Apis.Auth.AspNetCore3 for use in this app.
            services
                .AddAuthentication(o =>
                {
                    // This forces challenge results to be handled by Google OpenID Handler, so there's no
                    // need to add an AccountController that emits challenges for Login.
                    o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
                    // This forces forbid results to be handled by Google OpenID Handler, which checks if
                    // extra scopes are required and does automatic incremental auth.
                    o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
                    // Default scheme that will handle everything else.
                    // Once a user is authenticated, the OAuth2 token info is stored in cookies.
                    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                 })
                .AddCookie()
                .AddGoogleOpenIdConnect(options =>
                {
                    options.ClientId = "549926791743-r7amfgehqh5buac9go7hrrqohltl6vtc.apps.googleusercontent.com";
                    options.ClientSecret = "N9ucl69djmDDQrjlEUv0ljwV";
                });
            return services;
        }
    }

     
}
