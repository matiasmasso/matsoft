Public Class Frm_FormaDePago

    Private mFormaDePago As FormaDePago
    Private mTipus As Contact.Tipus
    Private mContact As Contact
    Private _Contact As DTOContact
    Private mAllowEvents As Boolean
    Private mTbDiasDePago As DataTable
    Private mDsNBancs As DataSet
    Private mDsVacaciones As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Guid
        IcoPdf
        Ccc
        Pdf
        Ex
        IcoEx
    End Enum

    Private Enum ColsVac
        Mes1
        Dia1
        Mes2
        Dia2
        Mes3
        Dia3
    End Enum

    Public Sub New(ByVal oTipus As Contact.Tipus, ByVal oContact As Object, ByVal oFormadePago As FormaDePago)
        MyBase.New()
        Me.InitializeComponent()
        mTipus = oTipus
        mContact = oContact
        _Contact = BLL.BLLContact.Find(oContact.guid)
        mFormaDePago = oFormadePago
        LoadNBancs()
        With mFormaDePago
            If .NBanc IsNot Nothing Then
                ComboBoxNBanc.SelectedValue = .NBanc.Id
            Else
                ComboBoxNBanc.SelectedIndex = -1
            End If
            SetCodi(.Cod)
            NumericUpDownDias.Value = .Mesos * 30
            SetDias(.Dias)
            LoadIbans()
            LoadVacaciones(.Vacaciones)
        End With
        Refresca()
        mAllowEvents = True
    End Sub

    Public ReadOnly Property FormaDePago() As FormaDePago
        Get
            Return mFormaDePago
        End Get
    End Property

    Private Sub Refresca()
        Dim oFpg As New FormaDePago
        With oFpg
            .Cod = ComboBoxCfp.SelectedValue
            .Mesos = NumericUpDownDias.Value / 30
            .Dias = GetDiasFromGrid()
            '.Iban = Xl_IBAN1.IBAN
            .NBanc = MaxiSrvr.Banc.FromNum(BLL.BLLApp.Emp, ComboBoxNBanc.SelectedValue)
        End With
        LabelText.Text = oFpg.Text(BLL.BLLApp.Lang)
    End Sub

    Private Sub LoadVacaciones(ByVal oVacaciones As Vacaciones)
        mDsVacaciones = CreateVacacionesDataSource(oVacaciones)
        Dim oTb As DataTable = mDsVacaciones.Tables(0)

        With DataGridViewVacances
            With .RowTemplate
                .Height = DataGridViewVacances.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            Dim oMesColumn As New DataGridViewComboBoxColumn()
            With oMesColumn
                .HeaderText = "desde mes"
                .DataPropertyName = oTb.Columns(ColsVac.Mes1).ColumnName
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .MaxDropDownItems = 3
                .DataSource = GetMesosTb()
                .ValueMember = "NUM"
                .DisplayMember = "NOM"
            End With
            .Columns.Remove(.Columns(ColsVac.Mes1))
            .Columns.Insert(ColsVac.Mes1, oMesColumn)

            Dim oDiaColumn As New DataGridViewComboBoxColumn()
            With oDiaColumn
                .HeaderText = "dia"
                .Width = 40
                .DataPropertyName = oTb.Columns(ColsVac.Dia1).ColumnName
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .MaxDropDownItems = 3
                .DataSource = GetDiesDelMesTb()
                .ValueMember = "NUM"
                .DisplayMember = "NUM"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .Columns.Remove(.Columns(ColsVac.Dia1))
            .Columns.Insert(ColsVac.Dia1, oDiaColumn)

            oMesColumn = New DataGridViewComboBoxColumn()
            With oMesColumn
                .HeaderText = "fins mes"
                .DataPropertyName = oTb.Columns(ColsVac.Mes2).ColumnName
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .MaxDropDownItems = 12
                .DataSource = GetMesosTb()
                .ValueMember = "NUM"
                .DisplayMember = "NOM"
            End With
            .Columns.Remove(.Columns(ColsVac.Mes2))
            .Columns.Insert(ColsVac.Mes2, oMesColumn)

            oDiaColumn = New DataGridViewComboBoxColumn()
            With oDiaColumn
                .HeaderText = "dia"
                .Width = 40
                .DataPropertyName = oTb.Columns(ColsVac.Dia2).ColumnName
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .MaxDropDownItems = 12
                .DataSource = GetDiesDelMesTb()
                .ValueMember = "NUM"
                .DisplayMember = "NUM"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .Columns.Remove(.Columns(ColsVac.Dia2))
            .Columns.Insert(ColsVac.Dia2, oDiaColumn)

            oMesColumn = New DataGridViewComboBoxColumn()
            With oMesColumn
                .HeaderText = "aplaçar a"
                .DataPropertyName = oTb.Columns(ColsVac.Mes3).ColumnName
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .MaxDropDownItems = 3
                .DataSource = GetMesosTb()
                .ValueMember = "NUM"
                .DisplayMember = "NOM"
            End With
            .Columns.Remove(.Columns(ColsVac.Mes3))
            .Columns.Insert(ColsVac.Mes3, oMesColumn)

            oDiaColumn = New DataGridViewComboBoxColumn()
            With oDiaColumn
                .HeaderText = "dia"
                .Width = 40
                .DataPropertyName = oTb.Columns(ColsVac.Dia3).ColumnName
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .MaxDropDownItems = 3
                .DataSource = GetDiesDelMesTb()
                .ValueMember = "NUM"
                .DisplayMember = "NUM"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .Columns.Remove(.Columns(ColsVac.Dia3))
            .Columns.Insert(ColsVac.Dia3, oDiaColumn)

        End With

    End Sub

    Private Function GetMesosTb() As DataTable
        Dim oTb As New DataTable()
        oTb.Columns.Add("NUM", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("NOM", System.Type.GetType("System.String"))
        Dim oRow As DataRow
        Dim oLang As DTOLang = BLL.BLLSession.Current.Lang
        oRow = oTb.NewRow
        oRow(0) = 0
        oRow(1) = ""
        oTb.Rows.Add(oRow)
        For i As Integer = 1 To 12
            oRow = oTb.NewRow
            oRow(0) = i
            oRow(1) = oLang.MesAbr(i)
            oTb.Rows.Add(oRow)
        Next
        Return oTb
    End Function

    Private Function GetWeekDaysTb() As DataTable
        Dim oTb As New DataTable()
        oTb.Columns.Add("NUM", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("NOM", System.Type.GetType("System.String"))
        Dim oRow As DataRow
        Dim oLang As DTOLang = BLL.BLLSession.Current.Lang

        Dim sOrd(5) As String
        sOrd(0) = "primer"
        sOrd(1) = "segon"
        sOrd(2) = "tercer"
        sOrd(3) = "quart"
        sOrd(4) = "darrer"
        Dim sOrdNum As String = ""
        Dim i As Integer
        Dim j As Integer
        For i = 0 To 4
            sOrdNum = sOrd(i)
            For j = 1 To 7
                oRow = oTb.NewRow
                oRow(0) = (i * 7) + j
                oRow(1) = sOrdNum & " " & oLang.WeekDay(j)
                oTb.Rows.Add(oRow)
            Next
        Next
        Return oTb
    End Function

    Private Function GetDiesDelMesTb() As DataTable
        Dim oTb As New DataTable()
        oTb.Columns.Add("NUM", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("NOM", System.Type.GetType("System.String"))
        Dim oRow As DataRow
        oRow = oTb.NewRow
        oRow(0) = 0
        oRow(1) = " "
        oTb.Rows.Add(oRow)
        For i As Integer = 1 To 30
            oRow = oTb.NewRow
            oRow(0) = i
            oRow(1) = "dia " & Format(i, "00")
            oTb.Rows.Add(oRow)
        Next
        oRow = oTb.NewRow
        oRow(0) = 31
        oRow(1) = "últim dia del mes"
        oTb.Rows.Add(oRow)
        Return oTb
    End Function

    Private Function GetDiasFromGrid() As DiasDePago
        Dim oDias As New DiasDePago
        With oDias
            .Codi = IIf(RadioButtonDiasDelMes.Checked, DiasDePago.Codis.DiasDelMes, DiasDePago.Codis.DiasDeLaSemana)
            Dim oRow As DataRow
            For Each oRow In mTbDiasDePago.Rows
                If Not IsDBNull(oRow(0)) Then
                    If oRow(0) > 0 Then
                        .Dias.Add(oRow(0))
                    End If
                End If
            Next
        End With
        Return oDias
    End Function

    Private Sub SetCodi(ByVal oCod As DTOCustomer.FormasDePagament)
        If ComboBoxCfp.Items.Count = 0 Then
            With ComboBoxCfp
                .DataSource = mFormaDePago.DsCods(BLL.BLLApp.Lang).Tables(0)
                .ValueMember = "ID"
                .DisplayMember = "NOM"
            End With
        End If
        ComboBoxCfp.SelectedValue = oCod
    End Sub

    Private Sub SetDias(ByVal oDias As DiasDePago)
        mTbDiasDePago = New DataTable
        mTbDiasDePago.Columns.Add("DIA", System.Type.GetType("System.Int32"))

        If oDias.Dias.Count > 0 Then

            Dim oRow As DataRow
            Dim iDia As Integer
            For Each iDia In oDias.Dias
                oRow = mTbDiasDePago.NewRow
                oRow(0) = iDia
                mTbDiasDePago.Rows.Add(oRow)
            Next
        End If

        Select Case oDias.Codi
            Case DiasDePago.Codis.DiasDelMes
                SetDiasDelMes(oDias)
            Case DiasDePago.Codis.DiasDeLaSemana
                SetDiasDeLaSemana(oDias)
        End Select
    End Sub

    Private Sub SetDiasDelMes(ByVal oDias As DiasDePago)
        With DataGridViewDies
            With .RowTemplate
                .Height = DataGridViewDies.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = mTbDiasDePago
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            Dim oDiaColumn As New DataGridViewComboBoxColumn()
            With oDiaColumn
                .HeaderText = "dia"
                .Width = 30
                .DataPropertyName = mTbDiasDePago.Columns(0).ColumnName
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .MaxDropDownItems = 3
                .DataSource = GetDiesDelMesTb()
                .ValueMember = "NUM"
                .DisplayMember = "NOM"
            End With
            .Columns.Remove(.Columns(0))
            .Columns.Insert(0, oDiaColumn)
        End With
    End Sub

    Private Sub SetDiasDeLaSemana(ByVal oDias As DiasDePago)
        With DataGridViewDies
            With .RowTemplate
                .Height = DataGridViewDies.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oDias.Dias
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            Dim oWeekdayColumn As New DataGridViewComboBoxColumn()
            With oWeekdayColumn
                .HeaderText = "dia"
                .Width = 60
                .DataPropertyName = "Dias"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .MaxDropDownItems = 3
                .DataSource = GetWeekDaysTb()
                .ValueMember = "NUM"
                .DisplayMember = "NOM"
            End With
            '.Columns.Remove(.Columns(0))

            .Columns.Insert(0, oWeekdayColumn)
        End With
    End Sub

    Private Sub ComboBoxCfp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxCfp.SelectedIndexChanged
        If mAllowEvents Then
            Refresca()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub NumericUpDownDias_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDownDias.ValueChanged
        If mAllowEvents Then
            Refresca()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mFormaDePago
            .Cod = ComboBoxCfp.SelectedValue
            .Mesos = NumericUpDownDias.Value / 30
            .Dias = GetDiasFromGrid()
            '.Iban = Xl_IBAN1.IBAN
            If ComboBoxNBanc.SelectedIndex = -1 Then
                .NBanc = Nothing
            Else
                .NBanc = MaxiSrvr.Banc.FromNum(BLL.BLLApp.Emp, mDsNBancs.Tables(0).Rows(ComboBoxNBanc.SelectedIndex)(0))
            End If
            Dim oVacaciones As New Vacaciones
            Dim oVacacion As Vacacion
            Dim oTb As DataTable = mDsVacaciones.Tables(0)
            Dim oRow As DataRow
            Dim BlAplaza30dias As Boolean
            For Each oRow In oTb.Rows
                BlAplaza30dias = (oRow(ColsVac.Mes3) <= 0 Or oRow(ColsVac.Dia3) <= 0)
                oVacacion = New Vacacion(oRow(ColsVac.Dia1), oRow(ColsVac.Mes1), oRow(ColsVac.Dia2), oRow(ColsVac.Mes2), BlAplaza30dias, oRow(ColsVac.Dia3), oRow(ColsVac.Mes3))
                oVacaciones.Add(oVacacion)
            Next
            .Vacaciones = oVacaciones
            '.RaiseAfterUpdate()
        End With
        RaiseEvent AfterUpdate(mFormaDePago, e)
        Me.Close()
    End Sub

    Private Sub LoadNBancs()
        Dim SQL As String = "SELECT CLI,ABR FROM CLIBNC WHERE " _
        & "EMP=" & App.Current.Emp.Id & " and " _
        & "ACTIU=1 " _
        & "ORDER BY ABR"
        mDsNBancs = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        With ComboBoxNBanc
            .DisplayMember = "ABR"
            .ValueMember = "CLI"
            .DataSource = mDsNBancs.Tables(0)
        End With
    End Sub

    Private Sub ComboBoxNBanc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxNBanc.SelectedIndexChanged
        If mAllowEvents Then
            Refresca()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Function CreateVacacionesDataSource(ByVal oVacaciones As Vacaciones) As DataSet
        Dim oTb As New DataTable
        With oTb.Columns
            .Add("MES1", System.Type.GetType("System.Int32"))
            .Add("DIA1", System.Type.GetType("System.Int32"))
            .Add("MES2", System.Type.GetType("System.Int32"))
            .Add("DIA2", System.Type.GetType("System.Int32"))
            .Add("MES3", System.Type.GetType("System.Int32"))
            .Add("DIA3", System.Type.GetType("System.Int32"))
        End With
        Dim oRow As DataRow
        For i As Integer = 0 To oVacaciones.Count - 1
            oRow = oTb.NewRow
            oRow(ColsVac.Mes1) = oVacaciones(i).DesdeMes
            oRow(ColsVac.Dia1) = oVacaciones(i).DesdeDia
            oRow(ColsVac.Mes2) = oVacaciones(i).HastaMes
            oRow(ColsVac.Dia2) = oVacaciones(i).HastaDia
            oRow(ColsVac.Mes3) = oVacaciones(i).AplazaMes
            oRow(ColsVac.Dia3) = oVacaciones(i).AplazaDia
            oTb.Rows.Add(oRow)
        Next

        Dim oDs As New DataSet
        oDs.Tables.Add(oTb)
        Return oDs
    End Function

    Private Sub DataGridViewDies_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewDies.RowValidated
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub RadioButton_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    RadioButtonWeekDays.CheckedChanged, _
     RadioButtonDiasDelMes.CheckedChanged
        If mAllowEvents Then
            SetDias(mFormaDePago.Dias)
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub DataGridView_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles _
    DataGridViewVacances.RowValidated, DataGridViewDies.RowValidated
        'If mAllowEvents Then
        'ButtonOk.Enabled = True
        'End If
    End Sub


    Private Sub DataGridView_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles _
    DataGridViewDies.CellValueChanged, DataGridViewVacances.CellValueChanged
        'Select Case e.ColumnIndex
        'Case Cols.Min, Cols.Max, Cols.Chk, Cols.CheckPropi, Cols.CheckGrup, Cols.CheckAltres
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
        'End Select
    End Sub

    Private Sub DataGridView_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DataGridViewDies.CurrentCellDirtyStateChanged, DataGridViewVacances.CurrentCellDirtyStateChanged
        Dim oGrid As DataGridView = CType(sender, DataGridView)
        'provoca CellValueChanged a cada clic sense sortir de la casella
        'Select Case DataGridViewDies.CurrentCell.ColumnIndex
        'Case Cols.Chk, Cols.Chk, Cols.CheckPropi, Cols.CheckGrup, Cols.CheckAltres
        oGrid.CommitEdit(DataGridViewDataErrorContexts.Commit)
        'End Select
    End Sub



    Private Sub Xl_Contact_Ibans1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Contact_Ibans1.RequestToAddNew
        Dim oIban As DTOIban = BLL.BLLIban.NewIban(_Contact)
        Dim oFrm As New Frm_Iban(oIban)
        AddHandler oFrm.AfterUpdate, AddressOf LoadIbans
        oFrm.Show()
    End Sub

    Private Sub LoadIbans()
        Dim oIbans As List(Of DTOIban) = BLL.BLLIbans.FromContact(_Contact)
        Xl_Contact_Ibans1.Load(oIbans)
    End Sub

    Private Sub Xl_Contact_Ibans1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Contact_Ibans1.RequestToRefresh
        LoadIbans()
    End Sub
End Class
