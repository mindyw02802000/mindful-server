




using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Do;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BlModelService : IBlModells
    {
        IDal dal;
        IBlOrders orders;
        IBldetailingModels detailingModels;
        public BlModelService(IDal dal, IBlOrders orders,IBldetailingModels detailingModels)
        {
            this.dal = dal;
            this.orders = orders;
            this.detailingModels = detailingModels;
        }


        public void delete(int id)
        {
            BlModell o = GetModellById(id);
            List<BlDetailingModel> toDelete = detailingModels.GetDetailingModelId(id);
            foreach (BlDetailingModel b in toDelete)
            {
                detailingModels.delete(b.Id);
            }
            Modell o2 = new Modell()
            {
                IdModel = o.IdModel,
                Price = o.Price,
                Picture = o.Picture,
                Kategory = o.Kategory
            };
            dal.Modells.delete(o2);
        }


        public BlModell GetModellById(int Id)
        {
            var list = GetModels();
            return list.Find(x => x.IdModel == Id);
        }

        public List<BlModell> GetModels()
        {
            var oList = dal.Modells.GetModels();
            List<BlModell> list = new();
            oList.ForEach(o =>
            {
                var oo = new BlModell()
                {
                    IdModel = o.IdModel,
                    Price = o.Price,
                    Picture = o.Picture,
                    Kategory = o.Kategory,
                };
                o.DetailingModels.ToList().ForEach(x =>
                {
                    var y = new BlDetailingModel()
                    {
                        IdModel = x.IdModel,
                        Count = x.Count,
                        Size = x.Size,
                        Id = x.Id,
                    };
                    //  var y=dal.DetailsModels.GetDetailingModel
                    oo.DetailingModels.Add(y);
                });
                list.Add(oo);
            }
            );
            return list;
        }

        
        public List<BlModell> GetModelsByKategory(string k)
        {
            var o = GetModels();

            var x = o.FindAll(x => x.Kategory.Equals(k));
            return x;
        }


        public void add(BlModell item)
        {
            var y = new DetailingModel();

            List<DetailingModel> list = new List<DetailingModel>();
            foreach (var m in item.DetailingModels)
            {
                DetailingModel detailing = new DetailingModel();
                detailing.IdModel = m.IdModel;
                detailing.Count = m.Count;
                detailing.Size = m.Size;
                list.Add(detailing);

            }

            Modell o = new Modell()
            {
                IdModel = item.IdModel,
                Price = item.Price,
                Picture = item.Picture,
                Kategory = item.Kategory,
                DetailingModels = list
            };
            dal.Modells.add(o);
        }



        public void edit(BlModell? item)
        {

            Modell? m = new Modell()
            {
                IdModel = item.IdModel,
                Price = item.Price,
                Picture = item.Picture,
                Kategory = item.Kategory,
            };
            dal.Modells.edit(m);
        }

       
    }
}

