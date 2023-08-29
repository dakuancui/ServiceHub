using System;
using ServiceHub.API.Application.Models;

namespace ServiceHub.API.Application.Environment
{
    public class ProfileEnvironment : IProfileEnvironment
    {
        public IEnumerable<Profile> Profiles { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

