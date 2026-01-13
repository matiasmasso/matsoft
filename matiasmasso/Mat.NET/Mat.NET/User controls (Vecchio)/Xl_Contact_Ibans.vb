Public Class Xl_Contact_Ibans
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Ico
        Bank
        Format
        IcoEx
    End Enum

    Public Shadows Sub Load(value As List(Of DTOIban))
        _ControlItems = New ControlItems
        For Each oIban As DTOIban In value
            Dim oControlItem As New ControlItem(oIban)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Iban As DTOIban
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Return oControlItem.Source
        End Get
    End Property




    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewImageColumn)
            With CType(.Columns(Cols.Ico), DataGridViewImageColumn)
                .HeaderText = ""
                .DataPropertyName = "Ico"
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.NullValue = Nothing
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Bank)
                .HeaderText = "Entitat"
                .DataPropertyName = "Bank"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Format)
                .HeaderText = "Format"
                .DataPropertyName = "Format"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            .Columns.Add(New DataGridViewImageColumn)
            With CType(.Columns(Cols.Icoex), DataGridViewImageColumn)
                .HeaderText = ""
                .DataPropertyName = "IcoEx"
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.NullValue = Nothing
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
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
            Dim oIban As DTOIban = Me.Iban
            If oIban IsNot Nothing Then
                Dim oMenu_Iban As New Menu_Iban(oIban)
                AddHandler oMenu_Iban.AfterUpdate, AddressOf refreshrequest
                oContextMenu.Items.AddRange(oMenu_Iban.Range)
            End If
        End If

        oContextMenu.Items.Add(New ToolStripMenuItem("afegir...", Nothing, AddressOf Do_AddNew))

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub
    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOIban

        Property Ico As Image
        Property Bank As String
        Property Format As String
        Property IcoEx As Image

        Public Sub New(oIban As DTOIban)
            MyBase.New()
            _Source = oIban
            With oIban
                _Ico = IIf(oIban.DocFile Is Nothing, Nothing, My.Resources.pdf)
                If .BankBranch Is Nothing Then
                    _Bank = .Digits
                Else
                    _Bank = BLL.BLLBank.NomComercialORaoSocial(.BankBranch.Bank)
                End If
                _Format = .Format.ToString
                If .FchTo > Nothing Then
                    If .FchTo < Today Then
                        _IcoEx = My.Resources.aspa
                    End If
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class

