using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Repos
{
    public interface IAccessoiresRepository
    {
        List<Accessoires> GetAccessoires();
        Accessoires GetAccessoireById(int? id);
        void CreateAccessoire(AccessoireVM model);
        void EditAccessoire(AccessoireVM accessoireVM);

        void DeleteAccessoire(Accessoires accessoire);
        void Dispose();
    }
}