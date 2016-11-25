using Presenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.Core.Interfaces
{
    public interface IDataManager
    {
        List<Product> GetProductsByFamily(int familyId);
    }
}
