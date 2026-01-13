Public Class Frm_Incentiu
    Private _Incentiu As DTOIncentiu
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOIncentiu)
        MyBase.New()
        Me.InitializeComponent()
        _Incentiu = value
        BLL.BLLIncentiu.Load(_Incentiu)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        'LoadEventos()
        With _Incentiu
            If _Incentiu.FchFrom > Date.MinValue Then
                DateTimePickerFchFrom.Value = .FchFrom
            Else
                DateTimePickerFchFrom.Value = Today
            End If
            If _Incentiu.FchTo <> Nothing Then
                DateTimePickerFchTo.Value = .FchTo
                DateTimePickerFchTo.Visible = True
                CheckBoxTo.Checked = True
            End If

            If _Incentiu.Evento IsNot Nothing Then
                'For Each itm As Evento In ComboBoxEventos.Items
                'If Itm.Id = _Incentiu.Evento.Id Then
                'ComboBoxEventos.SelectedItem = Itm
                'End If
                'Next
            End If

            CheckBoxOnlyInStock.Checked = .OnlyInStk

            RadioButtonDto.Checked = (.Cod = MaxiSrvr.Incentiu.Cods.Dto)
            RadioButtonFreeUnits.Checked = (.Cod = MaxiSrvr.Incentiu.Cods.FreeUnits)
            EnableLists()

            Xl_Products1.Load(_Incentiu.Products)
            Xl_QtyDtos1.Load(_Incentiu.QtyDtos, CurrentCod)

            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew

        End With
        _AllowEvents = True
    End Sub

    Private Sub EnableLists()
        Dim BlEnabled As Boolean = (RadioButtonDto.Checked Or RadioButtonFreeUnits.Checked)
        GroupBoxProducts.Enabled = BlEnabled
        GroupBoxQtyDtos.Enabled = BlEnabled

        If BlEnabled Then
            Dim BlDtos As Boolean = RadioButtonDto.Checked
            GroupBoxQtyDtos.Text = IIf(BlDtos, "descomptes", "quantitats")
            LabelQty.Text = IIf(BlDtos, "quantitat", "amb carrec")
            LabelDto.Text = IIf(BlDtos, "descompte", "s/carrec")
            Xl_TextBoxNumDto.Mat_CustomFormat = IIf(BlDtos, Xl_TextBoxNum.Formats.percent, Xl_TextBoxNum.Formats.Numeric)

            'If DataGridViewDtos.Columns.Count > ColsDto.Dto Then
            ' Dim sFormat As String = IIf(BlDtos, "#\%;-#\%;#", "#")
            'DataGridViewDtos.Columns(ColsDto.Dto).DefaultCellStyle.Format = sFormat
            ' End If
        End If
    End Sub

    Private Function CurrentCod() As Incentiu.Cods
        Dim oCod As Incentiu.Cods = MaxiSrvr.Incentiu.Cods.NotSet
        If RadioButtonDto.Checked Then
            oCod = MaxiSrvr.Incentiu.Cods.Dto
        ElseIf RadioButtonFreeUnits.Checked Then
            oCod = MaxiSrvr.Incentiu.Cods.FreeUnits
        End If
        Return oCod
    End Function

    Private Sub SetButtons()
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Incentiu
            .NomEsp = TextBoxNom.Text
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxTo.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Nothing
            End If
            '.Evento = ComboBoxEventos.SelectedItem
            .OnlyInStk = CheckBoxOnlyInStock.Checked
            .Products = Xl_Products1.Values
            '.QtyDtos = GetQtyDtosFromGrid()
            .Cod = CurrentCod()

            Dim exs As New List(Of Exception)
            If BLL.BLLIncentiu.Update(_Incentiu, exs) Then
                RaiseEvent AfterUpdate(sender, e)
                Me.Close()
            Else
                MsgBox("fallo al grabar incentiu", MsgBoxStyle.Exclamation)
            End If
        End With
    End Sub


    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem de la tabla?", MsgBoxStyle.OkCancel, "INCENTIUS")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLIncentiu.Delete(_Incentiu, exs) Then
                MsgBox("eliminat", MsgBoxStyle.Information, "INCENTIUS")
                RaiseEvent AfterUpdate(sender, e)
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "INCENTIUS")
        End If
    End Sub

    Private Sub TextBoxNom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNom.TextChanged
        If _AllowEvents Then
            SetButtons()
        End If
    End Sub


    Private Sub Xl_LookupProduct1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
        ButtonAddProduct.Enabled = True
    End Sub

    Private Sub ButtonAddProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAddProduct.Click
        Dim oProducts As List(Of DTOProduct) = Xl_Products1.Values
        oProducts.Add(Xl_LookupProduct1.Product)
        Xl_Products1.Load(oProducts)
        Xl_LookupProduct1.Clear()
        ButtonAddProduct.Enabled = False
        SetButtons()
    End Sub

    Private Sub ButtonAddQtyDto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAddQtyDto.Click
        Dim oQtyDtos As List(Of DTOQtyDto) = Xl_QtyDtos1.Values
        Dim item As New DTOQtyDto
        With item
            .Qty = Xl_TextBoxNumQty.Value
            If CurrentCod() = Incentiu.Cods.Dto Then
                .Dto = Xl_TextBoxNumDto.Value
            Else
                .FreeUnits = Xl_TextBoxNumDto.Value
            End If
        End With
        oQtyDtos.Add(item)
        Xl_QtyDtos1.Load(oQtyDtos, CurrentCod)
        Xl_TextBoxNumQty.Clear()
        Xl_TextBoxNumDto.Clear()
        SetButtons()
    End Sub

    Private Sub Xl_TextBoxNumQtyDto_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_TextBoxNumQty.AfterUpdate, Xl_TextBoxNumDto.AfterUpdate
        If _AllowEvents Then
            If Xl_TextBoxNumDto.Value <> 0 And Xl_TextBoxNumQty.Value <> 0 Then
                ButtonAddQtyDto.Enabled = True
            End If
        End If
    End Sub

    Private Sub CheckBoxTo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxTo.CheckedChanged
        DateTimePickerFchTo.Visible = True
        SetButtons()
    End Sub

    Private Sub CheckBoxOnlyInStock_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxOnlyInStock.CheckedChanged
        SetButtons()
    End Sub

    Private Sub RadioButtonQtyDto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        RadioButtonDto.CheckedChanged, _
         RadioButtonFreeUnits.CheckedChanged

        If _AllowEvents Then
            EnableLists()

            Dim oQtyDtos As List(Of DTOQtyDto) = Xl_QtyDtos1.Values
            Xl_QtyDtos1.Load(oQtyDtos, CurrentCod)

        End If
    End Sub
End Class