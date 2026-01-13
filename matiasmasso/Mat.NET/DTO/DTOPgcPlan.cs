using System;

namespace DTO
{
    public class DTOPgcPlan : DTOBaseGuid
    {
        public string nom { get; set; }
        public int yearFrom { get; set; }
        public int yearTo { get; set; }
        public DTOPgcPlan lastPlan { get; set; }


        public enum Ctas
        {
            NotSet = 0,
            ResultatsAnyAnterior = 120,
            ResultatsAnyCorrent = 130,
            CreditsBancaris = 170,
            InmobilitzatIntangible = 203,
            InmobilitzatSoftware = 206,
            InmobilitzatTerrenys = 210,
            InmobilitzatConstruccions = 211,
            InmobilitzatInstalacionsAltres = 215,
            InmobilitzatMobles = 216,
            InmobilitzatHardware = 217,
            InmobilitzatVehicles = 218,
            InmobilitzatAltres = 219,
            AmrtAcumIntangible = 2803,
            AmrtAcumSoftware = 2806,
            AmrtAcumConstruccions = 2811,
            AmrtAcumInstalacionsAltres = 2815,
            AmrtAcumMobles = 2816,
            AmrtAcumHardware = 2817,
            AmrtAcumVehicles = 2818,
            AmrtAcumAltres = 2819,
            Mercancies = 30000,
            MercanciesTransit = 30004,
            MercanciesObsoletes = 30005,
            ProveidorsEur = 4000,
            ProveidorsUsd = 40040,
            ProveidorsGbp = 40041,
            FrasPendentsDeRebre = 40090,
            AbonamentsPendentsDeRebre = 40091,
            FrasComisionsPendentsDeRebre = 40092,
            FrasTransportPendentsDeRebre = 40093,
            ServeisEur = 41000,
            VisasPagadas = 413,
            Clients = 430,
            ClientsXecsEnCartera = 4310,
            ClientsXecsDescomptats = 4311,
            ClientsXecsAlCobro = 4312,
            Impagats = 4315,
            ImpagatsPagosACompte = 43155,
            Clients_Anticips = 438,
            TransferenciesDesconegudes = 43809,
            DeutorsVaris = 440,
            RepsCobrosClients = 44007,
            VisasCobradas = 44008,
            AnticipsTreballadors = 460,
            PagasTreballadors = 465,
            RetencionsInteresos = 473,
            Irpf = 47510,
            IrpfTreballadors = 47511,
            IrpfProfessionals = 47512,
            IrpfLloguers = 47513,
            NominaEmbargos = 47516,
            NominaDeutes = 544,
            Caixa = 570,
            Bancs = 572,
            BancsEfectesDescomptats = 52080,
            BancsPagaresDescomptats = 52082,
            IvaDeutor = 470,
            IvaSoportatNacional = 47200,
            IvaSoportatIntracomunitari = 47201,
            IvaSoportatImportacio = 47202,
            IvaRepercutitNacional = 47710,
            IvaRepercutitIntracomunitari = 47711,
            IvaRepercutitImportacio = 47712,
            IvaReduit = 4772,
            IvaSuperReduit = 4773,
            IvaRecarrecEquivalencia = 4774,
            IvaRecarrecReduit = 4775,
            IvaRecarrecSuperReduit = 4776,
            LeasingACurt = 52401,
            Marketplaces = 55250,
            DipositIrrevocableDeCompra = 56101,
            Compras = 600,
            Transport_internacional_aranzels = 60051,
            Transport_internacional_fletes = 60055,
            Transport_internacional_despeses = 60056,
            DevolucionsDeCompres = 608,
            Inventari = 6100,
            Lloguers = 62110,
            DespesesBancaries = 6260,
            DespesesCobrament = 6261,
            DespesesPagament = 6262,
            DespesesImpago = 6265,
            ComisionsRepresentants = 6231,
            InteresosNegogiacioEfectes = 664,
            Nomina = 6400,
            Dietas = 6401,
            Indemnitzacions = 641,
            SegSocialDevengo = 642,
            SegSocialCreditor = 476,
            DiferenciesDeCanvi = 668,
            DotacioAmortitzacioIntangible = 680,
            DotacioAmortitzacioMaterial = 681,
            InventariDesvaloritzacio = 6931,
            Vendes = 700,
            DevolucionsDeVendes = 708,
            Indemintzacions = 75901,
            Redondeos = 75903,
            ImpagosRecuperacioDespeses = 75905,
            IngresosDiversos = 75909,
            IngressosLloguersVivendas = 75201,
            IngressosLloguersLocals = 70551,
        }


        public DTOPgcPlan() : base()
        {
        }

        public DTOPgcPlan(Guid oGuid) : base(oGuid)
        {
        }
    }
}
