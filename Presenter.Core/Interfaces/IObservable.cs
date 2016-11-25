using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.Core.Interfaces
{
    public interface IObservable
    {
        void AddObserver(IObserver observer);
    }
}
