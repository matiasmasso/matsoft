Public Class PremiumLine
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOPremiumLine)
        Return Await Api.Fetch(Of DTOPremiumLine)(exs, "PremiumLine", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oPremiumLine As DTOPremiumLine, exs As List(Of Exception)) As Boolean
        If Not oPremiumLine.IsLoaded And Not oPremiumLine.IsNew Then
            Dim pPremiumLine = Api.FetchSync(Of DTOPremiumLine)(exs, "PremiumLine", oPremiumLine.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOPremiumLine)(pPremiumLine, oPremiumLine, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oPremiumLine As DTOPremiumLine, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOPremiumLine)(oPremiumLine, exs, "PremiumLine")
        oPremiumLine.IsNew = False
    End Function


    Shared Async Function Delete(oPremiumLine As DTOPremiumLine, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOPremiumLine)(oPremiumLine, exs, "PremiumLine")
    End Function

    Shared Async Function EmailRecipients(exs As List(Of Exception), oPremiumLine As DTOPremiumLine) As Task(Of List(Of DTOUser))
        Return Await Api.Fetch(Of List(Of DTOUser))(exs, "PremiumLine/EmailRecipients", oPremiumLine.Guid.ToString())
    End Function


    Shared Async Function ExcelEmailRecipients(exs As List(Of Exception), oPremiumLine As DTOPremiumLine, Optional oCustomersToFilter As List(Of DTOGuidNom) = Nothing) As Task(Of MatHelper.Excel.Sheet)
        Dim retval As MatHelper.Excel.Sheet = Nothing
        If PremiumLine.Load(oPremiumLine, exs) Then
            retval = New MatHelper.Excel.Sheet(oPremiumLine.Nom)

            With retval
                .AddColumn("Guid", MatHelper.Excel.Cell.NumberFormats.W50)
                .AddColumn("Llengua", MatHelper.Excel.Cell.NumberFormats.W50)
                .AddColumn("Email", MatHelper.Excel.Cell.NumberFormats.W50)
                .AddColumn("Pais", MatHelper.Excel.Cell.NumberFormats.W50)
                .AddColumn("Zona", MatHelper.Excel.Cell.NumberFormats.W50)
                .AddColumn("Població", MatHelper.Excel.Cell.NumberFormats.W50)
                .AddColumn("Client", MatHelper.Excel.Cell.NumberFormats.W50)
            End With

            Dim items As List(Of DTOUser) = Await PremiumLine.EmailRecipients(exs, oPremiumLine)
            If exs.Count = 0 Then
                For Each item As DTOUser In items
                    If oCustomersToFilter Is Nothing OrElse oCustomersToFilter.Any(Function(x) x.Guid.Equals(item.Contact.Guid)) Then
                        Dim orow As MatHelper.Excel.Row = retval.AddRow()
                        orow.AddCell(item.Guid.ToString())
                        orow.AddCell(item.Lang.Tag)
                        orow.AddCell(item.EmailAddress)
                        orow.AddCell(DTOAddress.Country(item.Contact.Address).ISO)
                        orow.AddCell(DTOAddress.Zona(item.Contact.Address).Nom)
                        orow.AddCell(DTOAddress.Location(item.Contact.Address).Nom)
                        orow.AddCell(item.Contact.NomAndNomComercial())
                    End If
                Next

            End If
        End If
        Return retval
    End Function

    Shared Function ExcelEmailRecipients(oRecipients As List(Of DTOUser)) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("Destinataris circular Premium Line")

        With retval
            .AddColumn("Guid", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Llengua", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Email", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Pais", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Zona", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Població", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Client", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Classificacio", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Canal", MatHelper.Excel.Cell.NumberFormats.W50)
        End With

        For Each item As DTOUser In oRecipients
            Dim orow As MatHelper.Excel.Row = retval.AddRow()
            orow.AddCell(item.Guid.ToString())
            orow.AddCell(item.Lang.Tag)
            orow.AddCell(item.EmailAddress)
            orow.AddCell(DTOAddress.Country(item.Contact.Address).ISO)
            orow.AddCell(DTOAddress.Zona(item.Contact.Address).Nom)
            orow.AddCell(DTOAddress.Location(item.Contact.Address).Nom)
            orow.AddCell(item.Contact.NomAndNomComercial())
            If item.Contact.ContactClass IsNot Nothing Then
                orow.AddCell(item.Contact.ContactClass.Nom.Esp)
                If item.Contact.ContactClass.DistributionChannel IsNot Nothing Then
                    orow.AddCell(item.Contact.ContactClass.DistributionChannel.LangText.Esp)
                End If
            End If
        Next

        Return retval
    End Function
End Class

Public Class PremiumLines
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOPremiumLine))
        Return Await Api.Fetch(Of List(Of DTOPremiumLine))(exs, "PremiumLines")
    End Function

End Class
