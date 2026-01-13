Public Class Frm_Newsletters
    Private _DefaultValue As DTONewsletter
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTONewsletter = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_Newsletters_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Newsletters1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Newsletters1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Newsletters1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Newsletters1.RequestToAddNew
        Dim oNewsletter As New DTONewsletter
        Dim oFrm As New Frm_Newsletter(oNewsletter)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Newsletters1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Newsletters1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oNewsletters = Await FEB2.Newsletters.All(exs)
        If exs.Count = 0 Then
            Xl_Newsletters1.Load(oNewsletters, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class