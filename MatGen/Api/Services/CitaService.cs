using Api.Entities;
using DocumentFormat.OpenXml.Bibliography;
using DTO;
using System;
using System.ComponentModel;

namespace Api.Services
{
    public class CitaService
    {
        private readonly MatGenContext _db;

        public CitaService(MatGenContext db)
        {
            _db = db;
        }

        public CitaModel? Find(Guid guid)
        {
            return _db.Cita
                .Where(x => x.Guid == guid)
                .Select(x => new CitaModel(x.Guid)
                {
                    Pub = x.Pub,
                    Author = x.Author,
                    Title = x.Title,
                    Year = x.Year,
                    Pags = x.Pags,
                    Container = x.Container,
                    Url = x.Url,
                    Content = x.Text,
                    FchCreated = x.FchCreated
                }).FirstOrDefault();
        }

        public bool Update(CitaModel value)
        {
            Citum? entity;
            if (value.IsNew)
            {
                entity = new Citum();
                _db.Cita.Add(entity);
                entity.Guid = value.Guid;
            }
            else
                entity = _db.Cita.Where(x => x.Guid == value.Guid).FirstOrDefault();

            if (entity == null) throw new Exception("Cita not found");

            entity.Pub = value.Pub;
            entity.Author = value.Author;
            entity.Title = value.Title;
            entity.Year = value.Year;
            entity.Pags = value.Pags;
            entity.Container = value.Container;
            entity.Url = value.Url;
            entity.Text = value.Content;

            // Save changes in database
            _db.SaveChanges();
            return true;
        }

        public bool Delete(Guid guid)
        {
            var entity = _db.Cita.Single(x => x.Guid.Equals(guid));
            _db.Cita.Remove(entity);
            _db.SaveChanges();
            return true;
        }
    }

    public class CitasService
    {
        private readonly MatGenContext _db;

        public CitasService(MatGenContext db)
        {
            _db = db;
        }

        public List<CitaModel> All()
        {
            return _db.Cita
                .OrderBy(x => x.Pub)
                .ThenBy(x => x.Year)
                .Select(x => new CitaModel(x.Guid)
                {
                    Pub = x.Pub,
                    Author = x.Author,
                    Title = x.Title,
                    Year = x.Year,
                    Pags = x.Pags,
                    Container = x.Container,
                    Url = x.Url,
                    Content = x.Text,
                    FchCreated = x.FchCreated
                }).ToList();
        }
    }
}
