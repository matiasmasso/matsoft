Public Class Frm_Last_Incidencies
    Private _Query As DTOIncidenciaQuery
    Private _AllowEvents As Boolean

    Public Sub New(oQuery As DTOIncidenciaQuery)
        MyBase.New()
        InitializeComponent()
        _Query = oQuery
        LoadComboboxClose()
        With _Query
            If .Customer IsNot Nothing Then
                CheckBoxCustomer.Checked = True
                Xl_Contact21.Visible = True
                Xl_Contact21.Contact = .Customer
            End If
            If .Product IsNot Nothing Then
                CheckBoxProduct.Checked = True
                Xl_LookupProduct1.Visible = True
                Xl_LookupProduct1.Value = .Product
            End If
            If .Tancament IsNot Nothing Then
                ComboBoxClose.SelectedValue = .Tancament.Id
            End If
            Select Case .Src
                Case DTOIncidencia.Srcs.NotSet
                    CheckBoxSrcProducte.Checked = True
                    CheckBoxSrcTransport.Checked = True
                Case DTOIncidencia.Srcs.Producte
                    CheckBoxSrcProducte.Checked = True
                Case DTOIncidencia.Srcs.Transport
                    CheckBoxSrcTransport.Checked = True
            End Select
            CheckBoxIncludeClosed.Checked = .IncludeClosed
            ComboBoxClose.Enabled = CheckBoxIncludeClosed.Checked
        End With
        refresca()
        _AllowEvents = True
    End Sub

    Private Sub refresca()
        Application.DoEvents()
        Cursor = Cursors.WaitCursor
        With _Query
            .Src = GetSrc()
            .Product = GetProduct()
            .Customer = GetCustomer()
            .Tancament = GetTancament()
            .IncludeClosed = CheckBoxIncludeClosed.Checked
        End With
        BLL_Incidencies.LoadQuery(_Query)
        Xl_Incidencies1.Load(_Query)
        Cursor = Cursors.Default
    End Sub

    Private Function GetSrc() As DTOIncidencia.Srcs
        Dim retval As DTOIncidencia.Srcs = DTOIncidencia.Srcs.NotSet
        If CheckBoxSrcProducte.Checked And Not CheckBoxSrcTransport.Checked Then retval = DTOIncidencia.Srcs.Producte
        If CheckBoxSrcTransport.Checked And Not CheckBoxSrcProducte.Checked Then retval = DTOIncidencia.Srcs.Transport
        Return retval
    End Function

    Private Sub LoadComboboxClose()
        Dim oCodisDeTancament As List(Of DTOIncidenciaCod) = BLL_Incidencies.CodisDeTancament
        ComboBoxClose.Items.Clear()
        ComboBoxClose.Items.Add(New MaxiSrvr.ListItem(0, "(tots els codis de tancament)"))
        For Each oCodiDeTancament As DTOIncidenciaCod In oCodisDeTancament
            ComboBoxClose.Items.Add(New MaxiSrvr.ListItem(CInt(oCodiDeTancament.Id), oCodiDeTancament.Esp))
        Next
        ComboBoxClose.SelectedIndex = 0
    End Sub


    Private Sub ButtonExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExcel.Click
        Cursor = Cursors.WaitCursor
        Dim oExcelSheet As DTOExcelSheet = BLL_Incidencies.MatExcel(_Query)
        UIHelper.ShowExcel(oExcelSheet)
        Cursor = Cursors.Default
    End Sub


    Private Function GetProduct() As DTOProduct
        Dim retval As DTOProduct = Nothing
        If CheckBoxProduct.Checked Then
            retval = Xl_LookupProduct1.Product
        End If
        Return retval
    End Function

    Private Function GetCustomer() As DTOCustomer
        Dim retval As DTOCustomer = Nothing
        If CheckBoxCustomer.Checked Then
            If Xl_Contact21.Contact IsNot Nothing Then
                retval = New DTOCustomer(Xl_Contact21.Contact.Guid)
            End If
        End If
        Return retval
    End Function

    Private Function GetTancament() As DTOIncidenciaCod
        Dim retval As DTOIncidenciaCod = Nothing
        If ComboBoxClose.SelectedIndex > 0 Then
            Dim iCod As Integer = ComboBoxClose.SelectedItem.value
            retval = New DTOIncidenciaCod(iCod)
        End If
        Return retval
    End Function

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        Xl_Contact21.AfterUpdate, _
         Xl_LookupProduct1.AfterUpdate, _
          ComboBoxClose.SelectedIndexChanged, _
           CheckBoxSrcProducte.CheckedChanged, _
            CheckBoxSrcTransport.CheckedChanged

        If _AllowEvents Then
            refresca()
        End If
    End Sub

    Private Sub CheckBoxProduct_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxProduct.CheckedChanged
        If _AllowEvents Then
            Xl_LookupProduct1.Visible = CheckBoxProduct.Checked
            If Not CheckBoxProduct.Checked Then refresca()
        End If
    End Sub

    Private Sub CheckBoxCustomer_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxCustomer.CheckedChanged
        If _AllowEvents Then
            Xl_Contact21.Visible = CheckBoxCustomer.Checked
            If Not CheckBoxCustomer.Checked Then
                Xl_Contact21.Contact = Nothing
                refresca()
            End If
        End If
    End Sub

    Private Sub CheckBoxIncludeClosed_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxIncludeClosed.CheckedChanged
        If _AllowEvents Then
            ComboBoxClose.Enabled = CheckBoxIncludeClosed.Checked
            refresca()
        End If
    End Sub

    Private Sub Xl_Incidencies1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Incidencies1.RequestToRefresh
        refresca()
    End Sub
End Class