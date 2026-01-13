Public Class Frm_CliReturns
    Private _DefaultValue As DTOCliReturn
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOCliReturn = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_CliReturns_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_CliReturns1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_CliReturns1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_CliReturns1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_CliReturns1.RequestToAddNew
        Dim oCliReturn = DTOCliReturn.Factory(Current.Session.User)

        Dim oFrm As New Frm_CliReturn(oCliReturn)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_CliReturns1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CliReturns1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub
    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oCliReturns = Await FEB.CliReturns.All(exs)
        If exs.Count = 0 Then
            Xl_CliReturns1.Load(oCliReturns, _DefaultValue, _SelectionMode)

            Dim oMgzs As List(Of DTOMgz) = oCliReturns.GroupBy(Function(g) g.Mgz).Select(Function(group) group.Key).Distinct.ToList
            With ComboBox1
                .DataSource = oMgzs
                .DisplayMember = "FullNom"
            End With
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_CliReturns1.Filter = e.Argument
    End Sub
End Class