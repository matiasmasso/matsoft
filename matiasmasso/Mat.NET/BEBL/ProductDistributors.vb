Public Class ProductDistributors


    Shared Function List(oBrand As DTOProductBrand) As List(Of DTOProductRetailer)
        Dim retval As List(Of DTOProductRetailer) = ProductDistributorsLoader.FromBrand(oBrand)
        Return retval
    End Function

    Shared Function List(oProveidor As DTOProveidor) As List(Of DTOProductRetailer)
        Dim retval As List(Of DTOProductRetailer) = ProductDistributorsLoader.FromManufacturer(oProveidor)
        Return retval
    End Function

    Shared Function List(oUser As DTOUser) As List(Of DTOProductDistributor)
        Dim retval As List(Of DTOProductDistributor) = ProductDistributorsLoader.List(oUser)
        Return retval
    End Function

    Shared Function List(oRep As DTORep) As List(Of DTOProductDistributor)
        Dim retval As List(Of DTOProductDistributor) = ProductDistributorsLoader.List(oRep)
        Return retval
    End Function


    Shared Function ManufacturerCsv(oProveidor As DTOProveidor) As String
        Dim sb As New System.Text.StringBuilder
        Dim oDistributors As List(Of DTOProductRetailer) = List(oProveidor)
        For Each item As DTOProductRetailer In oDistributors
            sb.Append(Chr(34) & item.Country & Chr(34) & ";")
            sb.Append(Chr(34) & item.Region & Chr(34) & ";")
            sb.Append(Chr(34) & item.Location & Chr(34) & ";")
            sb.Append(Chr(34) & item.Name & Chr(34) & ";")
            sb.AppendLine(Chr(34) & item.Address & Chr(34))
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function



    Shared Function DistribuidorsOficials(oUser As DTOUser, oBrand As DTOProductBrand, Optional oIncentiu As DTOIncentiu = Nothing) As List(Of DTOProductDistributor)
        BEBL.User.Load(oUser)
        Dim retval As List(Of DTOProductDistributor) = ProductDistributorsLoader.DistribuidorsOficials(oUser, oBrand, oIncentiu)
        Return retval
    End Function

    Shared Function Zonas(oProduct As DTOProduct, oCountry As DTOCountry) As List(Of DTOZona)
        Dim retval As List(Of DTOZona) = ProductDistributorsLoader.GetZonas(oProduct, oCountry)
        Return retval
    End Function

    Shared Function BestLocation(oProduct As DTOProduct, Optional oParentArea As DTOArea = Nothing) As DTOLocation
        Dim retval As DTOLocation = ProductDistributorsLoader.BestLocation(oProduct, oParentArea)
        Return retval
    End Function

    Shared Function PerChannel(oUser As DTOUser, Optional iYear As Integer = 0) As MatHelper.Excel.Book
        If iYear = 0 Then iYear = DTO.GlobalVariables.Today().Year
        Dim retval As MatHelper.Excel.Book = ProductDistributorsLoader.PerChannel(oUser, iYear)
        Return retval
    End Function




    Shared Async Function StoreLocatorExcelMailing(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of Boolean)
        Dim oSubscription = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.StoreLocatorExcelMailing)
        Dim oAllSubscriptors As List(Of DTOSubscriptor) = BEBL.Subscription.SubscriptorsWithManufacturer(oSubscription)
        Dim oProveidors As List(Of DTOContact) = oAllSubscriptors.Select(Function(x) x.contact).Distinct.ToList
        If oProveidors.Count > 0 Then
            Dim sb As New Text.StringBuilder
            For Each oProveidor As DTOProveidor In oProveidors
                Dim oSubscriptors As List(Of DTOSubscriptor) = oAllSubscriptors.Where(Function(x) x.contact.Equals(oProveidor)).ToList
                If Await SendStoreLocatorExcel(oEmp, oProveidor, oSubscriptors, exs) Then
                    sb.AppendFormat("{0} Store locator successfully sent to {1}", oProveidor.nom, DTOSubscriptor.RecipientsString(oSubscriptors))
                Else
                    exs.Add(New Exception(String.Format("{0} Store locator failed to send to {1} due to next reasons:", oProveidor.nom, DTOSubscriptor.RecipientsString(oSubscriptors))))
                End If
            Next
        End If

        Return exs.Count = 0
    End Function

    Shared Async Function SendStoreLocatorExcel(oEmp As DTOEmp, oProveidor As DTOProveidor, oSubscriptors As List(Of DTOSubscriptor), exs As List(Of Exception)) As Task(Of Boolean)
        Dim recipients = oSubscriptors.Select(Function(x) x.emailAddress).ToList
        Dim oMailmessage = DTOMailMessage.Factory(recipients)
        With oMailmessage
            .bcc = {"matias@matiasmasso.es"}.ToList
            .subject = "Store Locator"
            .bodyUrl = BEBL.Mailing.BodyUrl(DTODefault.MailingTemplates.StoreLocator, oProveidor.Guid.ToString())
        End With
        Dim oSheet = BEBL.ProductDistributors.Excel(oProveidor)
        MailMessageHelper.AttachExcel(oMailmessage, oSheet, exs)
        Await MailMessageHelper.Send(oEmp, oMailmessage, exs)
        Return exs.Count = 0
    End Function


    Shared Function Excel(oProveidor As DTOProveidor) As MatHelper.Excel.Sheet
        Dim sTitle As String = String.Format("M+O {0} {1} {2:00}", DTO.GlobalVariables.Today().Year, DTO.GlobalVariables.Today().Month, DTO.GlobalVariables.Today().Day)
        Dim sFilename As String = String.Format("M+O Store Locator {0:yyyyMMdd}.xlsx", DTO.GlobalVariables.Today())
        Dim retval As New MatHelper.Excel.Sheet(sTitle, sFilename)
        With retval
            .AddColumn("Store Name")
            .AddColumn("Address")
            .AddColumn("City")
            .AddColumn("Postcode")
            .AddColumn("Region")
            .AddColumn("Country")
        End With

        Dim items As List(Of DTOProductDistributor) = BEBL.WebAtlas.distribuidors(oProveidor)
        For Each item In items
            Dim oRow As MatHelper.Excel.Row = retval.AddRow
            oRow.AddCell(item.Nom)
            oRow.AddCell(item.Adr)
            oRow.AddCell(item.Location.nom)
            oRow.AddCell(item.ZipCod)
            oRow.AddCell(item.Zona.nom)
            oRow.AddCell(item.Country.nom)
        Next

        Return retval
    End Function
End Class
