using Catel.Data;
using DataTablesParser;
using FoodMenu.Models;
using FoodMenu.Repositories;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace FoodMenu.BL
{
    public class MeetingBL
    {
        public async Task<ReturnModel<MeetingModel>> Create (MeetingModel meetingModel)
        {
            var result = new ReturnModel<MeetingModel> { Status = true };
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var MeetingRepository = session.GetRepository<IMeetingRepository>();

                var meeting = new Meeting();
                meeting.Id = meetingModel.Id;
                meeting.ClientId = meetingModel.ClientId;
                meeting.Date = meetingModel.Date;
                meeting.Weight = meetingModel.Weight;
                meeting.Water = meetingModel.Water;
                meeting.Muscle = meetingModel.Muscle;
                meeting.FatPercentage = meetingModel.FatPercentage;
                meeting.MeetingIndex = meetingModel.MeetingIndex;
                meeting.ArmMuscleMeasurement = meetingModel.ArmMuscleMeasurement;
                meeting.ArmNOMuscleMeasurement = meetingModel.ArmNOMuscleMeasurement;
                meeting.HipMeasurement = meetingModel.HipMeasurement;
                meeting.StomachMeasurement = meetingModel.StomachMeasurement;
                meeting.ThighMeasurement = meetingModel.ThighMeasurement;
                meeting.FrontHandFat = meetingModel.FrontHandFat;
                meeting.BackHandFat = meetingModel.BackHandFat;
                meeting.ThighFat = meetingModel.ThighFat;
                meeting.BackFat = meetingModel.BackFat;
                meeting.FatAvrg = meetingModel.FatAvrg;
                MeetingRepository.Add(meeting);

                await session.SaveChangesAsync();

                meetingModel.Id = meeting.Id;
                result.Result = meetingModel;
                return result;
            }
        }

        public Task<FormatedList<MeetingModel>> GetAll (NameValueCollection requestParams,int clientId)
        {
            return Task.Run(() =>
            {
                using(var session = new UnitOfWork<FoodMenuEntities>())
                {
                    IMeetingRepository MeetingRepository = session.GetRepository<IMeetingRepository>();

                    var MeetingList = MeetingRepository.Find(u => u.ClientId == clientId).Select(m => new MeetingModel
                    {
                        Id = m.Id,
                        ClientId = m.ClientId,
                        Date = m.Date,
                        Weight = m.Weight,
                        Water = m.Water,
                        Muscle = m.Muscle,
                        FatPercentage = m.FatPercentage,
                        MeetingIndex = m.MeetingIndex,
                        ArmMuscleMeasurement = m.ArmMuscleMeasurement,
                        ArmNOMuscleMeasurement = m.ArmNOMuscleMeasurement,
                        HipMeasurement = m.HipMeasurement,
                        StomachMeasurement = m.StomachMeasurement,
                        ThighMeasurement = m.ThighMeasurement,
                        FrontHandFat = m.FrontHandFat,
                        BackHandFat = m.BackHandFat,
                        ThighFat = m.ThighFat,
                        BackFat = m.BackFat,
                        FatAvrg = m.FatAvrg,
                    });

                    var parser = new DataTableEntityParser<MeetingModel>(requestParams,MeetingList.AsQueryable());

                    var list = parser.Parse();

                    return list;
                }
            });
        }

        public Task<ReturnModel<List<MeetingModel>>> GetFirstAndLast (int clientId)
        {
            ReturnModel < List <MeetingModel>> model = new ReturnModel<List<MeetingModel>>();
            return Task.Run(() =>
            {
                using(var session = new UnitOfWork<FoodMenuEntities>())
                {
                    IMeetingRepository MeetingRepository = session.GetRepository<IMeetingRepository>();

                    var allClientMeetings = MeetingRepository.Find(u => u.ClientId == clientId);
                    var MeetingList = allClientMeetings.Where(m => m.Id == allClientMeetings.Min(n => n.Id) || m.Id== allClientMeetings.Max(x=>x.Id)).Select(m => new MeetingModel
                    {
                        Id = m.Id,
                        ClientId = m.ClientId,
                        Date = m.Date,
                        Weight = m.Weight,
                        Water = m.Water,
                        Muscle = m.Muscle,
                        FatPercentage = m.FatPercentage,
                        MeetingIndex = m.MeetingIndex,
                        ArmMuscleMeasurement = m.ArmMuscleMeasurement,
                        ArmNOMuscleMeasurement = m.ArmNOMuscleMeasurement,
                        HipMeasurement = m.HipMeasurement,
                        StomachMeasurement = m.StomachMeasurement,
                        ThighMeasurement = m.ThighMeasurement,
                        FrontHandFat = m.FrontHandFat,
                        BackHandFat = m.BackHandFat,
                        ThighFat = m.ThighFat,
                        BackFat = m.BackFat,
                        FatAvrg = m.FatAvrg,
                    }).OrderBy(o=>o.Id);
                    model.Status = true;
                    model.Result = MeetingList.ToList();
                    return model;
                }
            });
        }

        public async Task<ReturnModel<MeetingModel>> GetByID (int meetingID)
        {
            MeetingModel meetingModel = new MeetingModel();
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var MeetingRepository = session.GetRepository<IMeetingRepository>();

                var meeting = await MeetingRepository.GetByID(meetingID);
                meetingModel.Id = meeting.Id;
                meetingModel.ClientId = meeting.ClientId;
                meetingModel.Date = meeting.Date;
                meetingModel.Weight = meeting.Weight;
                meetingModel.Water = meeting.Water;
                meetingModel.Muscle = meeting.Muscle;
                meetingModel.FatPercentage = meeting.FatPercentage;
                meetingModel.MeetingIndex = meeting.MeetingIndex;
                meetingModel.ArmMuscleMeasurement = meeting.ArmMuscleMeasurement;
                meetingModel.ArmNOMuscleMeasurement = meeting.ArmNOMuscleMeasurement;
                meetingModel.HipMeasurement = meeting.HipMeasurement;
                meetingModel.StomachMeasurement = meeting.StomachMeasurement;
                meetingModel.ThighMeasurement = meeting.ThighMeasurement;
                meetingModel.FrontHandFat = meeting.FrontHandFat;
                meetingModel.BackHandFat = meeting.BackHandFat;
                meetingModel.ThighFat = meeting.ThighFat;
                meetingModel.BackFat = meeting.BackFat;
                meetingModel.FatAvrg = meeting.FatAvrg;

                return new ReturnModel<MeetingModel>
                {
                    Status = true,
                    Result = meetingModel
                };
            };
        }

        public async Task<ReturnModel<bool>> Update (MeetingModel meetingModel)
        {
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var MeetingRepository = session.GetRepository<IMeetingRepository>();

                var meeting = await MeetingRepository.GetByID(meetingModel.Id);

                meeting.Id = meetingModel.Id;
                meeting.ClientId = meetingModel.ClientId;
                meeting.Date = meetingModel.Date;
                meeting.Weight = meetingModel.Weight;
                meeting.Water = meetingModel.Water;
                meeting.Muscle = meetingModel.Muscle;
                meeting.FatPercentage = meetingModel.FatPercentage;
                meeting.MeetingIndex = meetingModel.MeetingIndex;
                meeting.ArmMuscleMeasurement = meetingModel.ArmMuscleMeasurement;
                meeting.ArmNOMuscleMeasurement = meetingModel.ArmNOMuscleMeasurement;
                meeting.HipMeasurement = meetingModel.HipMeasurement;
                meeting.StomachMeasurement = meetingModel.StomachMeasurement;
                meeting.ThighMeasurement = meetingModel.ThighMeasurement;
                meeting.FrontHandFat = meetingModel.FrontHandFat;
                meeting.BackHandFat = meetingModel.BackHandFat;
                meeting.ThighFat = meetingModel.ThighFat;
                meeting.BackFat = meetingModel.BackFat;
                meeting.FatAvrg = meetingModel.FatAvrg;
                await session.SaveChangesAsync();

                return new ReturnModel<bool> { Status = true };
            }
        }


    }
}
