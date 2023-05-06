﻿using Data.Dapper.Models;
using Data.Dapper.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dapper.Repositories
{
    public class ActionsRepository: GenericRepository<Actions>, IActionsRepository
    {
        public ActionsRepository():base() { }
    }
}