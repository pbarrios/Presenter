using Presenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.Core.Interfaces
{
    public interface IScreenManager
    {
        Process StartPresentation(Presentation presentation);
    }
}
