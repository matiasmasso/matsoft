using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using DTO.Helpers;
using iText.StyledXmlParser.Jsoup.Helper;
using Web.LocalComponents;

namespace Web.Services
{
    public class BancTransfersService : IDisposable
    {
        public List<BancModel.Transfer>? Values { get; set; }
        public event Action<Exception?>? OnChange;
        public DbState State { get; set; } = DbState.StandBy;

        private AppStateService appstate;
        private AtlasService atlasService;
        private EmpsService empsService;
        private IbansService ibansService;
        private PgcCtasService pgcCtasService;
        private BancsService bancsService;

        public BancTransfersService(AppStateService appstate,
            AtlasService atlasService,
            EmpsService empsService,
            IbansService ibansService,
            PgcCtasService pgcCtasService,
            BancsService bancsService)
        {
            this.appstate = appstate;
            this.atlasService = atlasService;
            this.empsService = empsService;
            this.ibansService = ibansService;
            this.pgcCtasService = pgcCtasService;
            this.bancsService = bancsService;


        atlasService.OnChange += NotifyChange;
            empsService.OnChange += NotifyChange;
            ibansService.OnChange += NotifyChange;
            pgcCtasService.OnChange += NotifyChange;
            bancsService.OnChange += NotifyChange;

            _ = Task.Run(async () => await FetchAsync());

        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    Values = await appstate.GetAsync<List<BancModel.Transfer>>("BancTransfers");
                    OnChange?.Invoke(null);
                }
                catch (Exception ex)
                {
                    State = DbState.Failed;
                    OnChange?.Invoke(ex);
                }
                State = DbState.StandBy;
            }
        }

        public EmpModel? Emp(EmpModel.EmpIds emp) => empsService.GetValue(emp);
        public IbanModel? ActiveIban(IbanModel.Cods cod, Guid contact) => ibansService.GetActiveValue(cod, contact);
        public BancModel? Banc(EmpModel.EmpIds emp) => bancsService.Values?.Where(x => x.Emp == emp && !x.Obsoleto).FirstOrDefault();
        public BancModel? Banc(Guid guid) => bancsService.GetValue(guid);
        public PgcCtaModel? PgcCta(PgcCtaModel.Cods cod) => pgcCtasService.GetValue(cod);

        public async Task UpdateAsync(List<BancModel.Transfer> transfers)
        {
            await appstate.PostAsync<List<BancModel.Transfer>, bool>(transfers, "bancTransfers");
        }

        public ContactModel? Beneficiari(BancModel.Transfer.Item item) => atlasService.Contact(item.Beneficiari);

        void NotifyChange(Exception? ex)
        {
            OnChange?.Invoke(ex);
        }

        public List<string> SepaDocs(List<BancModel.Transfer>? transfers)
        {
            List<string> retval = new();
            foreach (var transfer in transfers ?? new())
            {
                var file = SepaDoc(transfer);
                if (file != null) retval.Add(file);
            }
            return retval;
        }

        public string SepaDoc(BancModel.Transfer transfer)
        {
            var emp = empsService.GetValue((EmpModel.EmpIds)transfer.Emp()!);
            var org = atlasService.Contact(emp?.Org);
            if (org == null) throw new Exception("Sender company unknown");
            var nif = org.Nifs.Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(nif)) throw new Exception("Sender company Vat number not found");
            var ibans = ibansService.FromBanc(transfer.Banc);
            if (ibans == null || ibans.Count() == 0) throw new Exception("Sender bank is missing account number");
            if (ibans.Count() > 1) throw new Exception("Sender bank has multiple active account numbers");
            var iban = ibans.First();
            if (string.IsNullOrEmpty(iban.Ccc)) throw new Exception("Sender bank is missing account number");
            var swift = ibansService.Swift(iban.Branch);
            if (string.IsNullOrEmpty(swift)) throw new Exception($"sender bank is missing swift code for account {iban?.Ccc}");
            var sepaDoc = new DTO.Integracions.Sepa.Transfer
            {
                //Id = transfer.Cca!.Guid.ToString("N"),
                Id = transfer.Cca.FormattedId(),
                RaoSocialEmisor = org.RaoSocial,
                NifEmisor = nif,
                IbanEmisor = iban.Ccc,
                BancEmisorSwift = swift,
                Fch = (transfer.Fch() ?? DateTime.Today).Date
            };
            foreach (var item in transfer.Items)
            {
                var beneficiari = atlasService.Contact(item?.Beneficiari);
                if (string.IsNullOrEmpty(beneficiari?.RaoSocial)) throw new Exception($"Receiver company unknown {item?.Beneficiari.ToString()}");
                if (string.IsNullOrEmpty(item?.Ccc)) throw new Exception($"{beneficiari} is missing account number");
                swift = ibansService.Swift(item.BankBranch);
                if (string.IsNullOrEmpty(swift)) throw new Exception($"{beneficiari} bank is missing swift code for account {item?.Ccc}");
                if (item.Amount?.Eur == null || item.Amount.Eur <= 0) throw new Exception($"{beneficiari} amount {item.Amount?.Eur} is not valid");
                sepaDoc.Items.Add(
                    new DTO.Integracions.Sepa.Transfer.Item
                    {
                        Beneficiari = beneficiari.RaoSocial,
                        Iban = item.Ccc,
                        Swift = swift,
                        Concept = item.Concept,
                        Amount = (decimal)item.Amount.Eur
                    });
            }

            string? retval = sepaDoc.XML();
            return retval;
        }

        public void Dispose()
        {
            atlasService.OnChange -= NotifyChange;
            empsService.OnChange -= NotifyChange;
            ibansService.OnChange -= NotifyChange;
            pgcCtasService.OnChange -= NotifyChange;
            bancsService.OnChange -= NotifyChange;

        }
    }
}


