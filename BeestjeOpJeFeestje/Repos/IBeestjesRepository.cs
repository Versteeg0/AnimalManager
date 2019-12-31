using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Repos
{
    public interface IBeestjesRepository
    {
        List<Beestje> GetBeestjes();

       Beestje GetBeestjeById(int? id);

       void AddBeestje(BeestjeVM beest);

       void EditBeestje(BeestjeVM beest);

       void DeleteBeestje(Beestje beest);

       void Dispose();
       List<Accessoires> GetAccessoiresById(int id);
    }
}