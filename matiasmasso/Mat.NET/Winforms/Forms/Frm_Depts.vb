Public Class Frm_Depts
    Private _Depts As List(Of DTODept)
    Private _AllowEvents As Boolean

    Private Async Sub Frm_Depts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _Depts = Await FEB2.Depts.All(exs)
        If exs.Count = 0 Then
            Dim oBrands = Await FEB2.ProductBrands.All(exs, Current.Session.User)
            If exs.Count = 0 Then
                Dim oBrand As DTOProductBrand = Nothing
                If _Depts.Count > 0 Then
                    oBrand = _Depts.First.Brand
                End If
                Xl_ProductBrands1.Load(oBrands, DTO.Defaults.SelectionModes.Browse, , oBrand)
                refrescaDepts()
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
                Me.Close()
            End If
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If

    End Sub


    Private Sub Xl_ProductBrands1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductBrands1.ValueChanged
        If _AllowEvents Then
            refrescaDepts()
        End If
    End Sub

    Private Sub Xl_Depts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Depts1.RequestToRefresh
        reloadDepts()
    End Sub

    Private Sub Xl_Depts1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Depts1.RequestToAddNew
        Dim oDept = DTODept.Factory(Xl_ProductBrands1.Value)
        Dim oValues = Xl_Depts1.Values
        If oValues.Count > 0 Then oDept.Ord = oValues.Max(Function(x) x.Ord) + 1
        Dim oFrm As New Frm_Dept(oDept)
        AddHandler oFrm.AfterUpdate, AddressOf reloadDepts
        oFrm.Show()
    End Sub

    Private Async Sub reloadDepts()
        Dim exs As New List(Of Exception)
        _Depts = Await FEB2.Depts.All(exs)
        If exs.Count = 0 Then
            refrescaDepts()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub refrescaDepts()
        Dim oBrand = Xl_ProductBrands1.Value
        Dim oDepts = _Depts.Where(Function(x) x.Brand.Equals(oBrand)).ToList
        Xl_Depts1.Load(oDepts)
    End Sub
End Class