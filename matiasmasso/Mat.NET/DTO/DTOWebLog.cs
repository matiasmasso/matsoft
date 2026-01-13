using System;

namespace DTO
{
    public class DTOWebLog
    {
        public DateTime Fch { get; set; }
        public DTOUser User { get; set; }
        public LogCodes Cod { get; set; }

        public enum LogCodes
        {
            NotSet,
            PasswordRequest,
            ShowTarifas,
            ShowFacturas,
            EditPwd,
            UploadFile,
            Nomina,
            CliPncs,
            EShopPunts,
            RepLiqs,
            Retenciones,
            ProWelcome,
            Catalogos,
            Zonas,
            Mxf,
            ShowImpagats,
            Logos,
            Outlet,
            RepBasket,
            CliBasket,
            RepReportsDiari,
            Clis,
            ArtRanking,
            Forecast,
            Cyc,
            RepFras,
            Mod347,
            Tc1,
            ArtPromos,
            Tarifa,
            BancRenovacions,
            iMatGetUsrFromEmail,
            CsvCataleg,
            RocheExport,
            IncidenciaNew,
            RepCliApertures,
            Incentius,
            AEATModels,
            GuidxDeveloper,
            TarifaExcel,
            TpaEBooks,
            WsEShopsStocks,
            ProRepComFollowUpSummary,
            ProRepComFollowUpDay,
            ProRepComFollowUpPdc,
            ProRepComFollowUpPnc,
            ProRepComFollowUpAlb,
            ProRepComFollowUpFra,
            ProRepLiq,
            SellOut,
            Raffles,
            ProductDistributorsCsv,
            RepComFollowUps
        }
    }
}
