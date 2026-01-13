Public Class CustomerTarifaController
    Inherits _MatController

    Async Function TarifaByCustomer(customer As Guid, fch As Nullable(Of Long)) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oCustomer = Await FEB2.Customer.Find(exs, customer)
        Dim DtFch As Date = Nothing
        If fch IsNot Nothing Then DtFch = TextHelper.DecodedDate(fch)
        Dim oTarifa = Await FEB2.CustomerTarifa.Load(exs, oCustomer, DtFch)
        Dim oVarios = oTarifa.Brands.FirstOrDefault(Function(x) x.Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Varios)))
        If oVarios IsNot Nothing Then
            oTarifa.Brands.Remove(oVarios)
        End If
        Dim oUser = ContextHelper.GetUser()
        Select Case oUser.Rol.id
            Case DTORol.Ids.CliLite
                oTarifa.costEnabled = False
        End Select

        Return LoginOrView("tarifa", oTarifa)
    End Function

    Async Function TarifaByUser(fch As Nullable(Of Long)) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser Is Nothing Then
            retval = LoginOrView("tarifa")
        Else
            Dim DtFch As Date = Nothing
            If fch IsNot Nothing Then DtFch = TextHelper.DecodedDate(fch)
            Dim oTarifa = Await FEB2.CustomerTarifa.Load(exs, oUser, DtFch)
            Dim oVarios = oTarifa.Brands.FirstOrDefault(Function(x) x.Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Varios)))
            If oVarios IsNot Nothing Then
                oTarifa.Brands.Remove(oVarios)
            End If
            Select Case oUser.Rol.id
                Case DTORol.Ids.cliLite
                    oTarifa.CostEnabled = False
            End Select

            If DtFch = Nothing Then DtFch = Today
            ViewBag.Title = Mvc.ContextHelper.Tradueix("Tarifa de precios a ", "Tarifa de preus a ", "Price list at ") & Format(DtFch, "dd/MM/yyyy")
            retval = LoginOrView("tarifa", oTarifa)
        End If
        Return retval
    End Function



End Class