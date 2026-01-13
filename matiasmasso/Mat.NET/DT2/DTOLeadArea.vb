Public Class DTOLeadArea
    Property Area As Object
    Property Leads As List(Of DTOLeadChecked)

    Public Sub New(oArea As DTOArea)
        MyBase.New()
        _Area = oArea
        _Leads = New List(Of DTOLeadChecked)
    End Sub

    Shared Function Csv(values As List(Of DTOLeadArea)) As DTOCsv
        Dim retval As New DTOCsv

        For Each value As DTOLeadArea In values
            For Each item As DTOLeadChecked In value.Leads.FindAll(Function(x) x.Checked = True)
                Dim oRow As New DTOCsvRow
                oRow.Cells.Add(item.EmailAddress)
                retval.Rows.Add(oRow)
            Next
        Next
        Return retval
    End Function

End Class

Public Class DTOLeadChecked
    Inherits DTOUser
    Property Checked As Boolean
    Property Brand As DTOProductBrand

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub
End Class
