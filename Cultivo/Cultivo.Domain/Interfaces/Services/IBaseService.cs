﻿using Cultivo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultivo.Domain.Interfaces.Services
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
    }
}