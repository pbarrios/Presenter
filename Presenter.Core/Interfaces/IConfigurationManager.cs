﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presenter.Core.Entities;

namespace Presenter.Core.Interfaces
{
    public interface IConfigurationManager
    {
        string DbFilesPath { get; }
        List<Presentation> Presentations { get; }
    }
}
