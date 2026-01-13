Public Class SepaCoreMandatoManualController
    Inherits _MatController


    Function Index() As ActionResult
        ViewBag.Title = ContextHelper.Lang.Tradueix("mandato bancario SEPA CORE", "mandat bancari SEPA CORE", "SEPA CORE Direct Debit Mandate", "mandato bancario SEPA CORE")
        Return View("SepaCoreMandatoManual")
    End Function


End Class
