

Public Class Frm_Rol_Select
    Private _DefaultValue As DTORol
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTORol = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_Rols_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Rols1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Rols1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Rols1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Rols1.RequestToAddNew
        Dim oRol As New DTORol
        Dim oFrm As New Frm_Rol(oRol)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Rols1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Rols1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub
    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oRols = Await FEB.Rols.All(exs)
        If exs.Count = 0 Then
            Dim userRol = Current.Session.User.Rol
            Select Case userRol.Id
                Case DTORol.Ids.SuperUser
                Case DTORol.Ids.Admin
                    oRols.Remove(oRols.Where(Function(x) x.Id = DTORol.Ids.SuperUser).First)
                Case DTORol.Ids.Accounts
                    oRols.Remove(oRols.Where(Function(x) x.Id = DTORol.Ids.SuperUser).First)
                    oRols.Remove(oRols.Where(Function(x) x.Id = DTORol.Ids.Admin).First)
                Case Else
                    oRols.Remove(oRols.Where(Function(x) x.Id = DTORol.Ids.SuperUser).First)
                    oRols.Remove(oRols.Where(Function(x) x.Id = DTORol.Ids.Admin).First)
                    oRols.Remove(oRols.Where(Function(x) x.Id = DTORol.Ids.Accounts).First)
            End Select
            Xl_Rols1.Load(oRols, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

End Class