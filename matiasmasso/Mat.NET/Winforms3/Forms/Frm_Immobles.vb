Public Class Frm_Immobles
    Private _DefaultValue As DTOImmoble
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOImmoble = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_Immobles_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_Immobles1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Immobles1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Immobles1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Immobles1.RequestToAddNew
        Dim oImmoble As New DTOImmoble
        Dim oFrm As New Frm_Immoble(oImmoble)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Immobles1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Immobles1.RequestToRefresh
        refresca()
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        Dim oImmobles = Await FEB.Immobles.All(GlobalVariables.Emp, exs)
        If exs.Count = 0 Then
            Xl_Immobles1.Load(oImmobles, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oImmobles = Await FEB.Immobles.All(GlobalVariables.Emp, exs)
        If exs.Count = 0 Then
            Dim oSheet = FEB.Immobles.Excel(oImmobles, Current.Session.Lang)
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class