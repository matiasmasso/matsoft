Public Class Frm_ConsumerTickets
    Private _DefaultValue As DTOConsumerTicket
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOConsumerTicket = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        Me.InitializeComponent()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Xl_Years1.LoadFrom(2020)
    End Sub

    Private Async Sub Frm_ConsumerTickets_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_ConsumerTickets1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_ConsumerTickets1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_ConsumerTickets1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ConsumerTickets1.RequestToAddNew
        Dim oConsumerTicket = DTOConsumerTicket.Factory(Current.Session.User)
        Dim oFrm As New Frm_ConsumerTicket(oConsumerTicket)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_ConsumerTickets1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ConsumerTickets1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oEmp = GlobalVariables.Emp
        Dim year = Xl_Years1.Value
        Dim oConsumerTickets = Await FEB2.ConsumerTickets.All(exs, oEmp, year)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_ConsumerTickets1.Load(oConsumerTickets, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub RefrescaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefrescaToolStripMenuItem.Click
        Await refresca()
    End Sub

    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        Await refresca()
    End Sub
End Class