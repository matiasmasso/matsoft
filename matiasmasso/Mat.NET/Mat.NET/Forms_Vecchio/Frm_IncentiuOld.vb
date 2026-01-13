

Public Class Frm_IncentiuOld
    Private mIncentiu As Incentiu
    Private mDsStps As DataSet
    Private mDsQtyDtos As DataSet
    Private mEmp As DTOEmp = BLL.BLLApp.Emp
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum ColsProduct
        Text
    End Enum

    Private Enum ColsDto
        Qty
        Dto
    End Enum

    Public WriteOnly Property Incentiu() As Incentiu
        Set(ByVal Value As Incentiu)
            mIncentiu = Value
            LoadEventos()
            Refresca()
            mAllowEvents = True
        End Set
    End Property

    Private Sub Refresca()
        With mIncentiu
            TextBoxNom.Text = .Nom
            If mIncentiu.FchFrom > Date.MinValue Then
                DateTimePickerFchFrom.Value = .FchFrom
            Else
                DateTimePickerFchFrom.Value = Today
            End If
            If mIncentiu.FchTo > Date.MinValue And _
            mIncentiu.FchTo.Year < 2100 Then
                DateTimePickerFchTo.Value = .FchTo
                DateTimePickerFchTo.Visible = True
                CheckBoxTo.Checked = True
            Else
                DateTimePickerFchTo.Visible = False
                CheckBoxTo.Checked = False
            End If

            If mIncentiu.Evento IsNot Nothing Then
                For Each itm As Evento In ComboBoxEventos.Items
                    If itm.Id = mIncentiu.Evento.Id Then
                        ComboBoxEventos.SelectedItem = itm
                    End If
                Next
            End If

            CheckBoxOnlyInStock.Checked = .OnlyInStk

            RadioButtonDto.Checked = (.Cod = MaxiSrvr.Incentiu.Cods.Dto)
            RadioButtonFreeUnits.Checked = (.Cod = MaxiSrvr.Incentiu.Cods.FreeUnits)
            EnableLists()

            LoadGridProducts(mIncentiu.Products)
            LoadGridDtos(mIncentiu.QtyDtos)
            'ButtonDel.Enabled = .Id > 0
        End With
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

            If DataGridViewDtos.Columns.Count > ColsDto.Dto Then
                Dim sFormat As String = IIf(BlDtos, "#\%;-#\%;#", "#")
                DataGridViewDtos.Columns(ColsDto.Dto).DefaultCellStyle.Format = sFormat
            End If
        End If
    End Sub

    Private Sub LoadGridProducts(ByVal oProducts As Products)
        With DataGridViewProducts
            .RowTemplate.Height = .Font.Height * 1.3
            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add(New DataGridViewTextBoxColumn)
            .DataSource = oProducts
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToDeleteRows = True

            With .Columns(ColsProduct.Text)
                .DataPropertyName = "text"
                .HeaderText = "producte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With

    End Sub

    Private Sub LoadGridDtos(ByVal oQtyDtos As QtyDtos)
        Dim oTb As New DataTable
        With oTb.Columns
            .Add("QTY", System.Type.GetType("System.Int32"))
            .Add("DTO", System.Type.GetType("System.Int32"))
        End With
        Dim oItm As QtyDto
        Dim oRow As DataRow
        For Each oItm In oQtyDtos
            oRow = oTb.NewRow
            oRow(ColsDto.Qty) = oItm.Qty
            If RadioButtonDto.Checked Then
                oRow(ColsDto.Dto) = oItm.Dto
            Else
                oRow(ColsDto.Dto) = oItm.FreeUnits
            End If
            oTb.Rows.Add(oRow)
        Next
        mDsQtyDtos = New DataSet
        mDsQtyDtos.Tables.Add(oTb)
        With DataGridViewDtos
            .RowTemplate.Height = .Font.Height * 1.3
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToDeleteRows = True
            With .Columns(ColsDto.Qty)
                .HeaderText = "a partir de..."
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsDto.Dto)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                If RadioButtonDto.Checked Then
                    .HeaderText = "dte."
                    .DefaultCellStyle.Format = "#\%;-#\%;#"
                Else
                    .HeaderText = "s/carrec"
                    .DefaultCellStyle.Format = "#"
                End If
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With

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
        With mIncentiu
            .NomEsp = TextBoxNom.Text
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxTo.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Nothing
            End If
            .Evento = ComboBoxEventos.SelectedItem
            .OnlyInStk = CheckBoxOnlyInStock.Checked
            .Products = GetProductsFromGrid()
            .QtyDtos = GetQtyDtosFromGrid()
            .Cod = CurrentCod()
            If .Update() Then
                RaiseEvent AfterUpdate(sender, e)
                Me.Close()
            Else
                MsgBox("fallo al grabar incentiu", MsgBoxStyle.Exclamation)
            End If
        End With
    End Sub

    Private Function GetProductsFromGrid() As Products
        Dim retval As Products = CType(DataGridViewProducts.DataSource, Products)
        Return retval
    End Function

    Private Function GetQtyDtosFromGrid() As QtyDtos
        Dim oQtyDtos As New QtyDtos
        Dim oQtyDto As QtyDto = Nothing
        Dim oRow As DataRow = Nothing
        Dim oTb As DataTable = mDsQtyDtos.Tables(0)
        For Each oRow In oTb.Rows
            Select Case CurrentCod()
                Case MaxiSrvr.Incentiu.Cods.Dto
                    oQtyDto = New QtyDto(oRow(ColsDto.Qty), oRow(ColsDto.Dto))
                Case MaxiSrvr.Incentiu.Cods.FreeUnits
                    oQtyDto = New QtyDto(oRow(ColsDto.Qty), , oRow(ColsDto.Dto))
            End Select
            oQtyDtos.Add(oQtyDto)
        Next
        Return oQtyDtos
    End Function

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem de la tabla?", MsgBoxStyle.OkCancel, "INCENTIUS")
        If rc = MsgBoxResult.Ok Then
            mIncentiu.Delete()
            MsgBox("eliminat", MsgBoxStyle.Information, "INCENTIUS")
            RaiseEvent AfterUpdate(sender, e)
            Me.Close()
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "INCENTIUS")
        End If
    End Sub

    Private Sub TextBoxNom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNom.TextChanged
        If mAllowEvents Then
            SetButtons()
        End If
    End Sub

    Private Sub DataGridView_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs)
        If mAllowEvents Then
            SetButtons()
        End If
    End Sub

    Private Sub DataGridView_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
        If mAllowEvents Then
            SetButtons()
        End If
    End Sub

    Private Sub Xl_LookupProduct1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_LookupProduct1.AfterUpdate
        ButtonAddProduct.Enabled = True
    End Sub

    Private Sub ButtonAddProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAddProduct.Click
        Dim oProducts As Products = GetProductsFromGrid()
        oProducts.Add(Xl_LookupProduct1.Product)
        LoadGridProducts(oProducts)
        Xl_LookupProduct1.Clear()
        'ButtonAddProduct.Enabled = False
        SetButtons()
    End Sub

    Private Sub ButtonAddQtyDto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAddQtyDto.Click
        Dim oQtyDtos As QtyDtos = GetQtyDtosFromGrid()
        If RadioButtonDto.Checked Then
            oQtyDtos.Add(New QtyDto(Xl_TextBoxNumQty.Value, Xl_TextBoxNumDto.Value))
        Else
            oQtyDtos.Add(New QtyDto(Xl_TextBoxNumQty.Value, , Xl_TextBoxNumDto.Value))
        End If
        LoadGridDtos(oQtyDtos)
        Xl_TextBoxNumQty.clear()
        Xl_TextBoxNumDto.clear()
        SetButtons()
    End Sub

    Private Sub Xl_TextBoxNumQtyDto_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_TextBoxNumQty.AfterUpdate, Xl_TextBoxNumDto.AfterUpdate
        If mAllowEvents Then
            If Xl_TextBoxNumDto.Value <> 0 And Xl_TextBoxNumQty.Value <> 0 Then
                ButtonAddQtyDto.Enabled = True
            End If
        End If
    End Sub

    Private Sub DataGridViewProducts_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles DataGridViewProducts.UserDeletedRow
        SetButtons()
    End Sub

    Private Sub DataGridViewDtos_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles DataGridViewDtos.UserDeletedRow
        SetButtons()
    End Sub

    Private Sub CheckBoxTo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxTo.CheckedChanged
        DateTimePickerFchTo.Visible = True
        SetButtons()
    End Sub

    Private Sub CheckBoxOnlyInStock_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxOnlyInStock.CheckedChanged
        SetButtons()
    End Sub

    Private Sub LoadEventos()
        Dim SQL As String = "SELECT id,ESP,CAT,ENG FROM EVENT ORDER BY FCHTO DESC"
        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)

        Dim oEvento As New Evento()
        oEvento.Esp = "(seleccionar evento)"
        ComboBoxEventos.Items.Add(oEvento)
        Do While oDrd.Read
            ComboBoxEventos.Items.Add(New Evento(CInt(oDrd("ID"))))
        Loop
        oDrd.Close()

    End Sub

    Private Sub RadioButtonQtyDto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        RadioButtonDto.CheckedChanged, _
         RadioButtonFreeUnits.CheckedChanged

        If mAllowEvents Then
            EnableLists()
        End If
    End Sub
End Class