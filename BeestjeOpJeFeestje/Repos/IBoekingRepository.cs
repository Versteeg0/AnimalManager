using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Repos
{
    public interface IBoekingRepository
    {
        List<Boeking> GetAllBoeking();

        Boeking GetBoekingById(int? id);

        void AddBoeking(BoekingVM boekingVM);

        void RemoveBoeking(Boeking boeking);

        void Dispose();
    }
}