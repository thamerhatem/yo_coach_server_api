﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using YoCoachServer.Models.BindingModels;
using YoCoachServer.Models.Enums;

namespace YoCoachServer.Models.Repositories
{
    public class StudentRepository
    {
        public static List<Coach> ListCoaches(string cliendId)
        {
            try
            {
                using (var context = new YoCoachServerContext())
                {
                    var studentCoaches = context.StudentCoach.Where(x => x.StudentId.Equals(cliendId)).Include("Coach").ToList();
                    var coaches = new List<Coach>();
                    if (studentCoaches != null)
                    {
                        foreach(var studentCoach in studentCoaches)
                        {
                            var coach = context.Coach.Where(x => x.Id.Equals(studentCoach.CoachId)).Include("User").Include("Gyms").FirstOrDefault();
                            if(coach != null)
                            {
                                coaches.Add(coach);
                            }
                        }
                    }
                    return coaches;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static List<Schedule> ListSchedules(string coachId)
        {
            try
            {
                using (var context = new YoCoachServerContext())
                {
                    var schedules = context.Schedule.Where(x => x.Coach.Id.Equals(coachId)).Include("Gym").ToList();
                    return schedules;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static ValuesBindingModel FetchClientValues(string coachId, GetValuesBindingModel model)
        {
            try
            {
                using (var context = new YoCoachServerContext())
                {
                    var schedules = context.Schedule.Where(x => x.Students.FirstOrDefault().Id.Equals(model.StudentId) && x.Coach.Id.Equals(coachId)).ToList();
                    double? notConfirmedAmount = 0;
                    double? confirmedAmount = 0;
                    double? creditsPreQuantity = 0;
                    double? creditsPostQuantity = 0;
                    if (schedules != null)
                    {
                        foreach (Schedule schedule in schedules)
                        {
                            if (schedule.IsConfirmed.HasValue && schedule.IsConfirmed.Value)
                            {
                                confirmedAmount += schedule.TotalValue;
                            }
                            else
                            {
                                notConfirmedAmount += schedule.TotalValue;
                            }
                            if(schedule.Gym != null)
                            {
                                if(schedule.Gym.Credit != null)
                                {
                                    if (schedule.Gym.Credit.CreditPolicy.Equals(CreditPolicy.PRE))
                                    {
                                        creditsPreQuantity += schedule.CreditsQuantity;
                                    }
                                    else if (schedule.Gym.Credit.CreditPolicy.Equals(CreditPolicy.POST))
                                    {
                                        creditsPostQuantity += schedule.CreditsQuantity;
                                    }
                                }
                            }
                        }
                    }
                    var studentCoach = context.StudentCoach.FirstOrDefault(x => x.StudentId.Equals(model.StudentId) && x.CoachId.Equals(coachId));
                    ValuesBindingModel valueModel = new ValuesBindingModel()
                    {
                        StudentType = studentCoach.StudentType,
                        CreditsPreQuantity = creditsPreQuantity,
                        CreditsPostQuantity = creditsPostQuantity,
                        ConfirmedAmount = confirmedAmount,
                        NotConfirmedAmount = notConfirmedAmount,
                        TotalAmount = confirmedAmount + notConfirmedAmount
                    };
                    return valueModel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}