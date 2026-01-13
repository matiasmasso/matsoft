using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class IbanStructureService
    {

        public static IbanStructureModel? GetValue(string countryISO)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.IbanStructures
                    .AsNoTracking()
                    .Where(x => x.CountryIso == countryISO)
                    .Select(x => new IbanStructureModel(x.CountryIso)
                    {
                        Country = x.Country,

                        BankPosition = x.BankPosition,
                        BankLength = x.BankLength,
                        BankFormat = (IbanStructureModel.Formats)x.BankFormat,

                        BranchPosition = x.BranchPosition,
                        BranchLength = x.BranchLength,
                        BranchFormat = (IbanStructureModel.Formats)x.BranchFormat,

                        CheckDigitsPosition = x.CheckDigitsPosition,
                        CheckDigitsLength = x.CheckDigitsLength,
                        CheckDigitsFormat = (IbanStructureModel.Formats)x.CheckDigitsFormat,

                        AccountPosition = x.AccountPosition,
                        AccountLength = x.AccountLength,
                        AccountFormat = (IbanStructureModel.Formats)x.AccountFormat
                    }).FirstOrDefault();
            }
        }

        public static bool Update(IbanStructureModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.IbanStructure? entity;
                if (value.IsNew)
                {
                    entity = new Entities.IbanStructure();
                    db.IbanStructures.Add(entity);
                    entity.CountryIso = value.CountryISO;
                }
                else
                    entity = db.IbanStructures.Find(value.CountryISO);

                if (entity == null) throw new Exception("IbanStructure not found");

                entity.Country = value.Country;

                entity.BankPosition = (short)value.BankPosition;
                entity.BankLength = (short)value.BankLength;
                entity.BankFormat = (byte)value.BankFormat;

                entity.BranchPosition = (short)value.BranchPosition;
                entity.BranchLength = (short)value.BranchLength;
                entity.BranchFormat = (byte)value.BranchFormat;

                entity.CheckDigitsPosition = (short)value.CheckDigitsPosition;
                entity.CheckDigitsLength = (short)value.CheckDigitsLength;
                entity.CheckDigitsFormat = (byte)value.CheckDigitsFormat;

                entity.AccountPosition = (short)value.AccountPosition;
                entity.AccountLength = (short)value.AccountLength;
                entity.AccountFormat = (byte)value.AccountFormat;

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(string countryISO)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.IbanStructures.Remove(db.IbanStructures.Single(x => x.CountryIso == countryISO));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class IbanStructuresService
    {
        public static List<IbanStructureModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.IbanStructures
                    .AsNoTracking()
                    .Select(x => new IbanStructureModel(x.CountryIso)
                    {
                        Country = x.Country,

                        BankPosition = x.BankPosition,
                        BankLength = x.BankLength,
                        BankFormat = (IbanStructureModel.Formats)x.BankFormat,

                        BranchPosition = x.BranchPosition,
                        BranchLength = x.BranchLength,
                        BranchFormat = (IbanStructureModel.Formats)x.BranchFormat,

                        CheckDigitsPosition = x.CheckDigitsPosition,
                        CheckDigitsLength = x.CheckDigitsLength,
                        CheckDigitsFormat = (IbanStructureModel.Formats)x.CheckDigitsFormat,

                        AccountPosition = x.AccountPosition,
                        AccountLength = x.AccountLength,
                        AccountFormat = (IbanStructureModel.Formats)x.AccountFormat
                    }).ToList();
            }
        }
    }
}
