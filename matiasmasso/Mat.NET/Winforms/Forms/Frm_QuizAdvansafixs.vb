Public Class Frm_QuizAdvansafixs
    Private _DefaultValue As DTOQuizAdvansafix
    Private _SelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOQuizAdvansafix = Nothing, Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_QuizAdvansafixs_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_QuizAdvansafixs1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_QuizAdvansafixs1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_QuizAdvansafixs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_QuizAdvansafixs1.RequestToAddNew
        Dim oQuizAdvansafix As New DTOQuizAdvansafix
        Dim oFrm As New Frm_QuizAdvansafix(oQuizAdvansafix)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_QuizAdvansafixs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_QuizAdvansafixs1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim oQuizAdvansafixs As List(Of DTOQuizAdvansafix) = BLL.BLLQuizAdvansafixs.All
        Xl_QuizAdvansafixs1.Load(oQuizAdvansafixs, _DefaultValue, _SelectionMode)
    End Sub

End Class