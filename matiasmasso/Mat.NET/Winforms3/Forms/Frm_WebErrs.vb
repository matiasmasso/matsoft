Public Class Frm_WebErrs

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOWebErr = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_WebErrs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_WebErrs1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_WebErrs1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Async Sub Xl_WebErrs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_WebErrs1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oWebErrs = Await FEB.WebErrs.All(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_WebErrs1.Load(oWebErrs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class