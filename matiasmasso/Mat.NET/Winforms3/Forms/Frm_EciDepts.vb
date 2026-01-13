Public Class Frm_EciDepts
    Private _DefaultValue As DTO.Integracions.ElCorteIngles.Dept
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTO.Integracions.ElCorteIngles.Dept = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        Me.InitializeComponent()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_EciDepts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_ElCorteInglesDepts1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_ElCorteInglesDepts1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_ElCorteInglesDepts1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ElCorteInglesDepts1.RequestToAddNew
        Dim oEciDept As New DTO.Integracions.ElCorteIngles.Dept
        'Dim oFrm As New Frm_EciDept(oEciDept)
        'AddHandler oFrm.AfterUpdate, AddressOf refresca
        'oFrm.Show()
    End Sub

    Private Async Sub Xl_ElCorteInglesDepts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ElCorteInglesDepts1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oEciDepts = Await FEB.ElCorteInglesDepts.All(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_ElCorteInglesDepts1.Load(oEciDepts, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


End Class