Public Class ContactDoc

    Shared Function Find(oGuid As Guid) As DTOContactDoc
        Dim retval As DTOContactDoc = ContactDocLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oContactDoc As DTOContactDoc) As Boolean
        Dim retval As Boolean = ContactDocLoader.Load(oContactDoc)
        Return retval
    End Function

    Shared Function Update(oContactDoc As DTOContactDoc, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ContactDocLoader.Update(oContactDoc, exs)
        Return retval
    End Function

    Shared Function Delete(oContactDoc As DTOContactDoc, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ContactDocLoader.Delete(oContactDoc, exs)
        Return retval
    End Function

    Shared Function Url(oContactDoc As DTOContactDoc, AbsoluteUrl As Boolean) As String
        Dim retval As String = ""
        If oContactDoc IsNot Nothing Then
            retval = DTOWebDomain.Default(AbsoluteUrl).Url(CInt(DTODocFile.Cods.download), CryptoHelper.StringToHexadecimal(oContactDoc.DocFile.Hash))
        End If
        Return retval
    End Function
End Class

Public Class ContactDocs

    Shared Function All(oContact As DTOContact, Optional iYea As Integer = 0, Optional oType As DTOContactDoc.Types = DTOContactDoc.Types.NotSet) As List(Of DTOContactDoc)
        Dim retval As List(Of DTOContactDoc) = ContactDocsLoader.All(oContact, iYea, oType)
        Return retval
    End Function

    Shared Function All(oUser As DTOUser, Optional oType As DTOContactDoc.Types = DTOContactDoc.Types.NotSet) As List(Of DTOContactDoc)
        Dim retval = ContactDocsLoader.All(oUser, oType)
        Return retval
    End Function

    Shared Function All(oContact As DTOContact, Optional oType As DTOContactDoc.Types = DTOContactDoc.Types.NotSet) As List(Of DTOContactDoc)
        Dim retval = ContactDocsLoader.All(oContact, oType:=oType)
        Return retval
    End Function

    Shared Function Mod145s(oExercici As DTOExercici) As List(Of DTOContactDoc)
        Dim retval As List(Of DTOContactDoc) = ContactDocsLoader.Mod145s(oExercici)
        Return retval
    End Function

    Shared Function Retencions(oContacts As List(Of DTOContact)) As List(Of DTOContactDoc)
        Dim retval As List(Of DTOContactDoc) = ContactDocsLoader.Retencions(oContacts)
        Return retval
    End Function

    Shared Function ExcelMod145s(oExercici As DTOExercici) As MatHelper.Excel.Sheet
        Dim sFilename As String = String.Format("{0}.{1} Models 145.xlsx", oExercici.Emp.Org.PrimaryNifValue(), oExercici.Year)
        Dim retval As New MatHelper.Excel.Sheet(oExercici.Year, sFilename)

        With retval
            .AddColumn("Nom i cognoms", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Nif", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Data", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
        End With

        Dim items As List(Of DTOContactDoc) = Mod145s(oExercici)
        For Each item As DTOContactDoc In items
            Dim oRow As MatHelper.Excel.Row = retval.AddRow
            With item
                oRow.AddCell(.Contact.Nom, BEBL.ContactDoc.Url(item, True))
                oRow.AddCell(.Contact.PrimaryNifValue())
                oRow.AddCell(.Fch)
            End With
        Next
        Return retval
    End Function


End Class

