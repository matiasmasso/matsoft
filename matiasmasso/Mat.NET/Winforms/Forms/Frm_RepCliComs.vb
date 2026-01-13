Public Class Frm_RepCliComs

    Private _Rep As DTORep

    Public Sub New(oRep As DTORep)
        MyBase.New
        InitializeComponent()

        _Rep = oRep
        Me.Text = "Exclusions de " & _Rep.NickName
    End Sub


    Private Async Sub Frm_RepCliComs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_RepCliComs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_RepCliComs1.RequestToAddNew
        Dim oRepCliCom As New DTORepCliCom
        With oRepCliCom
            .Rep = _Rep
            .Fch = Today
            .ComCod = DTORepCliCom.ComCods.Standard
            .UsrCreated = Current.Session.User
        End With
        Dim oFrm As New Frm_RepCliCom(oRepCliCom)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_RepCliComs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_RepCliComs1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oRepCliComs = Await FEB2.RepCliComs.All(exs, _Rep)
        If exs.Count = 0 Then
            Xl_RepCliComs1.Load(oRepCliComs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class