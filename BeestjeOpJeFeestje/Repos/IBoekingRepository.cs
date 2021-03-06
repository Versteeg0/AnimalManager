﻿using BeestjeOpJeFeestje.Models;
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

        List<Beestje> GetBeestjes();

        List<Accessoires> GetAccessoires();

        void Dispose();
        Beestje GetBeestjeById(int id);
        Accessoires GetAccessoireById(int id);
    }
}