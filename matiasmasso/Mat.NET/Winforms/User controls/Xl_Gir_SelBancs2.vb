Public Class Xl_Gir_SelBancs2
    Private _ControlItems As ControlItems
    Private _Csbs As List(Of DTOCsb)
    Private _Csas As List(Of DTOCsa)
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Checked
        Banc
        Tot
        Min
        Max
        Clasificacio
        Despeses
        Tae
    End Enum

    Public Shadows Sub Load(oCsbs As List(Of DTOCsb), oBancs As List(Of DTOBanc), DtFch As Date)
        _Csbs = oCsbs
        _Csas = New List(Of DTOCsa)
        For Each oBanc In oBancs
            Dim oCsa = DTOCsa.Factory(GlobalVariables.Emp, oBanc, DTOCsa.FileFormats.SepaCore, True)
            oCsa.Fch = DtFch
            _Csas.Add(oCsa)
        Next
        refresca()
        Distribueix()
    End Sub

    Private Function Clon(oCsbs) As List(Of DTOCsb)
        Dim retval As New List(Of DTOCsb)
        retval.AddRange(oCsbs)
        Return retval
    End Function

    Public Sub refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems

        For Each oCsa In _Csas
            Dim oItem As New ControlItem(oCsa)
            _ControlItems.Add(oItem)
        Next

        LoadGrid()
        _AllowEvents = True
    End Sub


    Public Function Csas() As List(Of DTOCsa)
        Dim retval As New List(Of DTOCsa)
        For Each oItem As ControlItem In _ControlItems
            Dim oCsa As DTOCsa = oItem.Source
            oCsa.NominalMaxim = DTOAmt.Factory(oItem.Max)
            oCsa.NominalMinim = DTOAmt.Factory(oItem.Min)
            If oItem.Checked And oCsa.Items.Count > 0 Then
                retval.Add(oItem.Source)
            End If
        Next
        Return retval
    End Function

    Public Function NonEmptyCsas() As List(Of DTOCsa)
        Dim retval As New List(Of DTOCsa)
        For Each oItem As ControlItem In _ControlItems
            If oItem.Checked Then
                Dim oCsa As DTOCsa = oItem.Source
                If oCsa.Items.Count > 0 Then
                    oCsa.NominalMaxim = DTOAmt.Factory(oItem.Max)
                    oCsa.NominalMinim = DTOAmt.Factory(oItem.Min)
                    oCsa.Enabled = True
                    retval.Add(oItem.Source)
                End If
            End If
        Next
        Return retval
    End Function

    Public Function Despeses() As DTOAmt
        Dim Eur As Decimal
        For Each oItem As ControlItem In _ControlItems
            If oItem.Checked Then
                Eur += oItem.Despeses
            End If
        Next
        Dim retval As DTOAmt = DTOAmt.Factory(Eur)
        Return retval
    End Function

    Private Function CheckedItems() As List(Of ControlItem)
        Dim retval As New List(Of ControlItem)
        For Each oItem As ControlItem In _ControlItems
            If oItem.Checked Then
                retval.Add(oItem)
            End If
        Next
        Return retval
    End Function

    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                '.Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False
            '.ReadOnly = True

            .Columns.Add(New DataGridViewCheckBoxColumn)
            With DirectCast(.Columns(Cols.Checked), DataGridViewCheckBoxColumn)
                .DataPropertyName = "Checked"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 20
                '.DefaultCellStyle.NullValue = Nothing
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Banc)
                .HeaderText = "Entitat"
                .DataPropertyName = "Banc"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Tot)
                .HeaderText = "Girat"
                .DataPropertyName = "Tot"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Min)
                .HeaderText = "Minim"
                .DataPropertyName = "Min"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Max)
                .HeaderText = "Maxim"
                .DataPropertyName = "Max"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Clasificacio)
                .HeaderText = "Classificació"
                .DataPropertyName = "Classificacio"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Despeses)
                .HeaderText = "Despeses"
                .DataPropertyName = "Despeses"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Tae)
                .HeaderText = "Tae"
                .DataPropertyName = "Tae"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 %;-#,###0.00 %;#"
            End With


        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub DataGridView1_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        Select Case e.ColumnIndex
            Case Cols.Max, Cols.Min
                'RefreshRequest()
        End Select
    End Sub



    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If _AllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.Checked, Cols.Max, Cols.Min
                    Distribueix()
                    RaiseEvent ValueChanged(Me, MatEventArgs.Empty)
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Checked
                DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOCsa)
        Dim retval As New List(Of DTOCsa)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            'Dim oMenu_Csb As New Menu_Csb(SelectedItems.First)
            'AddHandler oMenu_Csb.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_Csb.Range)
            'oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Function MatchesBank(oCsa As DTOCsa, oCsb As DTOCsb) As Boolean
        Dim retval As Boolean = oCsb.Iban.BankBranch.Bank.Equals(oCsa.Banc.iban.BankBranch.Bank)
        Return retval
    End Function

    Private Function MatchesBankGroup(oCsa As DTOCsa, oCsb As DTOCsb) As Boolean
        Dim retval As Boolean = False 'TO IMPLEMENT
        Return retval
    End Function

    Private Sub Distribueix()
        Dim oAllCsbs = Clon(_Csbs)

        'abuida remeses
        For Each oControlItem In _ControlItems
            Dim oCsa = oControlItem.Source
            oCsa.Items.Clear()

            'posa a zero els totals de cada remesa
            With oControlItem
                .Min = 0
                If oCsa.Banc.classificacio IsNot Nothing Then
                    .Classificacio = oCsa.Banc.classificacio.eur
                    If oCsa.Banc.disposat Is Nothing Then
                        .Max = .Classificacio
                    Else
                        .Max = .Classificacio - oCsa.Banc.disposat.eur
                    End If
                End If
                .Despeses = 0
                .Tot = 0
                .Tae = 0
            End With
        Next


        'propis
        For Each oControlItem In _ControlItems
            If oControlItem.Checked Then
                Dim oCsa = oControlItem.Source
                Dim oCsbs = oAllCsbs.Where(Function(x) MatchesBank(oCsa, x)).ToList
                If oCsbs.Count > 0 Then
                    oCsa.Items.AddRange(oCsbs)
                    oControlItem.Tot = oCsa.Items.Sum(Function(x) x.Amt.Eur)
                    oControlItem.Min = oControlItem.Tot
                    oAllCsbs.RemoveAll(Function(x) MatchesBank(oCsa, x))
                End If
            End If
        Next

        'grup
        For Each oControlItem In _ControlItems
            If oControlItem.Checked Then
                Dim oCsa = oControlItem.Source
                Dim oCsbs = oAllCsbs.Where(Function(x) MatchesBankGroup(oCsa, x)).ToList
                If oCsbs.Count > 0 Then
                    oCsa.Items.AddRange(oCsbs)
                    oControlItem.Tot = oCsa.Items.Sum(Function(x) x.Amt.Eur)
                    oAllCsbs.RemoveAll(Function(x) MatchesBankGroup(oCsa, x))
                End If
            End If
        Next

        'altres
        Dim idx As Integer

        Dim availableBancs = _ControlItems.Any(Function(x) x.Checked)
        If availableBancs Then
            For i As Integer = oAllCsbs.Count - 1 To 0 Step -1
                Dim success As Boolean = False
                Do
                    Dim oControlItem = _ControlItems(idx)
                    If oControlItem.Checked Then
                        Dim oCsa = oControlItem.Source
                        oCsa.Items.Add(oAllCsbs(i))
                        oControlItem.Tot = oCsa.Items.Sum(Function(x) x.Amt.Eur)
                        oAllCsbs.RemoveAt(i)
                        success = True
                    End If
                    idx += 1
                    If idx = _ControlItems.Count Then idx = 0
                    If success Then Exit Do
                Loop
            Next
        End If

        DataGridView1.Refresh()
        Application.DoEvents()

        If oAllCsbs.Count > 0 Then
            MsgBox("excedit per " & oAllCsbs.Count & " efectes i " & Format(oAllCsbs.Sum(Function(x) x.Amt.Eur), "#,###.00") & "Eur !", MsgBoxStyle.Exclamation)
        End If


    End Sub


    Protected Class ControlItem
        Property Source As DTOCsa

        Property Checked As Boolean
        Property Banc As String
        Property Tot As Decimal
        Property Min As Decimal
        Property Max As Decimal
        Property Classificacio As Decimal
        Property Despeses As Decimal
        Property Tae As Decimal

        Public Sub New(oCsa As DTOCsa)
            MyBase.New()
            _Source = oCsa
            With oCsa
                _Banc = .Banc.abr
                _Tot = DTOCsb.TotalNominal(.Items).eur

                If .Banc.classificacio IsNot Nothing Then
                    _Classificacio = .Banc.classificacio.eur
                End If

                If .NominalMaxim IsNot Nothing Then
                    _Max = .NominalMaxim.Eur
                End If

                If .NominalMinim IsNot Nothing Then
                    _Min = .NominalMinim.Eur
                End If

                'If .NominalMinim IsNot Nothing Then
                ' _Min = .NominalMinim.Eur
                ' End If
                ' If .NominalMaxim IsNot Nothing Then
                ' _Max = .NominalMaxim.Eur
                ' End If
                _Despeses = DTOCsa.TotalDespeses(oCsa).eur
                _Tae = DTOCsa.GetTAE(oCsa)
                _Checked = True
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class


End Class

