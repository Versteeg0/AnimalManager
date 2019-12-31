using BeestjeOpJeFeestje.Models;
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

        void AddBoeking(Boeking boeking);

        void RemoveBoeking(Boeking boeking);

        void Dispose();
    }
}