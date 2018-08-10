using PetitsPainsAuChocolatine_PasDeBagarre.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitsPainsAuChocolatine_PasDeBagarre
{
    public class UserCollection : ObservableCollection<User>
    {
        public UserCollection(IEnumerable<User> users) 
            : base(users)
        {
        }
    }
}
