using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AutoMapper;

namespace Calabonga.Facts.Web.Infrastructure.Mappers.Base
{
    public static class MapperRegistration
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            var profiles = GetProfiles();
            return new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles.Select(profile => (Profile)Activator.CreateInstance(profile)!))
                {
                    cfg.AddProfile(profile);
                }
            });
        }

        private static List<Type> GetProfiles()
        {
            return (from t in typeof(Startup).GetTypeInfo().Assembly.GetTypes()
                    where typeof(IAutomapper).IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract
                    select t).ToList();

        }
    }
}
