﻿using PlatformService.Models;
using System.Collections.Generic;

namespace PlatformService.Data.Interfaces
{
    public interface IPlatformRepository
    {
        bool SaveChanges();

        IEnumerable<Platform> GetAll();
        Platform GetById(int id);
        void Create(Platform platform);
    }
}
