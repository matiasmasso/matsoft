using Api.Entities;
using DocumentFormat.OpenXml.Bibliography;
using DTO;
using Api.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
namespace Api.Services
{
    public class VehicleService
    {

        public static DTO.VehicleModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval =     Find(db, guid);
                if(retval != null)
                retval.Docfiles = DownloadTargetsService.FromTarget(guid).ToList();
                return retval;
            }
        }


        public static DTO.VehicleModel? Find(Entities.MaxiContext db, Guid guid)
        {
            return db.VwVehicles
                        .AsNoTracking()
                            .Where(x => x.Guid == guid)
                            .Select(x => new DTO.VehicleModel(guid)
                            {
                                Model = x.Model == null ? null : new GuidNom((Guid)x.ModelGuid!, string.Format("{0} {1}", x.Marca ?? "", x.Model ?? "")),
                                Conductor = x.ConductorGuid == null ? null : new GuidNom((Guid)x.ConductorGuid, x.ConductorNom ?? ""),
                                Venedor = x.VenedorGuid == null ? null : new GuidNom((Guid)x.VenedorGuid, x.VenedorNom ?? ""),
                                Contract = x.Contracte == null ? null : new GuidNom((Guid)x.Contracte, x.ContracteNom ?? ""),
                                Insurance = x.Insurance == null ? null : new GuidNom((Guid)x.Insurance, x.InsuranceNom ?? ""),
                                Matricula = x.Matricula ?? "",
                                Bastidor = x.Bastidor ?? "",
                                Alta = x.Alta,
                                Baixa = x.Baixa,
                                Privat = x.Privat,
                                Emp = (EmpModel.EmpIds)x.Emp
                            }).FirstOrDefault();
        }

        public static async Task<bool> UpdateAsync(DTO.VehicleModel value,  IFormFile? file)
        {
            using (var db = new Entities.MaxiContext())
            {
                var guid = value.Guid;
                Entities.VehicleFlotum? entity;
                if (value.IsNew)
                {
                    entity = new Entities.VehicleFlotum();
                    db.VehicleFlota.Add(entity);
                    entity.Guid = guid;
                }
                else
                    entity = db.VehicleFlota.Find(guid);

                if (entity == null) throw new System.Exception("Vehicle not found");

                entity.ModelGuid = value.Model?.Guid;
                entity.ConductorGuid = value.Conductor?.Guid;
                entity.VenedorGuid = value.Venedor?.Guid;
                entity.Contracte = value.Contract?.Guid;
                entity.Insurance = value.Insurance?.Guid;
                entity.Matricula = value.Matricula;
                entity.Bastidor = value.Bastidor;
                entity.Alta = value.Alta ?? default(DateOnly);
                entity.Baixa = value.Baixa;
                entity.Privat = value.Privat;
                entity.Emp = (int)value.Emp;

                if (file != null)
                {
                    entity.ImageMime = (int)value.ImageMime!;
                    entity.Image = file.ToByteArray();
                    //entity.Image = await file.BytesAsync();
                }


                db.SaveChanges();
                return true;
            }
        }


        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.VehicleFlota.Find(guid);
                if (entity != null)
                {
                    db.VehicleFlota.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }


        public static Media? Image(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.VehicleFlota
                        .AsNoTracking()
                    .Where(x=>x.Guid == guid)
                    .Select(x=>new Media(Media.MimeCods.Jpg, x.Image))
                    .FirstOrDefault();
            }
        }

    }
    public class VehiclesService
    {

        public static List<DTO.VehicleModel> All()
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.VwVehicles
                        .AsNoTracking()
                                .Select(x => new DTO.VehicleModel(x.Guid)
                                {
                                    Model = x.Model == null ? null : new GuidNom((Guid)x.ModelGuid!, string.Format("{0} {1}", x.Marca ?? "", x.Model ?? "")),
                                    Conductor = x.ConductorGuid == null ? null : new GuidNom((Guid)x.ConductorGuid, x.ConductorNom ?? ""),
                                    Venedor = x.VenedorGuid == null ? null : new GuidNom((Guid)x.VenedorGuid, x.VenedorNom ?? ""),
                                    Contract = x.Contracte == null ? null : new GuidNom((Guid)x.Contracte, x.ContracteNom ?? ""),
                                    Insurance = x.Insurance == null ? null : new GuidNom((Guid)x.Insurance, x.InsuranceNom ?? ""),
                                    Matricula = x.Matricula ?? "",
                                    Bastidor = x.Bastidor ?? "",
                                    Alta = x.Alta,
                                    Baixa = x.Baixa,
                                    Privat = x.Privat,
                                    Emp = (EmpModel.EmpIds)x.Emp
                                })
                                .OrderByDescending(x => x.Alta)
                                .ToList();

                return retval;
            }
        }

        public static List<GuidNom> CarModels()
        {
            using (var db = new Entities.MaxiContext()) { 
                return db.VehicleModels
                        .AsNoTracking()
                .Where(x => x.MarcaGuid != null)
                .Select(x => new GuidNom
                {
                    Guid = x.Guid,
                    Nom = string.Format("{0} {1}", x.Marca == null ? "" : x.Marca.Nom ?? "", x.Nom ?? "")
                })
                .ToList();
            }
        }
    }
}
