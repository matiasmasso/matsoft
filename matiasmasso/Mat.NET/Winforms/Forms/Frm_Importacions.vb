Imports Winforms

Public Class Frm_Importacions
    Private _Proveidor As DTOProveidor
    Private _Importacions As List(Of DTOImportacio)
    Private _IsLoaded(10)
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse
    Private _AllowEvents As Boolean

    Public Event ItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        Gral
        Weeks
        Transits
    End Enum

    Public Sub New(Optional oProveidor As DTOProveidor = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _Proveidor = oProveidor
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_Importacions2_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadYears
        Await refresca()
    End Sub

    Private Sub LoadYears()
        Dim iYears As New List(Of Integer)
        For i = Today.Year + 1 To 2007 Step -1
            iYears.Add(i)
        Next
        Xl_Years1.Load(iYears, Today.Year)
    End Sub

    Private Async Sub Xl_Importacions1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Importacions1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _Importacions = Await FEB2.Importacions.All(exs, Current.Session.Emp, Xl_Years1.Value, _Proveidor)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            If Current.Session.User.Equals(DTOUser.Wellknown(DTOUser.Wellknowns.victoria)) Then
                _Importacions = _Importacions.OrderByDescending(Function(x) x.FchETD).ToList
            End If

            LoadProveidors()
            Xl_Importacions1.Load(_Importacions, Nothing, _SelectionMode)
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Dim oTab As Tabs = TabControl1.SelectedIndex
        If Not _IsLoaded(oTab) Then
            Select Case oTab
                Case Tabs.Weeks
                    Dim oImportacions = Await FEB2.Importacions.Weeks(exs, Current.Session.Emp)
                    Xl_ImportHeaderxWeeks1.Load(oImportacions)
                Case Tabs.Transits
                    Await LoadTransits()
            End Select
            _IsLoaded(oTab) = True
        End If
    End Sub

    Private Sub LoadProveidors()
        Dim Query As List(Of DTOProveidor) = _Importacions.
            Where(Function(x) x.Proveidor IsNot Nothing).
            GroupBy(Function(g) New With {Key g.Proveidor.Guid, g.Proveidor.FullNom}).
            Select(Function(group) New DTOProveidor With {
                .Guid = group.Key.Guid,
                .FullNom = group.Key.FullNom}).
                OrderBy(Function(x) x.FullNom).
                ToList

        Dim NoPrv As New DTOProveidor(Guid.Empty)
        NoPrv.FullNom = "(sel·leccionar proveidor)"
        Query.Insert(0, NoPrv)

        With ComboBoxProveidor
            .DataSource = Query
            .DisplayMember = "FullNom"
        End With
    End Sub

    Private Async Function LoadTransits() As Task
        Dim exs As New List(Of Exception)
        Dim oTransits = Await FEB2.Importacions.Transits(exs, Current.Session.Emp)
        If exs.Count = 0 Then
            Xl_ImportTransits1.Load(oTransits)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Function CurrentProveidor() As DTOProveidor
        Dim retval As DTOProveidor = ComboBoxProveidor.SelectedItem
        If retval.Guid.Equals(Guid.Empty) Then retval = Nothing
        Return retval
    End Function

    Private Sub ComboBoxProveidor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxProveidor.SelectedIndexChanged
        Dim oProveidor As DTOProveidor = CurrentProveidor()
        If oProveidor Is Nothing Then
            Xl_Importacions1.Load(_Importacions)
        Else
            Dim oFilteredValues As List(Of DTOImportacio) = _Importacions.Where(Function(x) x.Proveidor IsNot Nothing AndAlso x.Proveidor.Equals(oProveidor)).ToList
            Xl_Importacions1.Load(oFilteredValues)
        End If
    End Sub

    Private Async Sub Xl_ImportTransits1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ImportTransits1.RequestToRefresh
        Await LoadTransits()
    End Sub

    Private Sub Xl_Importacions1_ItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Importacions1.ItemSelected
        RaiseEvent ItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Importacions1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Importacions1.RequestToAddNew
        Dim exs As New List(Of Exception)
        If _Proveidor IsNot Nothing Then FEB2.Proveidor.Load(_Proveidor, exs)
        Dim oImportacio = DTOImportacio.Factory(GlobalVariables.Emp, _Proveidor)
        Dim oFrm As New Frm_Importacio(oImportacio)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Importacions1_SortedColumnChange(sender As Object, e As DatagridviewSortedColumnChangedEventArgs) Handles Xl_Importacions1.SortedColumnChange
        SaveSettingString("Frm_Importacions.SortedColumnIndex", e.SortedColumnIndex)
        SaveSettingString("Frm_Importacions.ListSortDirection", e.ListSortDirection)
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim oImportacions = Xl_Importacions1.SelectedItems
        Dim oSheet = DTOImportacio.Excel(_Importacions, Current.Session.Lang)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        Await refresca()
    End Sub
End Class
