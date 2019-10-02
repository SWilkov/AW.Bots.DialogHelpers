using System;
using System.Collections.Generic;
using System.Text;

namespace AW.Bots.DialogHelpers.Interfaces
{
  public interface IConfigurationHelperService<T>
    where T: class
  {
    T Get(string name);
  }
}
