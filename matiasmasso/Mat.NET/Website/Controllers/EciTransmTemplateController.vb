Public Class EciTransmTemplateController
    Inherits _MatController

    <HttpGet>
    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oLang = ContextHelper.Lang

        Dim oHolding = DTOHolding.Wellknown(DTOHolding.Wellknowns.elCorteIngles)
        Dim model = Await FEB.Transmisions.HoldingHeaders(exs, oHolding, 10)
        If exs.Count = 0 Then
            ViewBag.Title = "El Corte Ingles. " & oLang.Tradueix("Plantilla de Modificación de Fecha entrega", "Plantilla de Modificació de data d'entrega", "Delivery date modification Excel template")
            Return View(model)
        Else
            Return Await MyBase.ErrorResult(exs)
        End If
    End Function


    <HttpPost>
    Async Function Excel(fch As Date, transms As List(Of Guid)) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oOrders = Await FEB.Transmisions.Orders(exs, transms)
        If exs.Count = 0 Then
            Dim oPlantilla = DTO.Integracions.ElCorteIngles.PlantillaModificacion.Factory(fch.Date(), oOrders)
            Dim oSheet = oPlantilla.ExcelSheet()
            retval = Await MyBase.ExcelResult(oSheet)
        Else
            Throw New Exception("OlaKAse")
            ' retval = PartialView("Error")
        End If

        Return retval
    End Function
End Class
