

Public Class Frm_FraProveidor
    Private mFra As FacturaDeProveidor = Nothing
    Private mTbAlbs As DataTable = Nothing
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Yea
        Id
        Desadv
        Fch
        Lins
        Bas
        Tot
    End Enum

    Public Sub New(ByVal oFra As FacturaDeProveidor)
        MyBase.New()
        Me.InitializeComponent()
        LoadComboFromEnum(ComboBoxCfp, GetType(DTOCustomer.FormasDePagament))
        mFra = oFra
        refresca()
        mAllowEvents = True
    End Sub

    Private Sub refresca()
        With mFra
            Me.Text = "FACTURA DE " & .Proveidor.Nom
            Xl_Contact_Logo1.Contact = .Proveidor
            TextBoxNum.Text = .Num
            DateTimePickerFch.Value = .Fch
            Xl_AmtBas.Amt = .Bas
            Xl_AmtIva.Amt = .Iva
            Xl_AmtLiq.Amt = .Liq
            ComboBoxCfp.SelectedValue = .Cfp
            If .Vto = Date.MinValue Then
            Else
                DateTimePickerVto.Value = .Vto
            End If

            If .Proveidor.DefaultCtaCarrec IsNot Nothing Then
                Xl_CtaCarrec.Cta = New DTOPgcCta(.Proveidor.DefaultCtaCarrec.Guid)
            End If
            If .Proveidor.DefaultCtaAbonament IsNot Nothing Then
                Xl_CtaAbonament.Cta = New DTOPgcCta(.Proveidor.DefaultCtaAbonament.Guid)
            End If
            ComboBoxCfp.SelectedValue = .Cfp
            LoadAlbs()
            If .Cca IsNot Nothing Then
                Xl_DocFile1.Load(.Cca.DocFile)
            End If
            ButtonOk.Enabled = True
        End With
    End Sub



    Private Sub LoadAlbs()
        Dim oTb As New DataTable
        With oTb.Columns
            .Add("YEA", System.Type.GetType("System.Int32"))
            .Add("ALB", System.Type.GetType("System.Int32"))
            .Add("DESADV", System.Type.GetType("System.Int32"))
            .Add("FCH", System.Type.GetType("System.DateTime"))
            .Add("LIN", System.Type.GetType("System.Int32"))
            .Add("BAS", System.Type.GetType("System.Decimal"))
            .Add("TOT", System.Type.GetType("System.Decimal"))
        End With

        For Each oAlb As Alb In mFra.Albs
            Dim oRow As DataRow = oTb.NewRow
            oRow("YEA") = oAlb.Yea
            oRow("ALB") = oAlb.Id
            oRow("DESADV") = oAlb.DelegatNum
            oRow("FCH") = oAlb.Fch
            oRow("LIN") = oAlb.Itms.Count
            oRow("BAS") = oAlb.BaseImponible.Eur
            oRow("TOT") = oAlb.Total.Eur
            oTb.Rows.Add(oRow)
        Next

        Dim BlOldEvents As Boolean = mAllowEvents
        mAllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False

            .DataSource = oTb
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToResizeRows = False
            .AllowDrop = True
            .BackgroundColor = Color.White

            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .HeaderText = "albará"
                .Width = 45
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Desadv)
                .HeaderText = "Desadv"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Lins)
                .HeaderText = "linies"
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Bas)
                .HeaderText = "base"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Tot)
                .HeaderText = "total"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        mAllowEvents = BlOldEvents
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If Xl_AmtIva.Amt.Eur > 0 Then
            Dim DtIvaFchUltimaDeclaracio As Date = BLLCca.IvaFchUltimaDeclaracio(BLLApp.Emp)
            Dim DtFra As Date = DateTimePickerFch.Value
            If DateTimePickerFch.Value <= DtIvaFchUltimaDeclaracio Then
                MsgBox("la data de factura " & DtFra.ToShortDateString & vbCrLf & " es anterior a la ultima declaracio de Iva del " & DtIvaFchUltimaDeclaracio.ToShortDateString, MsgBoxStyle.Exclamation, "MAT.NET")
                Exit Sub
            End If
        End If

        With mFra
            .Num = TextBoxNum.Text
            .Fch = DateTimePickerFch.Value
            .Cfp = ComboBoxCfp.SelectedValue
            .Vto = DateTimePickerVto.Value
            .Bas = Xl_AmtBas.Amt
            .Iva = Xl_AmtIva.Amt
            .Liq = Xl_AmtLiq.Amt
            .CtaCarrec = New PgcCta(Xl_CtaCarrec.Cta.Guid)
            .CtaAbonament = New PgcCta(Xl_CtaAbonament.Cta.Guid)
            .Obs = TextBoxObs.Text
            .PendentDeAbonar = Xl_AmtPendentDeAbonar.Amt
            If Xl_DocFile1.IsDirty Then
                .Cca.DocFile = Xl_DocFile1.Value
            End If
            .Update()
        End With
        Me.Close()
        RaiseEvent AfterUpdate(mFra, EventArgs.Empty)
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxNum.TextChanged, _
     DateTimePickerFch.ValueChanged, _
      DateTimePickerVto.ValueChanged, _
       TextBoxObs.TextChanged, _
        Xl_AmtPendentDeAbonar.AfterUpdate

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Function CurrentAlb() As Alb
        Dim oAlb As Alb = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim LngId As Long = DataGridView1.CurrentRow.Cells(Cols.Id).Value
            Dim iYea As Integer = DataGridView1.CurrentRow.Cells(Cols.Yea).Value
            oAlb = MaxiSrvr.Alb.FromNum(BLL.BLLApp.Emp, iYea, LngId)
        End If
        Return oAlb
    End Function


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oAlb As Alb = CurrentAlb()

        If oAlb IsNot Nothing Then
            Dim oMenu_Alb As New Menu_Alb(oAlb)
            AddHandler oMenu_Alb.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Alb.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Bas
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.CurrentRow IsNot Nothing Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        Refresca()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oFrm As New Frm_AlbNew2(CurrentAlb)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub
End Class