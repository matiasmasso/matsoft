Public Class Frm_Plantillas
    Private _DefaultValue As DTOPlantilla
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOPlantilla = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        Me.InitializeComponent()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_Plantillas_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Plantillas1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Plantillas1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Plantillas1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Plantillas1.RequestToAddNew
        Dim oPlantilla As New DTOPlantilla
        Dim oFrm As New Frm_Plantilla(oPlantilla)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Plantillas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Plantillas1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oPlantillas = Await FEB.Plantillas.All(exs, Current.Session.Emp)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_Plantillas1.Load(oPlantillas, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


End Class