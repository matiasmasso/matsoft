

Public Class Frm_Trp_Tarifa
    Private mTrpZon As TrpZon
    Private mDsZonSi As DataSet
    Private mDsZonNo As DataSet
    Private mDsCosts As DataSet
    Private DirtyHeader As Boolean
    Private DirtyZonas As Boolean
    Private DirtyCosts As Boolean
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oZon As TrpZon)
        MyBase.New()
        Me.InitializeComponent()

        mTrpZon = oZon

        Dim sTrpNom As String = mTrpZon.Transportista.Abr
        If sTrpNom = "" Then sTrpNom = BLLContact.NomComercialOrDefault(mTrpZon.Transportista)
        TextBoxTrpNom.Text = sTrpNom
        TextBoxTrpZon.Text = mTrpZon.Nom
        If mTrpZon.Cubicatje > 0 Then
            CheckBoxHeredaCubicatje.Checked = False
            TextBoxCubicatje.ReadOnly = False
        Else
            CheckBoxHeredaCubicatje.Checked = True
            TextBoxCubicatje.ReadOnly = True
        End If
        TextBoxCubicatje.Text = mTrpZon.CubitatjeSelfOrInherited
        CheckBoxActivat.Checked = mTrpZon.Activat

        PictureBoxLogo.Image = mTrpZon.Transportista.Logo
        LoadZonas()
        LoadCostos()
        If mTrpZon.Id > 0 Then ButtonDel.Enabled = True
        mAllowEvents = True

    End Sub



    Private Sub LoadZonas()
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Zona.Guid, Country.Iso+' '+Zona.Nom AS Nom ")
        sb.AppendLine("FROM TRPZON ")
        sb.AppendLine("INNER JOIN Zona ON TRPZON.Zona = Zona.Guid ")
        sb.AppendLine("INNER JOIN Country ON Zona.Country=Country.Guid ")
        sb.AppendLine("WHERE TRPZON.TrpGuid ='" & mTrpZon.Transportista.Guid.ToString & "' ")
        sb.AppendLine("AND TRPZON.TrpZon =" & mTrpZon.Id & " ")
        sb.AppendLine("ORDER BY Country.Iso+' '+Zona.Nom ")

        Dim SQL As String = sb.ToString
        mDsZonSi = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        With ListBoxZonasSi
            .DataSource = mDsZonSi.Tables(0)
            .DisplayMember = "Nom"
            .ValueMember = "Guid"
        End With

        sb = New Text.StringBuilder
        sb.AppendLine("SELECT Zona.Guid, Country.Iso+' '+Zona.Nom AS Nom ")
        sb.AppendLine("FROM Zona ")
        sb.AppendLine("INNER JOIN Country ON Zona.Country=Country.Guid ")
        sb.AppendLine("WHERE Zona.Guid NOT IN ( ")
        sb.AppendLine("     SELECT TrpZon.Zona FROM TrpZon WHERE TRPZON.TrpGuid ='" & mTrpZon.Transportista.Guid.ToString & "') ")
        sb.AppendLine("     ') ")
        sb.AppendLine("ORDER BY Country.Iso+' '+Zona.Nom ")
        SQL = sb.ToString
        mDsZonNo = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        With ListBoxZonasNo
            .DataSource = mDsZonNo.Tables(0)
            .DisplayMember = "Nom"
            .ValueMember = "Guid"
        End With
    End Sub

    Private Sub ButtonAddZona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAddZona.Click
        SwitchZona(mDsZonNo, mDsZonSi, ListBoxZonasNo.SelectedIndex)
    End Sub

    Private Sub ButtonRemoveZona_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRemoveZona.Click
        SwitchZona(mDsZonSi, mDsZonNo, ListBoxZonasSi.SelectedIndex)
    End Sub

    Private Sub TextBoxCubicatje_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxCubicatje.KeyPress
        Select Case e.KeyChar
            Case "."
                e.KeyChar = ","
            Case "0" To "9"
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub TextBoxTrpNom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxTrpZon.TextChanged, _
         TextBoxCubicatje.TextChanged, _
          CheckBoxActivat.CheckedChanged

        If mAllowEvents Then
            DirtyHeader = True
            SetDirty()
        End If
    End Sub

    Private Sub SetDirty()
        ButtonOk.Enabled = True
    End Sub

    Private Sub SwitchZona(ByVal oDsFrom As DataSet, ByVal oDsDest As DataSet, ByVal Idx As Integer)

        Dim oTbFrom As DataTable = oDsFrom.Tables(0)
        Dim oTbDest As DataTable = oDsDest.Tables(0)

        Dim oOldRow As DataRow = oTbFrom.Rows(Idx)
        Dim oNewRow As DataRow = oTbDest.NewRow

        oNewRow(0) = oOldRow(0)
        oNewRow(1) = oOldRow(1)
        oTbDest.Rows.Add(oNewRow)
        oTbFrom.Rows.RemoveAt(Idx)

        ButtonAddZona.Enabled = mDsZonNo.Tables(0).Rows.Count > 0
        ButtonRemoveZona.Enabled = mDsZonSi.Tables(0).Rows.Count > 0
        DirtyZonas = True
        SetDirty()
    End Sub

    Private Enum ColCosts
        HastaKg
        Eur
        FixEur
        MinEur
        Suplidos
        Cod
    End Enum


    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim BlUpdateHeader As Boolean = DirtyHeader
        If mTrpZon.Id = 0 And Not BlUpdateHeader Then
            BlUpdateHeader = (DirtyCosts Or DirtyZonas)
        End If

        With mTrpZon
            If BlUpdateHeader Then
                .Nom = TextBoxTrpZon.Text

                If CheckBoxHeredaCubicatje.Checked Then
                    .Cubicatje = 0
                Else
                    .Cubicatje = TextBoxCubicatje.Text
                End If
            End If

            .Activat = CheckBoxActivat.Checked

            If DirtyCosts Then .Costs = GetCostsFromGrid()
            If DirtyZonas Then .Zonas = GetZonasFromListBox()
            Dim oEx As Exception = .Update()
            If Not oEx Is Nothing Then
                MsgBox(oEx.Message)
            Else
                RaiseEvent AfterUpdate(sender, e)
                Me.Close()
            End If
        End With
    End Sub

    Private Function GetZonasFromListBox() As List(Of DTOZona)
        Dim retval As New List(Of DTOZona)

        Dim oTb As DataTable = mDsZonSi.Tables(0)
        For Each oRow As DataRow In oTb.Rows
            Dim oGuid As Guid = oRow(0)
            Dim oZona As New DTOZona(oGuid)
            retval.Add(oZona)
        Next
        Return retval
    End Function

    Private Function GetCostsFromGrid() As TrpCosts
        Dim oCosts As New TrpCosts
        Dim oCost As TrpCost
        Dim oRow As DataRow

        For Each oRow In mDsCosts.Tables(0).Rows
            oCost = New TrpCost
            With oCost
                .HastaKg = oRow(ColCosts.HastaKg)
                .Eur = oRow(ColCosts.Eur)
                If IsNumeric(oRow(ColCosts.FixEur)) Then
                    .FixEur = oRow(ColCosts.FixEur)
                Else
                    .FixEur = 0
                End If
                '.FixEur = oRow(ColCosts.FixEur)
                .MinEur = IIf(IsDBNull(oRow(ColCosts.MinEur)), 0, oRow(ColCosts.MinEur))
                If IsDBNull(oRow(ColCosts.Suplidos)) Then
                    .Suplidos = 0
                Else
                    .Suplidos = oRow(ColCosts.Suplidos)
                End If
                .Cod = CInt(oRow(ColCosts.Cod))
            End With
            oCosts.Add(oCost)
        Next
        Return oCosts
    End Function

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem tota la zona " & mTrpZon.Nom & vbCrLf & " de " & mTrpZon.Transportista.Nom & "?", MsgBoxStyle.OKCancel, "MAT.NET")
        Select Case rc
            Case MsgBoxResult.OK
                mTrpZon.Delete()
                RaiseEvent AfterUpdate(sender, e)
                Me.Close()
            Case Else
                MsgBox("Operació cancelada", MsgBoxStyle.Information, "MAT.NET")
        End Select
    End Sub

    Private Sub ButtonCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCalc.Click
        Dim sM3 As String = TextBoxCalcM3.Text
        Dim sKg As String = TextBoxCalcKg.Text
        Dim SngM3 As Decimal
        Dim SngKg As Decimal
        If IsNumeric(sM3) Then
            SngM3 = CSng(sM3)
            SngKg = mTrpZon.KgCubicats(SngM3)
            TextBoxCalcKg.Text = SngKg
        Else
            SngKg = CSng(TextBoxCalcKg.Text)
        End If

        Dim oCost As DTOAmt = mTrpZon.Cost(SngM3, SngKg)
        If oCost Is Nothing Then
            MsgBox("volum/pes no tarifat", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            LabelCalcCost.Text = "cost: " & oCost.CurFormatted
        End If
        ButtonCalc.Enabled = False
    End Sub

    Private Sub TextBoxCalcM3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
        TextBoxCalcM3.KeyPress, _
         TextBoxCalcKg.KeyPress
        If e.KeyChar = "." Then e.KeyChar = ","
    End Sub


    Private Sub TextBoxCalc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxCalcKg.TextChanged, TextBoxCalcM3.TextChanged
        Dim sM3 As String = TextBoxCalcM3.Text
        Dim sKg As String = TextBoxCalcKg.Text
        Dim BlEnable As Boolean = False
        If IsNumeric(sM3) Then BlEnable = True
        If IsNumeric(sKg) Then BlEnable = True
        ButtonCalc.Enabled = BlEnable
        LabelCalcCost.Text = "cost:"
    End Sub

    Private Sub LoadCostos()
        Dim SQL As String = "SELECT HASTAKG,EUR,FIXEUR,MINEUR,SUPLIDOS,CAST (COD AS CHAR) AS COD " _
        & "FROM TRPCOST WHERE " _
        & "TrpGuid='" & mTrpZon.Transportista.Guid.ToString & "' AND " _
        & "TRPZON=" & mTrpZon.Id & " " _
        & "ORDER BY HASTAKG"
        mDsCosts = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsCosts.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = True
            .RowHeadersWidth = 25
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False

            With .Columns(ColCosts.HastaKg)
                .HeaderText = "Kg"
                .Width = 60
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 Kg"
            End With
            With .Columns(ColCosts.Eur)
                .HeaderText = "Cost"
                .Width = 60
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.0000 €;-#,###0.0000 €;#"
            End With
            With .Columns(ColCosts.FixEur)
                .HeaderText = "Fixe"
                .Width = 60
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(ColCosts.MinEur)
                .HeaderText = "minim"
                .Width = 60
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(ColCosts.Suplidos)
                .HeaderText = "suplidos"
                .Width = 60
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(ColCosts.Cod)
                .Visible = False
            End With

            .Columns.Remove(.Columns(ColCosts.Cod))

            Dim oCodiCostsColumn As New DataGridViewComboBoxColumn()
            With oCodiCostsColumn
                .HeaderText = "codi costos"
                .DataPropertyName = oTb.Columns(ColCosts.Cod).ColumnName
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .MaxDropDownItems = 2
                .DataSource = GetCodiCostos()
                .ValueMember = "NUM"
                .DisplayMember = "NOM"
            End With
            .Columns.Add(oCodiCostsColumn)
        End With
    End Sub

    Private Function GetCodiCostos() As DataTable
        Dim oTb As DataTable = New DataTable()
        'oTb.Columns.Add("NUM", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("NUM", System.Type.GetType("System.String"))
        oTb.Columns.Add("NOM", System.Type.GetType("System.String"))
        Dim oRow As DataRow = oTb.NewRow
        oRow(0) = "0"
        oRow(1) = "cost fins a Kgs"
        oTb.Rows.Add(oRow)
        oRow = oTb.NewRow
        oRow(0) = "1"
        oRow(1) = "cost per Kg"
        oTb.Rows.Add(oRow)
        Return oTb
    End Function

    Private Sub DataGridView1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        Select Case e.ColumnIndex
            Case ColCosts.Cod
                'Dim i As Integer = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value

                'If i > 0 Then
                'orow(ColCosts.HastaKg).Value = mDsCosts.Tables(0).(ColCosts.Cod)
                'DataGridView1.Rows(e.RowIndex).Cells(ColCosts.HastaKg).Value = mDsCosts.Tables(0).(ColCosts.Cod)
                'Else
                'orow(ColCosts.HastaKg).Value = 0
                'DataGridView1.Rows(e.RowIndex).Cells(ColCosts.HastaKg).Value = 0
                'End If

        End Select
    End Sub





    Private Sub DataGridView1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        Select Case e.ColumnIndex
            Case ColCosts.HastaKg
                If Not IsNumeric(e.FormattedValue) Then
                    'e.value = 999999
                End If
            Case ColCosts.HastaKg, ColCosts.Eur, ColCosts.FixEur, ColCosts.MinEur, ColCosts.Suplidos
                If Not IsNumeric(e.FormattedValue) Then
                    'e.Value = 0
                End If
            Case ColCosts.Cod
                'Dim oComboCell As DataGridViewComboBoxCell = DataGridView1.Rows(e.RowIndex).Cells(ColCosts.Cod)
                'Dim s As String = e.FormattedValue
                'Dim oComboCol As DataGridViewComboBoxColumn = DataGridView1.Columns(ColCosts.Cod)
                'For i As Integer = 1 To oComboCol.Items.Count - 1
                'If oComboCol.Items(i).ToString = s Then Stop
                'Dim oRowView As DataRowView = oComboCol.Items(i)
                'Dim orowview.Row.ItemArray(0)
                'Next
                'If e.FormattedValue < 0 Then
                'If oRow > 0 Then
                'orow(ColCosts.HastaKg).Value = mDsCosts.Tables(0).(ColCosts.Cod)
                'Else
                'orow(ColCosts.HastaKg).Value = 0
                'End If
                'End If
        End Select
        DirtyCosts = True
        SetDirty()
    End Sub

    Private Sub DataGridView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'ho crida DataGridView1_EditingControlShowing
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case ColCosts.Eur, ColCosts.FixEur, ColCosts.MinEur, ColCosts.Suplidos
                If e.KeyChar = "." Then
                    e.KeyChar = ","
                End If
        End Select
    End Sub

    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing
        'fa que funcioni KeyPress per DataGridViews
        If TypeOf e.Control Is TextBox Then
            Dim oControl As TextBox = CType(e.Control, TextBox)
            AddHandler oControl.KeyPress, AddressOf DataGridView1_KeyPress
        End If
    End Sub

    Private Sub CheckBoxHeredaCubicatje_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxHeredaCubicatje.CheckedChanged
        If mAllowEvents Then
            If CheckBoxHeredaCubicatje.Checked Then
                TextBoxCubicatje.Text = mTrpZon.Transportista.Cubicaje
                TextBoxCubicatje.ReadOnly = True
            Else
                TextBoxCubicatje.ReadOnly = False
            End If
            DirtyHeader = True
        End If
    End Sub
End Class