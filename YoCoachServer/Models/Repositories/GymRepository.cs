﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoCoachServer.Models.Enums;
using static YoCoachServer.Models.BindingModels.CoachBindingModels;

namespace YoCoachServer.Models.Repositories
{
    public class GymRepository
    {
        public static List<Gym> ListGyms(int coachId)
        {
            try
            {
                using (var context = new YoCoachServerContext())
                {
                    var coach = context.Coach.Find(coachId);
                    if(coach != null)
                    {
                        return coach.Gyms.ToList();
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void addGym(int coachId, NewGymBindingModel model)
        {
            try
            {
                using (var context = new YoCoachServerContext())
                {
                    var coach = context.Coach.Find(coachId);
                    if (coach != null)
                    {
                        var creditPurchaseList = new List<CreditPurchase>();
                        var creditPurchase = new CreditPurchase()
                        {
                            Id = Guid.NewGuid().ToString(),
                            UnitValue = model.CreditPurchase.UnitValue,
                            PurchaseDate = model.CreditPurchase.PurchaseDate,
                            TotalQuantity = model.CreditPurchase.TotalQuantity,
                            ExpirationDate = model.CreditPurchase.ExpirationDate
                        };
                        creditPurchaseList.Add(creditPurchase);

                        var gym = new Gym()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = model.Name,
                            Address = model.Address,
                            CreditType = model.CreditType,
                            CreditPurchases = creditPurchaseList,
                            Coach = coach
                        };
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}