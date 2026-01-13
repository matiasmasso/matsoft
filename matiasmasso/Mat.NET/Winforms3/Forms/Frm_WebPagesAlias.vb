Public Class Frm_WebPagesAlias

    Private _DefaultValue As DTOWebPageAlias
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse
    Private _AllowEvents As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOWebPageAlias = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_WebPagesAlias_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
        _AllowEvents = True
    End Sub

    Private Sub Xl_WebPagesAlias1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_WebPagesAlias1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_WebPagesAlias1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_WebPagesAlias1.RequestToAddNew
        Dim oWebPageAlias As New DTOWebPageAlias()
        oWebPageAlias.domain = DTOWebPageAlias.Domains.All
        Dim oFrm As New Frm_WebPageAlias(oWebPageAlias)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_WebPagesAlias1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_WebPagesAlias1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub


    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oWebPagesAlias = Await FEB.WebPagesAlias.All(exs)
        If exs.count = 0 Then
            Xl_WebPagesAlias1.Load(oWebPagesAlias, _DefaultValue, _SelectionMode)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        If _AllowEvents Then
            Xl_WebPagesAlias1.Filter = e.Argument
        End If
    End Sub
End Class