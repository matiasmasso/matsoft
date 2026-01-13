

Public Class Frm_Insolvencia

    Private mInsolvencia As Insolvencia
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        ImpagatGuid
        CsbGuid
        Yea
        Csa
        Csb
        Eur
        Vto
        Txt
    End Enum

    Public WriteOnly Property Insolvencia() As Insolvencia
        Set(ByVal value As Insolvencia)
            mInsolvencia = value
            With mInsolvencia
                TextBoxCli.Text = .Contact.Nom
                Xl_AmtNominal.Amt = .Nominal
                Xl_AmtPagatACompte.Amt = .PagatACompte
                Xl_AmtGastos.Amt = .Gastos
                Xl_AmtComisio.Amt = .Comisio

                If .FchPresentacio > DateTime.MinValue Then
                    CheckBoxPresentacio.Checked = True
                    DateTimePickerPresentacio.Value = .FchPresentacio
                Else
                    DateTimePickerPresentacio.Visible = False
                End If

                If .FchAdmisio > DateTime.MinValue Then
                    CheckBoxAdmisio.Checked = True
                    DateTimePickerAdmisio.Value = .FchAdmisio
                Else
                    DateTimePickerAdmisio.Visible = False
                End If

                If .FchLiquidacio > DateTime.MinValue Then
                    CheckBoxLiquidacio.Checked = True
                    DateTimePickerLiquidacio.Value = .FchLiquidacio
                Else
                    DateTimePickerLiquidacio.Visible = False
                End If

                If .FchRehabilitacio > DateTime.MinValue Then
                    CheckBoxRehabilitacio.Checked = True
                    DateTimePickerRehabilitacio.Value = .FchRehabilitacio
                Else
                    DateTimePickerRehabilitacio.Visible = False
                End If

                LoadGrid()

                If .Exists Then
                    ButtonDel.Enabled = True
                Else
                    ButtonOk.Enabled = True
                End If
            End With
            mAllowEvents = True
        End Set
    End Property

    Private Sub CheckBoxPresentacio_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxPresentacio.CheckedChanged
        DateTimePickerPresentacio.Visible = CheckBoxPresentacio.Checked
        If mallowevents Then ButtonOk.Enabled = True
    End Sub

    Private Sub CheckBoxAdmisio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxAdmisio.CheckedChanged
        DateTimePickerAdmisio.Visible = CheckBoxAdmisio.Checked
        If mallowevents Then ButtonOk.Enabled = True
    End Sub

    Private Sub CheckBoxLiquidacio_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxLiquidacio.CheckedChanged
        DateTimePickerLiquidacio.Visible = CheckBoxLiquidacio.Checked
        If mallowevents Then ButtonOk.Enabled = True
    End Sub

    Private Sub CheckBoxRehabilitacio_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxRehabilitacio.CheckedChanged
        DateTimePickerRehabilitacio.Visible = CheckBoxRehabilitacio.Checked
        If mallowevents Then ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mInsolvencia
            .Nominal = Xl_AmtNominal.Amt
            .PagatACompte = Xl_AmtPagatACompte.Amt
            .Gastos = Xl_AmtGastos.Amt
            .Comisio = Xl_AmtComisio.Amt

            If CheckBoxPresentacio.Checked Then
                .FchPresentacio = DateTimePickerPresentacio.Value
            Else
                .FchPresentacio = Date.MinValue
            End If

            If CheckBoxAdmisio.Checked Then
                .FchAdmisio = DateTimePickerAdmisio.Value
            Else
                .FchAdmisio = Date.MinValue
            End If

            If CheckBoxLiquidacio.Checked Then
                .FchLiquidacio = DateTimePickerLiquidacio.Value
            Else
                .FchLiquidacio = Date.MinValue
            End If

            If CheckBoxRehabilitacio.Checked Then
                .FchRehabilitacio = DateTimePickerRehabilitacio.Value
            Else
                .FchRehabilitacio = Date.MinValue
            End If
        End With

        Dim exs as New List(Of exception)
        If mInsolvencia.Update( exs) Then
            RaiseEvent AfterUpdate(mInsolvencia, New System.EventArgs)
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al registrar la insolvencia")
        End If

    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem la ionsolvencia num." & mInsolvencia.Id & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        mInsolvencia.Delete()
        RaiseEvent AfterUpdate(sender, e)
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub LoadGrid()
        Dim oTb As New DataTable
        With oTb.Columns
            .Add("ImpagatGuid", System.Type.GetType("System.guid"))
            .Add("CsbGuid", System.Type.GetType("System.guid"))
            .Add("YEA", System.Type.GetType("System.Int32"))
            .Add("CSA", System.Type.GetType("System.Int32"))
            .Add("CSB", System.Type.GetType("System.Int32"))
            .Add("EUR", System.Type.GetType("System.Decimal"))
            .Add("VTO", System.Type.GetType("System.DateTime"))
            .Add("TXT", System.Type.GetType("System.String"))
        End With

        Dim oRow As DataRow
        For Each oItm As Impagat In mInsolvencia.Impagats
            oRow = oTb.NewRow
            oRow("ImpagatGuid") = oItm.Csb.Guid
            oRow("CsbGuid") = oItm.Csb.Guid
            oRow("YEA") = oItm.Csb.Csa.yea
            oRow("CSA") = oItm.Csb.Csa.Id
            oRow("CSB") = oItm.Csb.Id
            oRow("EUR") = oItm.Csb.Amt.Eur
            oRow("VTO") = oItm.Csb.Vto
            oRow("TXT") = oItm.Csb.txt
            oTb.Rows.Add(oRow)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.ImpagatGuid)
                .Visible = False
            End With
            With .Columns(Cols.CsbGuid)
                .Visible = False
            End With
            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.Csa)
                .Visible = False
            End With
            With .Columns(Cols.Csb)
                .Visible = False
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "nominal"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Vto)
                .HeaderText = "presentacio"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Txt)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With

    End Sub

    Private Function CurrentImpagat() As DTOImpagat
        Dim retval As DTOImpagat = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.ImpagatGuid).Value
            retval = New DTOImpagat(oGuid)
        End If
        Return retval
    End Function

    Private Sub SetContextMenuImpagats()
        Dim oContextMenu As New ContextMenuStrip
        Dim oImpagat As DTOImpagat = CurrentImpagat()

        If oImpagat IsNot Nothing Then
            Dim oMenuItemZoom As New ToolStripMenuItem("zoom", My.Resources.prismatics, AddressOf Zoom)
            oContextMenu.Items.Add(oMenuItemZoom)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu

    End Sub

    Private Sub Zoom()
        Dim oFrm As New Frm_Impagat(CurrentImpagat())
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim iFirstRow As Integer
        Dim oGrid As DataGridView = DataGridView1
        If oGrid.CurrentRow IsNot Nothing Then
            i = oGrid.CurrentRow.Index
        End If
        Dim j As Integer = Cols.Txt
        iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        LoadGrid()
        If iFirstRow >= 0 Then
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(j)
            End If
        End If
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DateTimePickerPresentacio.ValueChanged, _
    DateTimePickerAdmisio.ValueChanged, _
    DateTimePickerLiquidacio.ValueChanged, _
     DateTimePickerRehabilitacio.ValueChanged

        If mAllowEvents Then ButtonOk.Enabled = True
    End Sub

    Private Sub Xl_AmtNominal_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_AmtNominal.AfterUpdate, _
       Xl_AmtGastos.AfterUpdate, _
       Xl_AmtPagatACompte.AfterUpdate, _
        Xl_AmtComisio.AfterUpdate

        If mAllowEvents Then ButtonOk.Enabled = True
        CalculaDeute()
    End Sub

    Private Sub CalculaDeute()
        Dim oTmp As DTOAmt = Xl_AmtNominal.Amt.Clone
        oTmp.Add(Xl_AmtGastos.Amt)
        oTmp.Substract(Xl_AmtPagatACompte.Amt)
        oTmp.Add(Xl_AmtComisio.Amt)
        Xl_AmtDeute.Amt = oTmp
    End Sub
End Class