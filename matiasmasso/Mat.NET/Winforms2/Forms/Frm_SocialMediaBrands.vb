Public Class Frm_SocialMediaWidgets
    Private _DefaultValue As DTOSocialMediaWidget
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOSocialMediaWidget = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_SocialMediaWidgets_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_SocialMediaWidgets1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_SocialMediaWidgets1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_SocialMediaWidgets1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_SocialMediaWidgets1.RequestToAddNew
        Dim oSocialMediaWidget As New DTOSocialMediaWidget
        Dim oFrm As New Frm_SocialMediaWidget(oSocialMediaWidget)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_SocialMediaWidgets1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_SocialMediaWidgets1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub
    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oSocialMediaWidgets = Await FEB.SocialMediaWidgets.All(exs)
        If exs.Count = 0 Then
            Xl_SocialMediaWidgets1.Load(oSocialMediaWidgets, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

End Class