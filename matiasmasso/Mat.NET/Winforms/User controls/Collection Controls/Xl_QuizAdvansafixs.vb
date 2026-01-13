Public Class Xl_QuizAdvansafixs

    Inherits DataGridView

    Private _Values As List(Of DTOQuizAdvansafix)
    Private _DefaultValue As DTOQuizAdvansafix
    Private _SelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Ico
        Nom
        PurchasedSICT
        QtySICT
        PurchasedNoSICT
        QtyNoSICT
    End Enum

    Public Shadows Sub Load(values As List(Of DTOQuizAdvansafix), Optional oDefaultValue As DTOQuizAdvansafix = Nothing, Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOQuizAdvansafix) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOQuizAdvansafix In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oTotals As ControlItem = ControlItem.Totals(_ControlItems)
        _ControlItems.Insert(0, oTotals)

        MyBase.DataSource = _ControlItems
        If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
        End If

        If _DefaultValue IsNot Nothing Then
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Nom)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOQuizAdvansafix)
        Dim retval As List(Of DTOQuizAdvansafix)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Customer.FullNom.ToLower.Contains(_Filter.ToLower) Or x.LastUser.EmailAddress.ToLower.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Values IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTOQuizAdvansafix
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOQuizAdvansafix = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With CType(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.PurchasedSICT)
            .HeaderText = "SICT"
            .DataPropertyName = "SICTPurchased"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.QtySICT)
            .HeaderText = "stock"
            .DataPropertyName = "QtySICT"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.PurchasedNoSICT)
            .HeaderText = "No SICT"
            .DataPropertyName = "NoSICTPurchased"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.QtyNoSICT)
            .HeaderText = "stock"
            .DataPropertyName = "QtyNoSICT"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOQuizAdvansafix)
        Dim retval As New List(Of DTOQuizAdvansafix)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oMenu_QuizAdvansafix As New Menu_QuizAdvansafix(SelectedItems.First)
            AddHandler oMenu_QuizAdvansafix.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_QuizAdvansafix.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add("emails", Nothing, AddressOf Do_EmailList)
        oContextMenu.Items.Add("emails pendents de resposta", Nothing, AddressOf Do_PendingEmailList)
        oContextMenu.Items.Add("clients pendents de resposta", Nothing, AddressOf Do_PendingCustomerList)
        oContextMenu.Items.Add("importar missatges oberts", Nothing, AddressOf Do_ImportOpenEmails)
        oContextMenu.Items.Add("exportar", Nothing, AddressOf Do_Exportar)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf RefreshRequest)

        If BLLSession.Current.User.Rol.IsSuperAdmin Then
            oContextMenu.Items.Add("reset", Nothing, AddressOf Do_Reset)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Reset()
        Dim rc As MsgBoxResult = MsgBox("Estem a punt de eliminar tots els registres dels clients. Confirmem?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLQuizAdvansafixs.Reset(exs) Then
                RefreshRequest()
            Else
                UIHelper.WarnError(exs, "error al resetejar els registres")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Do_EmailList()
        Dim oCsv As DTOCsv = BLL.BLLQuizAdvansafixs.CsvMailing
        UIHelper.SaveCsvDialog(oCsv, "Guardar destinataris email")
    End Sub

    Private Sub Do_PendingEmailList()
        Dim oCsv As DTOCsv = BLL.BLLQuizAdvansafixs.CsvPendingEmailList
        UIHelper.SaveCsvDialog(oCsv, "Guardar emails pendents de resposta")
    End Sub

    Private Sub Do_Exportar()
        Dim oCsv As DTOCsv = BLL.BLLQuizAdvansafixs.Exportar
        UIHelper.SaveCsvDialog(oCsv, "QuizAdvansafix results")
    End Sub

    Private Sub Do_PendingCustomerList()
        Dim oCsv As DTOCsv = BLL.BLLQuizAdvansafixs.CsvPendingCustomerList(BLL.BLLSession.Current.User)
        UIHelper.SaveCsvDialog(oCsv, "Guardar clients pendents de resposta")
    End Sub

    Private Sub Do_ImportOpenEmails()
        Dim oDlg As New OpenFileDialog()
        With oDlg
            .Filter = "Fitxers Csv (*.csv)|*.Csv|Tots els fitxers (*.*)|*.*"
            If .ShowDialog Then
                Dim sFilename As String = .FileName
                Dim oCsv As DTOCsv = Nothing
                Dim exs As New List(Of Exception)
                If BLLCsv.FromFile(sFilename, oCsv, exs) Then
                    If BLL.BLLQuizAdvansafixs.ImportOpenEmails(oCsv, exs) Then
                        RefreshRequest()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOQuizAdvansafix = CurrentControlItem.Source
            Select Case _SelectionMode
                Case BLL.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_QuizAdvansafix(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case BLL.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Xl_QuizAdvansafixs_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Select Case oControlItem.Cod
                    Case ControlItem.Cods.NotApplicable
                        e.Value = My.Resources.empty
                    Case ControlItem.Cods.NotSet
                        e.Value = My.Resources.LedOff
                    Case ControlItem.Cods.Read
                        e.Value = My.Resources.LedYellow
                    Case ControlItem.Cods.Clicked
                        e.Value = My.Resources.LedBlue
                    Case ControlItem.Cods.Success
                        e.Value = My.Resources.vb
                End Select
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOQuizAdvansafix

        Property Nom As String
        Property SICTPurchased As Integer
        Property QtySICT As Integer
        Property NoSICTPurchased As Integer
        Property QtyNoSICT As Integer
        Property Cod As Cods


        Public Enum Cods
            NotApplicable
            NotSet
            Read
            Clicked
            Success
        End Enum

        Public Sub New()
            MyBase.New
        End Sub

        Public Sub New(value As DTOQuizAdvansafix)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Customer.FullNom
                _SICTPurchased = .SICTPurchased
                _NoSICTPurchased = .NoSICTPurchased
                _QtySICT = .QtySICT
                _QtyNoSICT = .QtyNoSICT

                If .FchConfirmed <> Nothing Then
                    _Cod = Cods.Success
                ElseIf .FchBrowse <> Nothing Then
                    _Cod = Cods.Clicked
                ElseIf .SplioOpen = True Then
                    _Cod = Cods.Read
                Else
                    _Cod = Cods.NotSet
                End If
            End With
        End Sub

        Shared Function Totals(oControlItems As ControlItems) As ControlItem
            Dim retval As New ControlItem
            Dim iSuccess As Integer = oControlItems.ToList.FindAll(Function(x) x.Cod = Cods.Success).Count
            Dim iBrowsed As Integer = oControlItems.ToList.FindAll(Function(x) x.Cod = Cods.Clicked).Count
            With retval
                .Nom = "total " & oControlItems.Count & " clients. Llegit " & iBrowsed + iSuccess & ". Contestat " & iSuccess & " "
                .SICTPurchased = oControlItems.Sum(Function(x) x.SICTPurchased)
                .NoSICTPurchased = oControlItems.Sum(Function(x) x.NoSICTPurchased)
                .QtySICT = oControlItems.Sum(Function(x) x.QtySICT)
                .QtyNoSICT = oControlItems.Sum(Function(x) x.QtyNoSICT)
                .Cod = Cods.NotApplicable
            End With
            Return retval
        End Function
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


