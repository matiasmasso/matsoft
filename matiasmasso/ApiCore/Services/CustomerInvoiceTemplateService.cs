using Api.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class CustomerInvoiceTemplateService
    {

        public static CustomerInvoiceModel.Template? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.CustomerInvoiceTemplates
                    //.Include(x => x.CustomerInvoiceTemplateItems)
                    .Where(x => x.Guid == guid)
                     .Select(x => new CustomerInvoiceModel.Template(x.Guid)
                     {
                         Emp = x.Emp.HasValue ? (EmpModel.EmpIds)x.Emp.Value : DTO.EmpModel.EmpIds.MMC,
                         Tag = x.Tag,
                         Customer = x.Customer,
                         CtaCredit = x.CtaCredit,
                         CtaDebit = x.CtaDebit,
                         Lang = new LangDTO(x.Lang),
                         IVApct = x.IvaPct,
                         IRPFpct = x.IrpfPct,
                         VfConcept = x.VfConcept,
                         VfTaxScheme = x.VfTaxScheme,
                         VfTaxType = x.VfTaxType,
                         VfTaxException = x.VfTaxException,

                         Items = x.CustomerInvoiceTemplateItems
                         .Where(y => y.Cod == 0)
                         .OrderBy(y => y.Lin)
                         .Select(y => new CustomerInvoiceModel.Item()
                         {
                             Cod = CustomerInvoiceModel.Item.Cods.Linia,
                             Concept = y.Concept,
                             Price = y.Price
                         }).ToList(),

                         Notas = x.CustomerInvoiceTemplateItems
                         .Where(y => y.Cod == 1)
                         .OrderBy(y => y.Lin)
                         .Select(y => new CustomerInvoiceModel.Item()
                         {
                             Cod = CustomerInvoiceModel.Item.Cods.Nota,
                             Concept = y.Concept
                         }).ToList()

                     }).FirstOrDefault();
            }
        }

        public static bool Update(CustomerInvoiceModel.Template value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Update(db, value);
                db.SaveChanges();
                return true;
            }
        }
        public static bool Update(MaxiContext db, CustomerInvoiceModel.Template value)
        {
            Entities.CustomerInvoiceTemplate? entity;
            if (value.IsNew)
            {
                entity = new Entities.CustomerInvoiceTemplate();
                db.CustomerInvoiceTemplates.Add(entity);
            }
            else
            {
                entity = db.CustomerInvoiceTemplates
                    .Include(x => x.CustomerInvoiceTemplateItems)
                    .FirstOrDefault(x => x.Guid == value.Guid);

                if (entity == null) throw new System.Exception("Template not found");
            }

            entity.Tag = value.Tag;
            entity.Emp = (int?)value.Emp;
            entity.Customer = value.Customer;
            entity.CtaDebit = value.CtaDebit;
            entity.CtaCredit = value.CtaCredit;
            entity.Lang = value.Lang?.ToString();
            entity.IvaPct = value.IVApct;
            entity.IrpfPct = value.IRPFpct;
            entity.VfConcept = value.VfConcept;
            entity.VfTaxScheme = value.VfTaxScheme;
            entity.VfTaxType = value.VfTaxType;
            entity.VfTaxException = value.VfTaxException;

            var items = value.Items
                .Select(x => new Entities.CustomerInvoiceTemplateItem()
                {
                    Cod = 0,
                    Lin = value.Items.IndexOf(x) + 1,
                    Concept = x.Concept,
                    Price = x.Price
                }).ToList();

            items.AddRange(value.Notas
                .Select((x, index) => new Entities.CustomerInvoiceTemplateItem()
                {
                    Cod = 1,
                    Lin = index + 1,
                    Concept = x.Concept,
                    Price = null
                }).ToList());

            entity.CustomerInvoiceTemplateItems = items;

            return true;
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.CustomerInvoiceTemplates.Remove(db.CustomerInvoiceTemplates.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }
    }
    public class CustomerInvoiceTemplatesService
    {
        public static List<CustomerInvoiceModel.Template> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.CustomerInvoiceTemplates
                    .Include(x => x.CustomerInvoiceTemplateItems)
                     .OrderBy(x => x.Emp)
                     .ThenBy(x => x.Tag)
                     .Select(x => new CustomerInvoiceModel.Template(x.Guid)
                     {
                         Emp = (EmpModel.EmpIds)x.Emp!,
                         Tag = x.Tag,
                         Customer = x.Customer,
                         CtaCredit = x.CtaCredit,
                         CtaDebit = x.CtaDebit,
                         Lang = new LangDTO(x.Lang),
                         IVApct = x.IvaPct,
                         IRPFpct = x.IrpfPct,
                         VfConcept = x.VfConcept,
                         VfTaxScheme = x.VfTaxScheme,
                         VfTaxType = x.VfTaxType,
                         VfTaxException = x.VfTaxException,

                         Items = x.CustomerInvoiceTemplateItems
                         .Where(y => y.Cod == 0)
                         .OrderBy(y => y.Lin)
                         .Select(y => new CustomerInvoiceModel.Item()
                         {
                             Cod = CustomerInvoiceModel.Item.Cods.Linia,
                             Concept = y.Concept,
                             Price = y.Price
                         }).ToList(),

                         Notas = x.CustomerInvoiceTemplateItems
                         .Where(y => y.Cod == 1)
                         .OrderBy(y => y.Lin)
                         .Select(y => new CustomerInvoiceModel.Item()
                         {
                             Cod = CustomerInvoiceModel.Item.Cods.Nota,
                             Concept = y.Concept
                         }).ToList()

                     }).ToList();
            }
        }
    }
}

