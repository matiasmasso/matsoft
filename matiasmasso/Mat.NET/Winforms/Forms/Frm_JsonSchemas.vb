Public Class Frm_JsonSchemas

    Private _DefaultValue As DTOJsonSchema
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOJsonSchema = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_JsonSchemas_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_JsonSchemas1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_JsonSchemas1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_JsonSchemas1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_JsonSchemas1.RequestToAddNew
        Dim oJsonSchema = DTOJsonSchema.Factory(Current.Session.User)
        Dim oFrm As New Frm_JsonSchema(oJsonSchema)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_JsonSchemas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_JsonSchemas1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oJsonSchemas = Await FEB2.JsonSchemas.All(exs)
        If exs.Count = 0 Then
            Xl_JsonSchemas1.Load(oJsonSchemas, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class