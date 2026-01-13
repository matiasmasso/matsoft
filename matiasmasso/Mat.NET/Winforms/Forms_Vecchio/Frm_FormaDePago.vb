
Public Class Frm_FormaDePago

    Private _PaymentTerms As DTOPaymentTerms
    'Private mFormadePago As FormaDePago
    Private mTipus As DTOIban.Cods
    'Private mContact As Contact
    Private _Contact As DTOContact
    Private mAllowEvents As Boolean
    Private mTbDiasDePago As DataTable
    Private mDsNBancs As DataSet
    Private mDsVacaciones As DataSet
    Private mEmp As DTOEmp = Current.session.emp

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

    Public Sub New(ByVal oTipus As DTOIban.Cods, ByVal oContact As DTOContact, ByVal oPaymentTerms As DTOPaymentTerms)
        MyBase.New()
        Me.InitializeComponent()
        mTipus = oTipus
        _Contact = oContact
        _PaymentTerms = oPaymentTerms
    End Sub

    Private Async Sub Frm_FormaDePago_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _PaymentTerms Is Nothing Then _PaymentTerms = New DTOPaymentTerms
        If Await LoadNBancs(_PaymentTerms.NBanc, exs) Then
            With _PaymentTerms
                SetCodi(.Cod)
                NumericUpDownDias.Value = .Months * 30
                SetDias()
                Await LoadIbans()
                LoadCaption()
                Xl_Vacaciones1.Load(.Vacaciones)
            End With
            mAllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Function LoadFromForm(ByRef oValue As DTOPaymentTerms) As DTOPaymentTerms
        With oValue
            .Cod = ComboBoxCfp.SelectedValue
            .Months = NumericUpDownDias.Value / 30
            .PaymentDays = GetDiasFromGrid()
            .PaymentDayCod = IIf(RadioButtonDiasDelMes.Checked, DTOPaymentTerms.PaymentDayCods.MonthDay, DTOPaymentTerms.PaymentDayCods.WeekDay)

            '.Iban = Xl_IBAN1.IBAN
            If ComboBoxNBanc.SelectedIndex = -1 Then
                .NBanc = Nothing
            Else
                .NBanc = ComboBoxNBanc.SelectedItem
            End If
            .Vacaciones = Xl_Vacaciones1.Values
        End With
        Return oValue
    End Function

    Private Sub LoadCaption()
        Dim oPaymentTerms As New DTOPaymentTerms
        LoadFromForm(oPaymentTerms)
        LabelText.Text = FEB2.PaymentTerms.Text(oPaymentTerms, Current.Session.Lang)
    End Sub

    Public ReadOnly Property PaymentTerms() As DTOPaymentTerms
        Get
            LoadFromForm(_PaymentTerms)
            Return _PaymentTerms
        End Get
    End Property



    Private Function GetWeekDaysTb() As DataTable
        Dim oTb As New DataTable()
        oTb.Columns.Add("NUM", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("NOM", System.Type.GetType("System.String"))
        Dim oRow As DataRow
        Dim oLang As DTOLang = Current.Session.Lang

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

    Private Function GetDiasFromGrid() As List(Of Integer)
        Dim retval As New List(Of Integer)
        Dim oRow As DataRow
        For Each oRow In mTbDiasDePago.Rows
            If Not IsDBNull(oRow(0)) Then
                If oRow(0) > 0 Then
                    retval.Add(oRow(0))
                End If
            End If
        Next
        Return retval
    End Function

    Private Sub SetCodi(ByVal oCod As DTOPaymentTerms.CodsFormaDePago)
        Dim oDict = DTOPaymentTerms.Cods(Current.Session.Lang)
        If ComboBoxCfp.Items.Count = 0 Then
            With ComboBoxCfp
                .DataSource = oDict
                .ValueMember = "Value"
                .DisplayMember = "Nom"
            End With
        End If
        ComboBoxCfp.SelectedItem = oDict.FirstOrDefault(Function(x) x.Value = oCod)
    End Sub

    Private Sub SetDias()
        mTbDiasDePago = New DataTable
        mTbDiasDePago.Columns.Add("DIA", System.Type.GetType("System.Int32"))

        If _PaymentTerms.PaymentDays.Count > 0 Then

            Dim oRow As DataRow
            Dim iDia As Integer
            For Each iDia In _PaymentTerms.PaymentDays
                oRow = mTbDiasDePago.NewRow
                oRow(0) = iDia
                mTbDiasDePago.Rows.Add(oRow)
            Next
        End If

        Select Case _PaymentTerms.PaymentDayCod
            Case DTOPaymentTerms.PaymentDayCods.MonthDay
                SetDiasDelMes()
            Case DTOPaymentTerms.PaymentDayCods.WeekDay
                SetDiasDeLaSemana()
        End Select
    End Sub

    Private Sub SetDiasDelMes()
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
                .DataPropertyName = "dia"
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

    Private Sub SetDiasDeLaSemana()
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
            ' Refresca()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub NumericUpDownDias_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDownDias.ValueChanged
        If mAllowEvents Then
            'Refresca()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _PaymentTerms
            .Cod = ComboBoxCfp.SelectedValue
            .Months = NumericUpDownDias.Value / 30
            .PaymentDays = GetDiasFromGrid()
            .PaymentDayCod = IIf(RadioButtonDiasDelMes.Checked, DTOPaymentTerms.PaymentDayCods.MonthDay, DTOPaymentTerms.PaymentDayCods.WeekDay)

            '.Iban = Xl_IBAN1.IBAN
            If ComboBoxNBanc.SelectedIndex = -1 Then
                .NBanc = Nothing
            Else
                .NBanc = ComboBoxNBanc.SelectedItem
            End If
            .Vacaciones = Xl_Vacaciones1.Values
        End With
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_PaymentTerms))
        Me.Close()
    End Sub

    Private Async Function LoadNBancs(oBanc As DTOBanc, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim oBancs = Await FEB2.Bancs.AllActive(Current.Session.Emp, exs)
        If oBanc IsNot Nothing AndAlso Not oBancs.Any(Function(x) x.Equals(oBanc)) Then
            oBancs.Add(oBanc)
            oBancs = oBancs.OrderBy(Function(x) x.AbrOrNom)
        End If
        If exs.Count = 0 Then
            With ComboBoxNBanc
                .DisplayMember = "Abr"
                .DataSource = oBancs
                If oBanc Is Nothing Then
                    .SelectedIndex = -1
                Else
                    .SelectedItem = oBancs.Find(Function(x) x.Equals(oBanc))
                End If
            End With
            retval = True
        End If
        Return retval
    End Function

    Private Sub ComboBoxNBanc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxNBanc.SelectedIndexChanged
        If mAllowEvents Then
            'Refresca()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub DataGridViewDies_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewDies.RowValidated
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub RadioButton_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    RadioButtonWeekDays.CheckedChanged,
     RadioButtonDiasDelMes.CheckedChanged
        If mAllowEvents Then
            SetDias()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_Vacaciones1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Vacaciones1.AfterUpdate
        ButtonOk.Enabled = True
    End Sub

    Private Sub DataGridView_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles _
     DataGridViewDies.RowValidated
        'If mAllowEvents Then
        'ButtonOk.Enabled = True
        'End If
    End Sub


    Private Sub DataGridView_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles _
    DataGridViewDies.CellValueChanged
        'Select Case e.ColumnIndex
        'Case Cols.Min, Cols.Max, Cols.Chk, Cols.CheckPropi, Cols.CheckGrup, Cols.CheckAltres
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
        'End Select
    End Sub

    Private Sub DataGridView_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DataGridViewDies.CurrentCellDirtyStateChanged
        Dim oGrid As DataGridView = DirectCast(sender, DataGridView)
        'provoca CellValueChanged a cada clic sense sortir de la casella
        'Select Case DataGridViewDies.CurrentCell.ColumnIndex
        'Case Cols.Chk, Cols.Chk, Cols.CheckPropi, Cols.CheckGrup, Cols.CheckAltres
        oGrid.CommitEdit(DataGridViewDataErrorContexts.Commit)
        'End Select
    End Sub



    Private Sub Xl_Contact_Ibans1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Contact_Ibans1.RequestToAddNew
        Dim oIban = DTOIban.Factory(GlobalVariables.Emp, _Contact, mTipus)
        Dim oFrm As New Frm_Iban(oIban)
        AddHandler oFrm.AfterUpdate, AddressOf LoadIbans
        oFrm.Show()
    End Sub
    Private Async Sub LoadIbans(sender As Object, e As MatEventArgs)
        Await LoadIbans()
    End Sub

    Private Async Function LoadIbans() As Task
        Dim exs As New List(Of Exception)
        Dim oIbans = Await FEB2.Ibans.FromContact(exs, _Contact)
        If exs.Count = 0 Then
            Xl_Contact_Ibans1.Load(oIbans)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_Contact_Ibans1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Contact_Ibans1.RequestToRefresh
        Await LoadIbans()
    End Sub


End Class
