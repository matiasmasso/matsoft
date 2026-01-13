Public Class Frm_Xecs
    Private _Lliurador As DTOContact
    Private _DefaultValue As DTOXec
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOXec = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub
    Public Sub New(oLliurador As DTOContact)
        MyBase.New()
        _Lliurador = oLliurador
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_Xecs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Xecs1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Xecs1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Xecs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Xecs1.RequestToAddNew
        Dim oXec = DTOXec.Factory(Current.Session.Emp)
        Dim oFrm As New Frm_Xec(oXec)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Xecs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Xecs1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oXecs = Await FEB2.Xecs.Headers(exs, _Lliurador)
        If exs.Count = 0 Then
            Xl_Xecs1.Load(oXecs, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class