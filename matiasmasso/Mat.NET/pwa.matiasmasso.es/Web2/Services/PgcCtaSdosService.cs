using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Web.Services
{
    public class PgcCtaSdosService : IDisposable
    {
        public List<PgcCtaSdoModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;
        public EmpModel? Emp { get; set; }
        public int Year { get; set; }

        public event Action<Exception?>? OnChange;

        private AppStateService appstate;
        private CultureService culture;
        public PgcCtasService pgcCtasService;
        public PgcClassesService pgcClassesService;
        public ContactsService contactsService;

        private bool isLoadingBalance;


        public PgcCtaSdosService(AppStateService appstate, CultureService culture, PgcCtasService pgcCtasService, PgcClassesService pgcClassesService, ContactsService contactsService)
        {
            this.culture = culture;
            this.pgcCtasService = pgcCtasService;
            this.pgcClassesService = pgcClassesService;
            this.contactsService = contactsService;
            this.appstate = appstate;

            contactsService.OnChange += NotifyChange;
            pgcCtasService.OnChange += NotifyChange;
            pgcClassesService.OnChange += NotifyChange;
        }

        public async Task FetchAsync(EmpModel? emp, int year)
        {
            if (State != DbState.IsLoading && emp != null && (emp != Emp || year != Year))
            {
                State = DbState.IsLoading;
                Emp = emp;
                Year = year;
                try
                {
                    var empId = (int)emp.Id;
                    Values = await appstate.GetAsync<List<PgcCtaSdoModel>>("PgcCtaSdos", empId.ToString(), year.ToString()) ?? new();
                    State = DbState.StandBy;
                    OnChange?.Invoke(null);
                }
                catch (Exception ex)
                {
                    State = DbState.Failed;
                    OnChange?.Invoke(ex);
                }
            }
        }

        public List<Item>? Ctas()
        {
            return Values?
            .GroupBy(x => pgcCtasService.GetValue(x.Cta))
            .Select(x => new PgcCtaSdosService.Item
            {
                Tag = x.Key,
                Label = string.Format("{0} {1}", x.Key?.Id, x.Key?.Nom?.Tradueix(culture.Lang())),
                Deb = x.Sum(y => y.Deb),
                Hab = x.Sum(y => y.Hab),
                SaldoDeutor = x.Key?.IsDeutora() ?? false ? x.Sum(y => y.Deb - y.Hab) : 0,
                SaldoCreditor = x.Key?.IsCreditora() ?? false ? x.Sum(y => y.Hab - y.Deb) : 0,
            })
            .OrderBy(x => x.Label)
            .ToList();
        }

        public List<Item> Contacts(PgcCtaModel cta)
        {
            return Values?
            .Where(x => x.Cta == cta.Guid)
            .Select(x => new Item
            {
                Tag = GetContact(x.Contact),
                Label = GetContact(x.Contact)?.RaoSocial,
                Deb = x.Deb,
                Hab = x.Hab,
                Saldo = cta.IsDeutora() ? x.Deb - x.Hab : x.Hab - x.Deb,
            })
            .OrderBy(x => x.Label)
            .ToList() ?? new();
        }

        public PgcCtaModel? GetCta(Guid guid) => pgcCtasService.GetValue(guid);
        public LangTextModel? CtaIdNom(Guid guid) => CtaIdNom(pgcCtasService.GetValue(guid));
        public LangTextModel? CtaIdNom(PgcCtaModel? cta)
        {
            LangTextModel? retval = null;
            if (cta != null)
            {
                retval = new LangTextModel
                {
                    Esp = $"{cta.Id} {cta.Nom?.Tradueix(LangDTO.Esp())}",
                    Cat = $"{cta.Id} {cta.Nom?.Tradueix(LangDTO.Cat())}",
                    Eng = $"{cta.Id} {cta.Nom?.Tradueix(LangDTO.Eng())}",
                    Por = $"{cta.Id} {cta.Nom?.Tradueix(LangDTO.Por())}"
                };
            }
            return retval;
        }

        public List<PgcClassModel>? Epigrafs() => pgcClassesService.Values;
        public LangTextModel? EpigrafNom(PgcClassModel epg) => pgcClassesService.Nom(epg);

        public decimal Saldo(PgcCtaSdoModel value)
        {
            var cta = pgcCtasService.GetValue(value.Cta);
            var retval = cta == null || cta.Act == PgcCtaModel.Acts.Deutora ? value.Deb - value.Hab : value.Hab - value.Deb;
            return retval;
        }

        public List<PgcCtaModel>? CtasFromEpigraf(Guid? epigraf) => epigraf == null ? null : pgcCtasService.Values?.Where(x => x.Epigraf == epigraf).OrderBy(x => x.Id).ToList();
        public ContactModel? GetContact(Guid? guid) => contactsService.GetValue(guid);

        public List<PgcCtaSdoModel>? FromEpigraf(PgcClassModel epigraf)
        {
            return Values?.Where(x => GetCta(x.Cta)?.Epigraf == epigraf.Guid).ToList() ?? new();
        }

        #region Balance

        public async Task<BalanceModel?> Balance(EmpModel emp, DateTime fch, BalanceModel.Modes mode)
        {
            BalanceModel? retval = null;
            if (emp != null)
            {
                if (!isLoadingBalance && (emp != Emp || fch.Year != Year || Values == null))
                {
                    isLoadingBalance = true;
                    Emp = emp;
                    Year = fch.Year;
                    Values = await appstate.GetAsync<List<PgcCtaSdoModel>>("PgcCtaSdos", ((int)emp.Id).ToString(), fch.Year.ToString()) ?? new();
                }
                retval = new BalanceModel(emp, fch, mode);
                var seed = Seed(retval)!;
                AddCls(retval, seed);
                await AddSumandos(retval);
                isLoadingBalance = false;

            }
            return retval;
        }

        private async Task AddSumandos(BalanceModel retval)
        {
            switch (retval.Mode)
            {
                case BalanceModel.Modes.Liabilities:
                    var pl = await Balance(retval.Emp, retval.Fch, BalanceModel.Modes.PL)!;
                    var plResult = pl.GetItem(PgcClassModel.Cods.bA5_Resultado_del_ejercicio)!;
                    var result = retval.GetItem(PgcClassModel.Cods.aBA17_Resultado_del_ejercicio)!;
                    Propagate(result, plResult.Deb, plResult.Hab);
                    break;
                case BalanceModel.Modes.PL:
                    //move A1 before finantial results
                    var bA1 = retval.GetItem(PgcClassModel.Cods.bA1_Resultado_de_explotacion)!;
                    var bA2 = retval.GetItem(PgcClassModel.Cods.bA2_Resultado_Financiero)!;
                    retval.Items.Remove(bA1);
                    var bA2Idx = retval.Items.IndexOf(bA2);
                    retval.Items.Insert(bA2Idx, bA1);

                    //move A2 before A3
                    var bA3 = retval.GetItem(PgcClassModel.Cods.bA3_Resultado_antes_de_impuestos)!;
                    retval.Items.Remove(bA2);
                    var bA3Idx = retval.Items.IndexOf(bA3);
                    retval.Items.Insert(bA3Idx, bA2);

                    //calc A3 = A1+A2
                    bA3.Deb = bA1.Deb + bA2.Deb;
                    bA3.Hab = bA1.Hab + bA2.Hab;


                    //A4 == A3+20
                    var bA4 = retval.GetItem(PgcClassModel.Cods.bA4_Resultado_de_operaciones_Continuadas)!;
                    var b20 = retval.GetItem(PgcClassModel.Cods.bA320_Impuesto_sobre_beneficios)!;
                    bA4.Deb = bA3.Deb + b20.Deb;
                    bA4.Hab = bA3.Hab + b20.Hab;

                    //A5 == A4+21
                    var bA5 = retval.GetItem(PgcClassModel.Cods.bA5_Resultado_del_ejercicio)!;
                    var b21 = retval.GetItem(PgcClassModel.Cods.bB21_Resultado_OI_neto_de_Impuestos)!;
                    bA5.Deb = bA4.Deb + b21.Deb;
                    bA5.Hab = bA4.Hab + b21.Hab;

                    break;
            }
        }

        private PgcClassModel? Seed(BalanceModel balance)
        {
            PgcClassModel? retval = null;
            switch (balance.Mode)
            {
                case BalanceModel.Modes.Assets:
                    retval = Epigrafs()?.FirstOrDefault(x => x.Cod == PgcClassModel.Cods.aA_Activo);
                    break;
                case BalanceModel.Modes.Liabilities:
                    retval = Epigrafs()?.FirstOrDefault(x => x.Cod == PgcClassModel.Cods.aB_Pasivo);
                    break;
                case BalanceModel.Modes.PL:
                    retval = Epigrafs()?.FirstOrDefault(x => x.Cod == PgcClassModel.Cods.b_Cuenta_Explotacion);
                    break;
            }
            return retval;
        }

        private void AddCls(BalanceModel retval, PgcClassModel cls, int level = 0, BalanceModel.Item? parent = null)
        {
            var item = new BalanceModel.Item()
            {
                Tag = cls,
                Parent = parent,
                Nom = EpigrafNom(cls),
                Level = level,
            };
            retval.Items.Add(item);

            var ctas = CtasFromEpigraf(cls.Guid);

            var sdos = Values?
            .Where(x => x.Fch <= retval.Fch && (ctas?.Any(y => x.Cta == y.Guid) ?? false))
            .GroupBy(x => x.Cta)
            .Select(x => new PgcCtaSdoModel()
            {
                Cta = x.Key,
                Deb = x.Sum(y => y.Deb),
                Hab = x.Sum(y => y.Hab)
            })
            .ToList();

            foreach (var sdo in sdos ?? new())
            {
                AddSdo(retval, sdo, level + 1, item);
            }
            foreach (var child in ChildEpigrafs(cls) ?? new())
            {
                AddCls(retval, child, level + 1, item);
            }
        }

        private List<PgcClassModel>? ChildEpigrafs(PgcClassModel? parent = null)
        {
            var retval = Epigrafs()?
            .Where(x => x.Parent == (parent == null ? null : parent?.Guid))
            .OrderBy(x => x.Ord)
            //.OrderBy(x => EpigrafNom(x)?.Esp)
            .ToList();
            return retval;
        }

        private void AddSdo(BalanceModel retval, PgcCtaSdoModel sdo, int level = 0, BalanceModel.Item? parent = null)
        {
            var cta = GetCta(sdo.Cta);
            var item = new BalanceModel.Item()
            {
                Tag = sdo,
                Parent = parent,
                Nom = CtaIdNom(cta),
                Level = level,
                Deb = sdo.Deb,
                Hab = sdo.Hab,
                Visible = false
            };
            retval.Items.Add(item);
            Propagate(parent, sdo.Deb, sdo.Hab);

        }

        void Propagate(BalanceModel.Item? parent, decimal deb, decimal hab)
        {
            if (parent != null)
            {
                if (!parent.HideFigures())
                {
                    parent.Deb += deb;
                    parent.Hab += hab;
                }
                Propagate(parent.Parent, deb, hab);
            }
        }

        #endregion

        void NotifyChange(Exception? ex)
        {
            OnChange?.Invoke(ex);
        }

        public void Dispose()
        {
            contactsService.OnChange -= NotifyChange;
            pgcCtasService.OnChange -= NotifyChange;
            pgcClassesService.OnChange -= NotifyChange;
        }

        public class Item
        {
            public IModel? Tag { get; set; }
            public string? Label { get; set; }
            public decimal? Deb { get; set; }
            public decimal? Hab { get; set; }
            public decimal? SaldoDeutor { get; set; }
            public decimal? SaldoCreditor { get; set; }
            public decimal? Saldo { get; set; }

            public override string ToString()
            {
                return String.Format("{0} {1:N2} {2:N2} {3:N2} {4:N2}", Label, Deb, Hab, SaldoDeutor, SaldoCreditor);
            }
        }
    }

}

