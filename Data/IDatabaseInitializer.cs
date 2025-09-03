using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Data;
public interface IDatabaseInitializer { Task InitializeAsync(); }