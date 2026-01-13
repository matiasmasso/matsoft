Public Class Frm_EDiversaOrdrSp

    Private _EdiversaOrdrSp As DTOEdiversaOrdrsp
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOEdiversaOrdrsp)
        MyBase.New()
        Me.InitializeComponent()
        _EdiversaOrdrSp = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub refresca()
        _AllowEvents = False
        With _EdiversaOrdrSp

        End With
        _AllowEvents = True
    End Sub


End Class